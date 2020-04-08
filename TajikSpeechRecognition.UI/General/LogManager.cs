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
            Logs = new ObservableCollection<LogItem>()
            {
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
                    new LogItem("dfbdfb;dlkfhb;dflhbdlf;hbd;fhbdfb;hlzdf;blkhdf;bzkhf;zbhdf;hkbz;dfhbd;fhb;zldfhb;zdfbhid;fb difhboidfhb dfoihbdifhbdoifhbdoifhbdoifbhiodfhb;dfbkdfbldfhb;dfhbldkfjbhd;fhbzhbldfbdfnb.,dfbn.df,bndflb", LogType.Success),
            };
        }

        private ObservableCollection<LogItem> logs;
        public ObservableCollection<LogItem> Logs
        {
            get => logs;
            set
            {
                logs = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CleareLogs => new Command(o => Logs.Clear());

        public void Log(string logValue, LogType type) => Logs.Add(new LogItem(logValue, type));

        public void LogInfo(string logValue) => Logs.Add(new LogItem(logValue, LogType.Information));

        public void LogSuccess(string logValue) => Logs.Add(new LogItem(logValue, LogType.Success));

        public void LogWarning(string logValue) => Logs.Add(new LogItem(logValue, LogType.Warning));

        public void LogError(string logValue) => Logs.Add(new LogItem(logValue, LogType.Error));

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
