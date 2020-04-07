using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;
using TajikSpeechRecognition.UI.General;
using TajikSpeechRecognition.UI.Pages;
using TajikSpeechRecognition.UI.Services;

namespace TajikSpeechRecognition.UI.ViewModels
{
    public class TextsViewModel : BaseViewModel
    {
        public TextsViewModel()
        {
            Texts = new ObservableCollection<Text>(DataProvider.GetEntities<Text>().ToList());
        }

        private ObservableCollection<Text> _texts;
        public virtual ObservableCollection<Text> Texts
        {
            get => _texts;
            set
            {
                _texts = value;
                RaisePropertyChanged();
            }
        }

        Text _selectedText;
        public Text SelectedText
        {
            get => _selectedText;
            set
            {
                _selectedText = value;
                RaisePropertyChanged();
            }
        }

        public ICommand DeleteAudio => new Command(a =>
        {
            var audio = a as Audio;
            if (audio != null)
            {
                AudioService.RemoveAudio(audio, DataProvider);
                SelectedText.Audios.Remove(audio);
            }
        });

        #region Add new text
        public ICommand AddNewText => new Command(AddText);

        public Text NewText { get; set; }

        private async void AddText(object o)
        {
            var viewModel = new TextViewModel();
            NewText = viewModel.Text;
            await DialogHost.Show(new TextPage() { DataContext = viewModel }, DialodIdentifiers.EntireWindow, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false)
                NewText = null;
            else
            {
                DataProvider.Add(NewText);
                Texts.Add(NewText);
                SelectedText = NewText;
                var words = DataProvider.GetEntities<Word>();
                NewText.Value.Split(' ').Where(w => w != "").ToList().ForEach(word =>
                    {
                        if (!words.Any(w => w.Value == word))
                        {
                            var newWord = new Word()
                            {
                                Value = word,
                                Phonemes = GetWordPhonemes(word)
                            };
                            DataProvider.Add(newWord);
                        }
                    });
                NewText = null;
                DataProvider.SaveChanges();
            }
            //eventArgs.Cancel();
        }

        protected string GetWordPhonemes(string word)
        {
            var phonemes = "";
            foreach (var char_ in word)
                phonemes += Core.Phonemes.List[char_.ToString()] + " ";

            return phonemes.Trim();
        }
        #endregion
    }
}
