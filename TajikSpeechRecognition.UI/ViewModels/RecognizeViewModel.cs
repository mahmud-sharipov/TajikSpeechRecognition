using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Services;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class RecognizeViewModel : BaseViewModel
    {
        public RecognizeViewModel()
        {
            Audio = new Audio() { FileName = "audio_to_recognize" };

            DeviceEnumerator = new MMDeviceEnumerator();
            Devices = DeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
                Microphones.Add(WaveIn.GetCapabilities(i));
            if (Microphones.Any())
                SelectedDevice = Devices.SingleOrDefault(d => d.FriendlyName.StartsWith(Microphones.FirstOrDefault().ProductName));
        }

        public Audio Audio { get; set; }

        private string error;
        public string Error
        {
            get => error;
            set
            {
                error = value;
                RaisePropertyChanged();
            }
        }

        private string result;
        public string Result
        {
            get => result;
            set
            {
                result = value;
                RaisePropertyChanged();
            }
        }

        #region Recognition
        readonly MMDeviceEnumerator DeviceEnumerator;
        WaveIn sourceStream;
        WaveFileWriter waveWriter;

        List<WaveInCapabilities> Microphones = new List<WaveInCapabilities>();
        public ICollection<MMDevice> Devices { get; set; }

        MMDevice selectedDevice;
        public MMDevice SelectedDevice
        {
            get => selectedDevice;
            set
            {
                selectedDevice = value;
                RaisePropertyChanged();
            }
        }

        bool isRecording;
        public bool IsRecording
        {
            get => isRecording;
            set
            {
                isRecording = value;
                RaisePropertyChanged();
            }
        }

        private double microphonePeakValue;
        public double MicrophonePeakValue
        {
            get => microphonePeakValue;
            set
            {
                microphonePeakValue = value;
                RaisePropertyChanged();
            }
        }

        public ICommand PlayAudio => new Command(o =>
        {
            if (File.Exists($"{AppManager.TempDir}/{Audio.FileName}.wav"))
                AudioPlayer.PlayAudio(Audio, AppManager.TempDir);
        });

        public void StartRecording()
        {
            if (isRecording) return;
            Error = "";
            if (selectedDevice == null)
            {
                Error = "Таҷҳизот барои сабт интихоб нашудааст!";
                return;
            }
            sourceStream = new WaveIn
            {
                DeviceNumber = Microphones.IndexOf(Microphones.Single(m => SelectedDevice.FriendlyName.StartsWith(m.ProductName))),
                WaveFormat = new WaveFormat(16000, 16, 1)
            };
            sourceStream.DataAvailable += SourceStreamDataAvailable;
            waveWriter = new WaveFileWriter($"{AppManager.TempDir}/{Audio.FileName}.wav", sourceStream.WaveFormat);
            IsRecording = true;

            sourceStream.StartRecording();
        }

        public void SourceStreamDataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter == null) return;
            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
            MicrophonePeakValue = (SelectedDevice?.AudioMeterInformation.MasterPeakValue ?? 0) * 100;
        }

        public void StopRecording()
        {
            if (!IsRecording) return;
            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }
            if (this.waveWriter == null)
            {
                return;
            }
            this.waveWriter.Dispose();
            this.waveWriter = null;
            IsRecording = false;
            MicrophonePeakValue = 0;
            RecognizeAudio();
        }

        protected void RecognizeAudio()
        {
            Task.Run(() =>
            {
                try
                {
                    var modelName = AppManager.ModelName;
                    var modelDir = AppManager.OutputDir + $"/{modelName}";
                    var cd = Directory.GetCurrentDirectory()[0].ToString().ToUpper();
                    cd = cd == "C" ? "" : $"/{cd}";
                    var script = $"/C cd {cd} {AppManager.SphinxDir}\\pocketsphinx&pocketsphinx_continuous -inmic \"yes\" -hmm \"{modelDir}\\model_parameters\\{modelName}.ci_cont\" -dict \"{modelDir}\\etc\\{modelName}.dic\" -lm \"{modelDir}\\etc\\{modelName}.lm.DMP\" -logfn nul -infile {AppManager.TempDir}\\{Audio.FileName}.wav";
                    CommandPrompt.Run(script, r => Result = r);
                }
                catch (Exception ex)
                {
                    UIManager.LogManager.LogError(ex.Message);
                }
            });
        }
        #endregion
    }
}
