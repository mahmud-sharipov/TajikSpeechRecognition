using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.Controls
{
    public class LogBaseControl : UserControl, INotifyPropertyChanged
    {
        public LogBaseControl()
        {
            FontColor = Brushes.Red;
            Icon = new PackIcon() { Width = 20, Height = 20 };
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

        #region Log
        public object Log
        {
            get { return (object)GetValue(LogProperty); }
            set { SetValue(LogProperty, value); }
        }
        public static readonly DependencyProperty LogProperty =
            DependencyProperty.Register("Log", typeof(object), typeof(LogBaseControl), new PropertyMetadata("LOG", LogChangedHandler));
        #endregion

        #region Value
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(LogBaseControl), new UIPropertyMetadata("", LogValueChangedHandler));
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static void LogChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (LogBaseControl)d;
            var log = e.NewValue as LogItem;
            if (log == null) return;
            switch (log.Type)
            {
                case LogType.Information:
                    control.Icon.Kind = PackIconKind.InfoOutline;
                    control.Icon.Foreground = Brushes.DeepSkyBlue;
                    control.FontColor = Brushes.Black;
                    break;
                case LogType.Success:
                    control.Icon.Kind = PackIconKind.CheckboxMarkedOutline;
                    control.Icon.Foreground = Brushes.Green;
                    control.FontColor = Brushes.Green;
                    break;
                case LogType.Warning:
                    control.Icon.Kind = PackIconKind.WarningOutline;
                    control.Icon.Foreground = Brushes.Orange;
                    control.FontColor = Brushes.Orange;
                    break;
                case LogType.Error:
                    control.Icon.Kind = PackIconKind.CloseOctagonOutline;
                    control.Icon.Foreground = Brushes.Red;
                    control.FontColor = Brushes.Red;
                    break;
                default:
                    control.Icon.Kind = PackIconKind.InfoOutline;
                    control.Icon.Foreground = Brushes.DeepSkyBlue;
                    control.FontColor = Brushes.Black;
                    break;
            }
        }

        public static void LogValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
