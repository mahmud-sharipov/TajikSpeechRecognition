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

        private void BasePage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.R)
                ((AudioViewModel)DataContext).StartRecording();
        }

        private void BasePage_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.R)
                ((AudioViewModel)DataContext).StopRecording();
            else if (e.Key == System.Windows.Input.Key.N)
                ((AudioViewModel)DataContext).SaveAndContinue(null);
        }
    }
}
