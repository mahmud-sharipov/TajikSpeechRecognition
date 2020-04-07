using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using TajikSpeechRecognition.UI.Pages;

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
                DataProvider.Delete(audio);
                Audios.Remove(audio);
                DataProvider.SaveChanges();
            }
        });

        #region Add new Audio
        public ICommand AddNewAudio => new Command(AddAudio);

        public Audio NewAudio { get; set; }

        private async void AddAudio(object o)
        {
            var viewModel = new AudioViewModel();
            NewAudio = viewModel.Audio;
            await DialogHost.Show(new TextPage() { DataContext = viewModel }, DialodIdentifiers.EntireWindow, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false)
                NewAudio = null;
            else
            {
                DataProvider.Add(NewAudio);
                DataProvider.SaveChanges();
                Audios.Add(NewAudio);
                SelectedAudio = NewAudio;
                NewAudio = null;
                DataProvider.SaveChanges();
            }
            //eventArgs.Cancel();
        }

        protected string GetWordPhonemes(string word)
        {
            var phonemes = "";
            foreach (var char_ in word)
                phonemes += Core.Phonemes.List[char_.ToString()] + " ";

            return phonemes.Trim();
        }
        #endregion
    }
}
