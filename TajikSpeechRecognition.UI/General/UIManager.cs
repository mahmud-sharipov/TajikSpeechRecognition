using System.Windows.Threading;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.Services;

namespace TajikSpeechRecognition.UI.General
{
    public class UIManager
    {
        public static DataProvider DataProvider { get; set; }

        public static LogManager LogManager { get; set; }

        public static ModelBuilder ModelBuilder { get; set; }

        public static ModelManager ModelManager { get; set; }

        public static Dispatcher MainDispatcher { get; set; }
    }
}
