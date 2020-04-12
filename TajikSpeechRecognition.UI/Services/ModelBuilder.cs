using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.Services
{
    public class ModelBuilder : INotifyPropertyChanged
    {
        public string ModelName => AppManager.ModelName;
        string ModelDir => AppManager.OutputDir + $"/{ModelName}";
        string ModelOutputDir => AppManager.OutputDir + $"/{ModelName}/Output";
        string EtcDir => ModelDir + "/etc";
        string WavDir => ModelDir + "/wav";
        string ARPAFilePaht = "";

        public ModelBuilder(DataProvider dataProvider, string _ARPAFilePaht)
        {
            ARPAFilePaht = _ARPAFilePaht;
            DataProvider = dataProvider;
        }

        public DataProvider DataProvider { get; set; }

        private bool isBuilding;
        public bool IsBuilding
        {
            get => isBuilding;
            set
            {
                isBuilding = value;
                RaisePropertyChanged();
            }
        }

        private bool canBuild = true;
        public bool CanBuild
        {
            get => canBuild;
            set
            {
                canBuild = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Build => new Command(BuildModel);

        public void BuildModel(object param)
        {
            Task.Run(() =>
            {
                CanBuild = false;
                IsBuilding = true;
                UIManager.LogManager.LogInfo("Build started...");

                if (Directory.Exists(ModelDir))
                    UIManager.LogManager.LogInfo("Recreating model directories...");
                else
                    UIManager.LogManager.LogInfo("Creating model directories...");

                RecreateDirectories(ModelDir);
                Directory.CreateDirectory(ModelDir);
                Directory.CreateDirectory(ModelOutputDir);
                Directory.CreateDirectory(EtcDir);
                Directory.CreateDirectory(WavDir);
                UIManager.LogManager.LogInfo("Model directories created!");

                GenerateModelFiles();
                CopyARPAFile();
                CopyAudios();
                RunBuildCommand();
                UIManager.LogManager.LogInfo("Build finished successfully!");
                CanBuild = true;
                IsBuilding = false;
            });
        }

        protected void RecreateDirectories(string modelDir)
        {
            try
            {
                if (Directory.Exists(modelDir))
                {
                    string[] files = Directory.GetFiles(modelDir);
                    string[] dirs = Directory.GetDirectories(modelDir);
                    foreach (string file in files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                    foreach (string dir in dirs)
                        RecreateDirectories(dir);
                    Directory.Delete(modelDir, false);
                }
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }

        protected void GenerateModelFiles()
        {
            UIManager.LogManager.LogInfo("Generating model files...");
            try
            {
                var sw = new StreamWriter(EtcDir + "/feat.params");
                sw.Write(ConfigTemplates.GetFeatParams());
                sw.Close();

                sw = new StreamWriter(EtcDir + "/sphinx_train.cfg");
                sw.Write(ConfigTemplates.GetSphinxTrainCongig(AppManager.ModelName));
                sw.Close();

                sw = new StreamWriter(EtcDir + $"/{AppManager.ModelName}.filler");
                sw.WriteLine("<s> SIL");
                sw.WriteLine("</s> SIL");
                sw.WriteLine("<sil> SIL");
                sw.Close();
                var words = DataProvider.GetEntities<Word>();
                sw = new StreamWriter(EtcDir + $"/{AppManager.ModelName}.phone");
                sw.Write(ConfigTemplates.GetPhonemes(words.Select(w => w.Phonemes)));
                sw.Close();

                var wordsDic = words.Select(w => w.Value + " " + w.Phonemes + "\r\n").ToList();
                sw = new StreamWriter(EtcDir + $"/{AppManager.ModelName}.dic");
                var sw2 = new StreamWriter(ModelOutputDir + $"/{AppManager.ModelName}.dic");
                var sw3 = new StreamWriter($"{AppManager.TempDir}/tajik.txt");
                wordsDic.ForEach(w =>
                {
                    sw.Write(w);
                    sw2.Write(w);
                });
                sw3.Write(string.Join("\r\n", words.Select(w => w.Value)));
                sw.Close();
                sw2.Close();
                sw3.Close();

                var audiosPath = GetAudios();
                sw = new StreamWriter(EtcDir + $"/{AppManager.ModelName}_test.fileids");
                sw.Write(audiosPath);
                sw.Close();

                sw = new StreamWriter(EtcDir + $"/{AppManager.ModelName}_train.fileids");
                sw.Write(audiosPath);
                sw.Close();

                var texts = GetTextAndAudio();
                sw = new StreamWriter(EtcDir + $"/{AppManager.ModelName}_test.transcription");
                sw.Write(texts);
                sw.Close();

                sw = new StreamWriter(EtcDir + $"/{AppManager.ModelName}_train.transcription");
                sw.Write(texts);
                sw.Close();

                UIManager.LogManager.LogInfo("Model files generated!");
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }

        protected void CopyARPAFile()
        {
            UIManager.LogManager.LogInfo("Copying ARPA file...");
            try
            {
                File.Copy(ARPAFilePaht, $"{EtcDir}/{ModelName}.lm.DMP", true);
                File.Copy(ARPAFilePaht, $"{ModelOutputDir}/{ModelName}.lm.DMP", true);
                UIManager.LogManager.LogInfo("ARPA file copied!");
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }

        protected void CopyAudios()
        {
            UIManager.LogManager.LogInfo("Copying audio files...");
            try
            {
                foreach (var audio in DataProvider.GetEntities<Audio>())
                {
                    var speakerName = audio.Speaker.Name;
                    if (!Directory.Exists($"{WavDir}/{speakerName}"))
                        Directory.CreateDirectory($"{WavDir}/{speakerName}");

                    UIManager.LogManager.LogInfo($"Copying {audio.Speaker.Name}'s file {audio.FileName}.wav");

                    File.Copy(AppManager.AudiosDir + "/" + audio.FileName + ".wav", $"{WavDir}/{speakerName}/{audio.FileName}.wav", true);
                }
                UIManager.LogManager.LogInfo("Audio files copied!");
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }

        protected void RunBuildCommand()
        {
            UIManager.LogManager.LogInfo("Building model...");
            try
            {
                var cd = Directory.GetCurrentDirectory()[0].ToString().ToUpper();
                cd = cd == "C" ? "" : $"/{cd}";
                var script = $"/C cd {cd} {ModelDir}&python ../../sphinx/sphinxtrain/scripts/sphinxtrain run";
                CommandPrompt.Run(script);
                UIManager.LogManager.LogInfo("Model build successfully!");
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }

        protected void GenerateCMDFile()
        {
            UIManager.LogManager.LogInfo("Generating cmd file...");
            try
            {
                var sw = new StreamWriter(ModelDir + $"/{ModelName}.cmd");
                var cd = Directory.GetCurrentDirectory()[0].ToString().ToUpper();
                cd = cd == "C" ? "" : $"/{cd}";
                sw.WriteLine($"cd ../../sphinx/pocketsphinx");
                sw.WriteLine($"pocketsphinx_continuous -inmic \"yes\" -hmm \"{ModelDir}\\model_parameters\\{ModelName}.ci_cont\" -dict \"{ModelDir}\\etc\\{ModelName}.dic\" -lm \"{ModelDir}\\etc\\{ModelName}.lm.DMP\" -logfn nul");
                sw.Close();
                UIManager.LogManager.LogInfo("cmd file generated...");
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }

        protected string GetAudios()
        {
            var builder = new StringBuilder();
            var audiosPath = DataProvider.GetEntities<Audio>().OrderBy(a => a.FileName).Select(a => a.Speaker.Name + "/" + a.FileName).ToList();
            foreach (var path in audiosPath)
                builder.AppendLine(path);

            return builder.ToString();
        }

        protected string GetTextAndAudio()
        {
            var builder = new StringBuilder();
            var audiosPath = DataProvider.GetEntities<Audio>().OrderBy(a => a.FileName).Select(a => "<s> " + a.Text.Value + " </s> (" + a.Speaker.Name + "/" + a.FileName + ")").ToList();
            foreach (var path in audiosPath)
                builder.AppendLine(path.Replace(".wav", ""));

            return builder.ToString();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
