using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;
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
        public ObservableCollection<Audio> Audios
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
    }
}
