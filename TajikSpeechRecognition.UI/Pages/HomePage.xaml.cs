using System.Net.Http;
using System.Text;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Services;

namespace TajikSpeechRecognition.UI.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class HomePage : BasePage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //UIManager.ModelBuilder.Upload();
            LM.Post();
        }
    }
}
