using System.Threading.Tasks;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.ViewModels;

namespace TajikSpeechRecognition.UI.Pages
{
    public partial class AudioPage : BasePage
    {
        public AudioPage()
        {
            InitializeComponent();
        }

        private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((AudioViewModel)DataContext).StartRecording();
        }

        private void Button_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((AudioViewModel)DataContext).StopRecording();
        }
    }
}
