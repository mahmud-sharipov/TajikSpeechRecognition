using System.ComponentModel;
using System.Runtime.CompilerServices;
using TajikSpeechRecognition.Model;

namespace TajikSpeechRecognition.UI.General
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public DataProvider DataProvider { get; set; } = UIManager.DataProvider;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
