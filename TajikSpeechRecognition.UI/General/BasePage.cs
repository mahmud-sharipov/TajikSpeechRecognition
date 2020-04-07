using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace TajikSpeechRecognition.UI.General
{
    public class BasePage : UserControl, INotifyPropertyChanged
    {
        public BaseViewModel ViewModel => DataContext as BaseViewModel;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
