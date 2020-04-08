using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.Controls
{
    public partial class LogControl : UserControl, INotifyPropertyChanged
    {
        public LogControl()
        {
            FontColor = Brushes.Red;
            Icon = new PackIcon();
            DataContext = this;
            InitializeComponent();
        }

        private Brush fontColor;
        public Brush FontColor
        {
            get => fontColor;
            set
            {
                fontColor = value;
                RaisePropertyChanged();
            }
        }

        private PackIcon icon;
        public PackIcon Icon
        {
            get => icon;
            set
            {
                icon = value;
                RaisePropertyChanged();
            }
        }

        private LogItem logItem;
        public LogItem LogItem
        {
            get => logItem;
            set
            {
                logItem = value;
                RaisePropertyChanged();
            }
        }

        public LogType LogType
        {
            get { return (LogType)GetValue(LogTypeProperty); }
            set { SetValue(LogTypeProperty, value); }
        }

        public static readonly DependencyProperty LogTypeProperty =
            DependencyProperty.Register(
                "LogType",
                typeof(LogType),
                typeof(LogControl),
                new PropertyMetadata(LogTypeChangedHandler));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(string),
                typeof(LogControl),
                new FrameworkPropertyMetadata("", LogValueChangedHandler));

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static void LogTypeChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static void LogValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        protected void InitInfo(LogItem log)
        {
            LogItem = log;
            switch (log.Type)
            {
                case LogType.Information:
                    Icon.Kind = PackIconKind.InfoOutline;
                    Icon.Foreground = Brushes.LightBlue;
                    FontColor = Brushes.Black;
                    break;
                case LogType.Success:
                    Icon.Kind = PackIconKind.CheckboxMarkedOutline;
                    Icon.Foreground = Brushes.Green;
                    FontColor = Brushes.Green;
                    break;
                case LogType.Warning:
                    Icon.Kind = PackIconKind.WarningOutline;
                    Icon.Foreground = Brushes.Orange;
                    FontColor = Brushes.Orange;
                    break;
                case LogType.Error:
                    Icon.Kind = PackIconKind.CloseOctagonOutline;
                    Icon.Foreground = Brushes.Red;
                    FontColor = Brushes.Red;
                    break;
                default:
                    Icon.Kind = PackIconKind.InfoOutline;
                    Icon.Foreground = Brushes.LightBlue;
                    FontColor = Brushes.Black;
                    break;
            }
        }
    }
}
