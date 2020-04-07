using MaterialDesignThemes.Wpf;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Pages;
using TajikSpeechRecognition.UI.Services;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class AudioViewModel : BaseViewModel
    {
        public AudioViewModel(ObservableCollection<Audio> audios) : this(new Audio(), audios) { }

        public AudioViewModel(Audio audio, ObservableCollection<Audio> audios)
        {
            Audio = audio;
            Audio.FileName = Guid.NewGuid().ToString().Replace("-", "");
            Speakers = DataProvider.GetEntities<Speaker>().ToList();
            Texts = DataProvider.GetEntities<Text>().ToList();
            Audios = audios;

            DeviceEnumerator = new MMDeviceEnumerator();
            Devices = DeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
                Microphones.Add(WaveIn.GetCapabilities(i));
            if (Microphones.Any())
                SelectedDevice = Devices.SingleOrDefault(d => d.FriendlyName.StartsWith(Microphones.FirstOrDefault().ProductName));
        }

        public Audio Audio { get; set; }

        public ObservableCollection<Audio> Audios { get; set; }

        private string speakerSearchText = "";
        public string SpeakerSearchText
        {
            get => speakerSearchText;
            set
            {
                speakerSearchText = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(FilteredSpeakers));
            }
        }

        public ICollection<Speaker> Speakers { get; set; }

        public ICollection<Speaker> FilteredSpeakers => Speakers.Where(s => (speakerSearchText != "" ? s.Name.ToLower().Contains(speakerSearchText.ToLower()) : true)).ToList();

        private string textSearchText = "";
        public string TextSearchText
        {
            get => textSearchText;
            set
            {
                textSearchText = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(FilteredTexts));
            }
        }

        public ICollection<Text> Texts { get; set; }

        public ICollection<Text> FilteredTexts => Texts.Where(s => (textSearchText != "" ? s.Value.ToLower().Contains(textSearchText.ToLower()) : true)).ToList();

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

        public ICommand SaveAndCreateNew => new Command(o =>
        {
            var result = AudioService.SaveAudio(Audio, DataProvider);
            if (result.Item1)
            {
                Audios.Add(Audio);
                Audio = new Audio()
                {
                    Speaker = Audio.Speaker,
                    Text = Audio.Text,
                    FileName = Guid.NewGuid().ToString().Replace("-", "")
                };
            }
            else
                Error = result.Item2;
        });

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
            if (File.Exists($"{AppManager.AudiosDir}/{Audio.FileName}.wav"))
                AudioPlayer.PlayAudio(Audio);
        });

        public void StartRecording()
        {
            Error = "";
            if (selectedDevice == null)
            {
                Error = "Device is not selected";
                return;
            }
            sourceStream = new WaveIn
            {
                DeviceNumber = Microphones.IndexOf(Microphones.Single(m => SelectedDevice.FriendlyName.StartsWith(m.ProductName))),
                WaveFormat = new WaveFormat(16000, 16, 1)
            };
            sourceStream.DataAvailable += SourceStreamDataAvailable;
            waveWriter = new WaveFileWriter($"{AppManager.AudiosDir}/{Audio.FileName}.wav", sourceStream.WaveFormat);
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
        }
        #endregion

        public static void ClosingEventHandler(object sender, DialogClosingEventArgs e)
        {
            var viewModel = ((AudioViewModel)(e.Content as AudioPage).DataContext);
            var audio = viewModel.Audio;
            if ((bool)e.Parameter == false)
            {
                if (File.Exists($"{AppManager.AudiosDir}/{audio.FileName}.wav"))
                    File.Delete($"{AppManager.AudiosDir}/{audio.FileName}.wav");
            }
            else
            {
                var result = AudioService.SaveAudio(audio, viewModel.DataProvider);
                if (result.Item1)
                    viewModel.Audios.Add(audio);
                else
                {
                    viewModel.Error = result.Item2;
                    e.Cancel();
                }
            }
        }
    }
}
