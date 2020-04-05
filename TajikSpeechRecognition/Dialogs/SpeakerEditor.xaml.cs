using System.Windows;
using TajikSpeechRecognition.Model;

namespace TajikSpeechRecognition.Dialogs
{
    /// <summary>
    /// Interaction logic for SpeakerEditor.xaml
    /// </summary>
    public partial class SpeakerEditor : Window
    {
        public Speaker Speaker { get; set; }

        public bool ShouldAdd { get; set; } = false;

        public SpeakerEditor()
        {
            Speaker = new Speaker();
            InitializeComponent();
            DataContext = Speaker;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ShouldAdd = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
