using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TajikSpeechRecognition.Model;

namespace TajikSpeechRecognition
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public DataProvider DataProvider { get; set; }

        public ObservableCollection<Text> Texts { get; set; }

        public ObservableCollection<Word> Words { get; set; }

        public ObservableCollection<Audio> Audios { get; set; }

        public ObservableCollection<Speaker> Speakers { get; set; }

        private string logs;

        public string Logs
        {
            get { return logs; }
            set { logs = value; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
            DataProvider = new DataProvider();
            InitData();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string popertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(popertyName));
        #endregion

        private void InitData()
        {
            Task.Run(() =>
            {
                Texts = new ObservableCollection<Text>(DataProvider.GetEntities<Text>().ToList());
                RaisePropertyChanged(nameof(Texts));
                Words = new ObservableCollection<Word>(DataProvider.GetEntities<Word>().ToList());
                RaisePropertyChanged(nameof(Words));
                Speakers = new ObservableCollection<Speaker>(DataProvider.GetEntities<Speaker>().ToList());
                RaisePropertyChanged(nameof(Speakers));
                Audios = new ObservableCollection<Audio>(DataProvider.GetEntities<Audio>().ToList());
                RaisePropertyChanged(nameof(Audios));
            });
        }

        public Action<string> Log { get; set; } = (s) => Console.WriteLine(s);

        #region Creator

        public void AddSpeaker(Speaker speaker)
        {
            DataProvider.Add(speaker);
            Speakers.Add(speaker);
            DataProvider.SaveChanges();
        }

        public void AddSpeaker(string name, int age, bool isMale) => AddSpeaker(new Speaker() { Name = name, Age = age, IsMale = isMale });

        public void AddText(Text text)
        {
            DataProvider.Add(text);
            Texts.Add(text);

            text.Value.Split(' ').ToList().ForEach(word =>
            {
                if (!Words.Any(w => w.Value == word))
                {
                    var newWord = new Word()
                    {
                        Value = word,
                        Phonemes = GetWordPhonemes(word)
                    };
                    DataProvider.Add(newWord);
                    Words.Add(newWord);
                }
            });
            DataProvider.SaveChanges();
        }

        public void AddText(string value, string description, int expectedTime, string id) => AddText(new Text() { Value = value, Description = description, ExpectedTime = expectedTime, Id = id });

        protected string GetWordPhonemes(string word)
        {
            var phonemes = "";
            foreach (var char_ in word)
                phonemes += Phonemes.List[char_.ToString()] + " ";

            return phonemes.Trim();
        }
        #endregion

        #region Build model
        public void BuildModel(string ARPAFilePaht)
        {
            Task.Run(() =>
            {
                var modelName = AppManager.ModelName;
                Log("Build started...");
                var modelDir = AppManager.OutputDir + $"/{modelName}";
                var modelOutputDir = AppManager.OutputDir + $"/{modelName}/Output";
                var etcDir = modelDir + "/etc";
                var wavDir = modelDir + "/wav";

                Log("Recreating model directories...");
                if (Directory.Exists(modelDir))
                    DeleteDirectory(modelDir);

                Directory.CreateDirectory(modelDir);
                Directory.CreateDirectory(modelOutputDir);
                Directory.CreateDirectory(etcDir);
                Directory.CreateDirectory(wavDir);
                Log("Model directories recreated!");

                Log("Generating model files...");
                GenerateModelFiles(etcDir, modelOutputDir);
                Log("Model files generated!");

                Log("Copying ARPA file...");
                File.Copy(ARPAFilePaht, $"{etcDir}/{modelName}.lm.DMP", true);
                File.Copy(ARPAFilePaht, $"{modelOutputDir}/{modelName}.lm.DMP", true);
                Log("ARPA file copied!");

                Log("Copying audio files...");
                CopyAudios(wavDir);
                Log("Audio files copied!");

                Log("Build model...");
                Build(modelDir);

                Log("Generating cmd file...");
                var sw = new StreamWriter(modelDir + $"/{modelName}.cmd");
                var cd = Directory.GetCurrentDirectory()[0].ToString().ToUpper();
                cd = cd == "C" ? "" : $"/{cd}";
                sw.WriteLine($"cd {cd} {AppManager.SphinxDir}\\pocketsphinx");
                sw.WriteLine($"pocketsphinx_continuous -inmic \"yes\" -hmm \"{modelDir}\\model_parameters\\{modelName}.ci_cont\" -dict \"{modelDir}\\etc\\{modelName}.dic\" -lm \"{modelDir}\\etc\\{modelName}.lm.DMP\" -logfn nul");
                sw.Close();

                Log("Model build successfully!");
            });
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
                DeleteDirectory(dir);

            Directory.Delete(target_dir, false);
        }

        protected void GenerateModelFiles(string etcDir, string modelOutputDir)
        {
            var sw = new StreamWriter(etcDir + "/feat.params");
            sw.Write(ConfigTemplates.GetFeatParams());
            sw.Close();

            sw = new StreamWriter(etcDir + "/sphinx_train.cfg");
            sw.Write(ConfigTemplates.GetSphinxTrainCongig(AppManager.ModelName));
            sw.Close();

            sw = new StreamWriter(etcDir + $"/{AppManager.ModelName}.filler");
            sw.WriteLine("<s> SIL");
            sw.WriteLine("</s> SIL");
            sw.WriteLine("<sil> SIL");
            sw.Close();

            sw = new StreamWriter(etcDir + $"/{AppManager.ModelName}.phone");
            sw.Write(ConfigTemplates.GetPhonemes(Words));
            sw.Close();

            var words = Words.Select(w => w.Value + " " + w.Value + "\r\n").ToList();
            sw = new StreamWriter(etcDir + $"/{AppManager.ModelName}.dic");
            var sw2 = new StreamWriter(modelOutputDir + $"/{AppManager.ModelName}.dic");
            words.ForEach(w =>
            {
                sw.Write(w);
                sw2.Write(w);
            });
            sw.Close();
            sw2.Close();

            var audiosPath = GetAudios();
            sw = new StreamWriter(etcDir + $"/{AppManager.ModelName}_test.fileids");
            sw.Write(audiosPath);
            sw.Close();

            sw = new StreamWriter(etcDir + $"/{AppManager.ModelName}_train.fileids");
            sw.Write(audiosPath);
            sw.Close();

            var texts = GetTextAndAudio();
            sw = new StreamWriter(etcDir + $"/{AppManager.ModelName}_test.transcription");
            sw.Write(texts);
            sw.Close();

            sw = new StreamWriter(etcDir + $"/{AppManager.ModelName}_train.transcription");
            sw.Write(texts);
            sw.Close();
        }

        protected void CopyAudios(string wavDir)
        {
            foreach (var audio in Audios)
            {
                var speakerName = audio.Speaker.Name;
                if (!Directory.Exists($"{wavDir}/{speakerName}"))
                    Directory.CreateDirectory($"{wavDir}/{speakerName}");

                Log($"Copying {audio.Speaker.Name}'s file {audio.FileName}.wav");

                File.Copy(AppManager.AudiosDir + "/" + audio.FileName + ".wav", $"{wavDir}/{speakerName}/{audio.FileName}.wav", true);
            }
        }

        protected void Build(string modelDir)
        {
            var cd = Directory.GetCurrentDirectory()[0].ToString().ToUpper();
            cd = cd == "C" ? "" : $"/{cd}";
            var script = $"/C cd {cd} {modelDir}&python ../../sphinx/sphinxtrain/scripts/sphinxtrain run";
            RunCMDCommand(script);
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
        #endregion

        #region Run model
        public void RunModel()
        {
            Task.Run(() =>
            {
                var modelName = AppManager.ModelName;
                var modelDir = AppManager.OutputDir + $"/{modelName}";
                var cd = Directory.GetCurrentDirectory()[0].ToString().ToUpper();
                cd = cd == "C" ? "" : $"/{cd}";
                var script = $"/C cd {cd} {AppManager.SphinxDir}\\pocketsphinx&pocketsphinx_continuous -inmic \"yes\" -hmm \"{modelDir}\\model_parameters\\{modelName}.ci_cont\" -dict \"{modelDir}\\etc\\{modelName}.dic\" -lm \"{modelDir}\\etc\\{modelName}.lm.DMP\" -logfn nul";
                RunCMDCommand(script);
            });
        }
        #endregion

        protected void RunCMDCommand(string script)
        {
            try
            {
                var process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = script;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo = startInfo;
                process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        Log(e.Data);
                });
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                Log($"ERROR: {ex.Message}");
            }
        }

    }
}
