using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.Controls
{
    /// <summary>
    /// Interaction logic for LogViewerControl.xaml
    /// </summary>
    public partial class LogViewerControl : UserControl, INotifyPropertyChanged
    {
        public LogViewerControl()
        {
            UIManager.LogManager.PropertyChanged += LogManager_PropertyChanged;
            DataContext = this;
            InitializeComponent();
        }

        private void LogManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LogManager.Logs))
                RaisePropertyChanged(nameof(Logs));
        }

        private string logSearchText = "";
        public string LogSearchText
        {   
            get => logSearchText;
            set
            {
                logSearchText = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Cleare => UIManager.LogManager.CleareLogs;

        public IEnumerable<LogItem> Logs
            => UIManager.LogManager.Logs.Where(l => (logSearchText == "" ? true : l.Log.ToLower().Contains(logSearchText.ToLower())));

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Scrol.ScrollToEnd();
        }
    }
}
