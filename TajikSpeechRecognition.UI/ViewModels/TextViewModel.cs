using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class TextViewModel : BaseViewModel
    {
        public TextViewModel()
        {
            Text = new Text();
        }

        public TextViewModel(Text text)
        {
            Text = text;
        }

        Text _speaker;
        public Text Text
        {
            get => _speaker;
            set
            {
                _speaker = value;
                RaisePropertyChanged();
            }
        }
    }
}
