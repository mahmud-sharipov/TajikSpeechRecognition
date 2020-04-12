using System.Windows;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Services;

namespace TajikSpeechRecognition.UI
{
    public partial class App : Application
    {
        public App()
        {
            UIManager.MainDispatcher = Dispatcher;
            UIManager.DataProvider = new DataProvider();
            UIManager.LogManager = new LogManager();
            UIManager.ModelBuilder = new ModelBuilder(UIManager.DataProvider, $"{AppManager.TempDir}/tajik.lm.DMP"); ;
        }
    }
}
