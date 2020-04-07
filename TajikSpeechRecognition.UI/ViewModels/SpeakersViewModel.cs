using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Pages;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class SpeakersViewModel : BaseViewModel
    {
        public SpeakersViewModel()
        {
            Speakers = new ObservableCollection<Speaker>(DataProvider.GetEntities<Speaker>().ToList());
        }

        private ObservableCollection<Speaker> _speaker;
        public virtual ObservableCollection<Speaker> Speakers
        {
            get => _speaker;
            set
            {
                _speaker = value;
                RaisePropertyChanged();
            }
        }

        Speaker _selectedSpeaker;
        public Speaker SelectedSpeaker
        {
            get => _selectedSpeaker;
            set
            {
                _selectedSpeaker = value;
                RaisePropertyChanged();
            }
        }

        public ICommand DeleteAudio => new Command(a =>
        {
            var audio = a as Audio;
            if (audio != null)
            {
                DataProvider.Delete(audio);
                SelectedSpeaker.Audios.Remove(audio);
                DataProvider.SaveChanges();
            }
        });

        #region Add new speaker
        public ICommand AddNewSpeaker => new Command(AddSpeaker);

        public Speaker NewSpeaker { get; set; }

        private async void AddSpeaker(object o)
        {
            var viewModel = new SpeakerViewModel();
            NewSpeaker = viewModel.Speaker;
            await DialogHost.Show(new SpeakerPage() { DataContext = viewModel }, DialodIdentifiers.EntireWindow, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false)
            {
                NewSpeaker = null;
            }
            else
            {
                DataProvider.Add(NewSpeaker);
                DataProvider.SaveChanges();
                Speakers.Add(NewSpeaker);
                SelectedSpeaker = NewSpeaker;
                NewSpeaker = null;
            }
            //eventArgs.Cancel();
        }
        #endregion
    }
}
