using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    public partial class LogViewerControl : UserControl
    {
        public LogViewerControl()
        {
            DataContext = this;
            InitializeComponent();
            LogManager.Logs.CollectionChanged += Logs_CollectionChanged;
        }

        private void Logs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                LogList.SelectedIndex = LogList.Items.Count - 1;
                LogList.ScrollIntoView(LogList.SelectedItem);
            });
        }

        public ICommand Cleare => UIManager.LogManager.CleareLogs;
    }
}
