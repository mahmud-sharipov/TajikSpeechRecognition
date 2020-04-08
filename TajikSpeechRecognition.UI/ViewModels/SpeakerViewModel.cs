using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class SpeakerViewModel : BaseViewModel
    {
        public SpeakerViewModel()
        {
            Speaker = new Speaker();
        }

        public SpeakerViewModel(Speaker speaker)
        {
            Speaker = speaker;
        }

        Speaker _speaker;
        public Speaker Speaker
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
