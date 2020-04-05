using System.Windows;
using TajikSpeechRecognition.Model;

namespace TajikSpeechRecognition.Dialogs
{

    public partial class TextEditor : Window
    {
        public Text Text{ get; set; }

        public bool ShouldAdd { get; set; } = false;

        public TextEditor()
        {
            Text = new Text();
            InitializeComponent();
            DataContext = Text;
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
