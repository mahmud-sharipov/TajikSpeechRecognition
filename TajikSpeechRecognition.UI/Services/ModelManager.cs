using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.Services
{
    public class ModelManager : INotifyPropertyChanged
    {
        public DataProvider DataProvider { get; set; }

        public ModelManager(DataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        private bool isRecognizing;
        public bool IsRecognizing
        {
            get => isRecognizing;
            set
            {
                isRecognizing = value;
                RaisePropertyChanged();
            }
        }

        private bool canRecognize = true;
        public bool CanRecognize
        {
            get => canRecognize;
            set
            {
                canRecognize = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Recognize => new Command(StartRecognize);

        public ICommand StopRecognize => new Command(StopLastProcess);

        private void StartRecognize(object obj)
        {
            Task.Run(() =>
            {
                try
                {
                    IsRecognizing = true;
                    CanRecognize = false;
                    var modelName = AppManager.ModelName;
                    var modelDir = AppManager.OutputDir + $"/{modelName}";
                    var cd = Directory.GetCurrentDirectory()[0].ToString().ToUpper();
                    cd = cd == "C" ? "" : $"/{cd}";
                    var script = $"/C cd {cd} {AppManager.SphinxDir}\\pocketsphinx&pocketsphinx_continuous -inmic \"yes\" -hmm \"{modelDir}\\model_parameters\\{modelName}.ci_cont\" -dict \"{modelDir}\\etc\\{modelName}.dic\" -lm \"{modelDir}\\etc\\{modelName}.lm.DMP\" -logfn nul";
                    CommandPrompt.Run(script, null);
                }
                catch (Exception ex)
                {
                    UIManager.LogManager.LogError(ex.Message);
                }
            });
        }

        private void StopLastProcess(object obj)
        {
            try
            {
                CommandPrompt.ActiveProcess.StandardInput.Close();
                CommandPrompt.ActiveProcess.Kill();
                CommandPrompt.ActiveProcess.CancelOutputRead();
                CommandPrompt.ActiveProcess.Close();
                CommandPrompt.ActiveProcess.Dispose();

                IsRecognizing = false;
                CanRecognize = true;
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
