using System.Windows;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI
{
    public partial class App : Application
    {
        public App()
        {
            UIManager.DataProvider = new DataProvider();
        }
    }
}
