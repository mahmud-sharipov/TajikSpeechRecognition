using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Pages;
using TajikSpeechRecognition.UI.Services;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class AudiosViewModel : BaseViewModel
    {
        public AudiosViewModel()
        {
            Audios = new ObservableCollection<Audio>(DataProvider.GetEntities<Audio>().ToList());
        }

        private ObservableCollection<Audio> _Audios;
        public virtual ObservableCollection<Audio> Audios
        {
            get => _Audios;
            set
            {
                _Audios = value;
                RaisePropertyChanged();
            }
        }

        Audio _selectedAudio;
        public Audio SelectedAudio
        {
            get => _selectedAudio;
            set
            {
                _selectedAudio = value;
                RaisePropertyChanged();
            }
        }

        public ICommand DeleteAudio => new Command(a =>
        {
            var audio = a as Audio;
            if (audio != null)
            {
                AudioService.RemoveAudio(audio, DataProvider);
                Audios.Remove(audio);
            }
        });

        public ICommand AddNewAudio => new Command(AddAudio);

        private async void AddAudio(object o)
        {
            var viewModel = new AudioViewModel(Audios);
            await DialogHost.Show(new AudioPage() { DataContext = viewModel }, DialodIdentifiers.EntireWindow, AudioViewModel.ClosingEventHandler);
        }
    }
}
