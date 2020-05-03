using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.ViewModels;

namespace TajikSpeechRecognition.UI.Pages
{
    public partial class RecognizePage : BasePage
    {
        public RecognizePage()
        {
            InitializeComponent();
        }

        private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((RecognizeViewModel)DataContext).StartRecording();
        }

        private void Button_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((RecognizeViewModel)DataContext).StopRecording();
        }
    }
}
