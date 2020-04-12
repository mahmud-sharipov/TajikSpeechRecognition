using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TajikSpeechRecognition.UI.General
{
    public class LogManager : INotifyPropertyChanged
    {
        public LogManager()
        {
            Logs = new ObservableCollection<LogItem>();
        }

        private static ObservableCollection<LogItem> logs;
        public static ObservableCollection<LogItem> Logs
        {
            get => logs;
            set
            {
                logs = value;
            }
        }

        public ICommand CleareLogs => new Command(o =>
        {
            Logs.Clear();
            RaisePropertyChanged(nameof(Logs));
        });

        public void Log(string logValue, LogType type) => UIManager.MainDispatcher?.Invoke(() => Logs.Add(new LogItem(logValue, type)));

        public void LogInfo(string logValue) => Log(logValue, LogType.Information);

        public void LogSuccess(string logValue) => Log(logValue, LogType.Success);

        public void LogWarning(string logValue) => Log(logValue, LogType.Warning);

        public void LogError(string logValue) => Log(logValue, LogType.Error);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
