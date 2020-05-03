using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Pages;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {

        }

        public ICommand Recognize => new Command(RecognizeText);

        private async void RecognizeText(object o)
        {
            var viewModel = new RecognizeViewModel();
            await DialogHost.Show(new RecognizePage() { DataContext = viewModel }, DialodIdentifiers.EntireWindow);
        }
    }
}
