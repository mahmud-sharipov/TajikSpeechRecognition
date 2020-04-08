using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TajikSpeechRecognition.UI.General
{
    public class LogItem : INotifyPropertyChanged
    {
        public LogItem(string log, LogType type)
        {
            Log = log;
            Type = type;
        }

        private LogType type;

        public LogType Type
        {
            get { return type; }
            set { type = value; RaisePropertyChanged(); }
        }

        private string log;

        public string Log
        {
            get { return log; }
            set { log = value; RaisePropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
