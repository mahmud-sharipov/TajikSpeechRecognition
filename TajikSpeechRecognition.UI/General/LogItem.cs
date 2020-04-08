using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajikSpeechRecognition.UI.General
{
    public class LogItem
    {
        public LogItem(string log, LogType type)
        {
            Log = log;
            Type = type;
        }

        public LogType Type { get; set; }

        public string Log { get; set; }
    }
}
