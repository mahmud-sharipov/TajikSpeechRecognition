using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Pages;
using TajikSpeechRecognition.UI.ViewModels;

namespace TajikSpeechRecognition.UI.Services
{
    public static class AudioService
    {
        public static Tuple<bool, string> SaveAudio(Audio audio, DataProvider provider)
        {
            if (!File.Exists($"{AppManager.AudiosDir}/{audio.FileName}.wav"))
                return new Tuple<bool, string>(false, "Audio doesn't recorded");
            else if (audio.Speaker == null && audio.Text == null)
                return new Tuple<bool, string>(false, "Speaker and text are required");
            else if (audio.Speaker == null)
                return new Tuple<bool, string>(false, "Speaker is required");
            else if (audio.Text == null)
                return new Tuple<bool, string>(false, "Text is required");

            provider.Add(audio);
            provider.SaveChanges();
            return new Tuple<bool, string>(true, "");
        }

        public static void RemoveAudio(Audio audio, DataProvider provider)
        {
            provider.Delete(audio);
            provider.SaveChanges();
            File.Delete($"{AppManager.AudiosDir}/{audio.FileName}.wav");
        }

        public static ICommand AddNewAudio => new Command(AddAudio);

        private static async void AddAudio(object o)
        {
            if (o == null) return;

            AudioViewModel viewModel = null;
            if (o is Speaker speaker)
            {
                viewModel = new AudioViewModel(null);
                viewModel.Audio.Speaker = speaker;
            }
            else if (o is Text text)
            {
                viewModel = new AudioViewModel(null);
                viewModel.Audio.Text = text;
            }
            else
                viewModel = new AudioViewModel(o as ObservableCollection<Audio>);

            await DialogHost.Show(new AudioPage() { DataContext = viewModel }, DialodIdentifiers.EntireWindow, AudioViewModel.ClosingEventHandler);
        }
    }
}
