using System.Windows.Input;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {

        }

        public ICommand BuildMidel => UIManager.ModelBuilder.Build;
    }
}
