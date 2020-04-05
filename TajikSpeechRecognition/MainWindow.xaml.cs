using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TajikSpeechRecognition.Model;

namespace TajikSpeechRecognition
{
    public partial class MainWindow : Window
    {
        MainViewModel Model;
        public MainWindow()
        {
            InitializeComponent();
            Model = new MainViewModel();
            Model.Log = s => this.Dispatcher.Invoke(() =>
            {
                Logger.AppendText(s + "\r\n");
                Logger.ScrollToEnd();
            });
            DataContext = Model;
        }

        private void BuildModel_Click(object sender, RoutedEventArgs e)
        {
            Model.BuildModel($"{AppManager.TempDir}/tajik.lm.DMP");
        }

        private void AddNewSpeaker_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.SpeakerEditor();
            dialog.ShowDialog();
            if (dialog.ShouldAdd)
                Model.AddSpeaker(dialog.Speaker);
        }

        private void AddNewText_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.TextEditor();
            dialog.ShowDialog();
            if (dialog.ShouldAdd)
                Model.AddText(dialog.Text);
        }

        private void RunModel_Click(object sender, RoutedEventArgs e)
        {
            Model.RunModel();
        }
    }
}
