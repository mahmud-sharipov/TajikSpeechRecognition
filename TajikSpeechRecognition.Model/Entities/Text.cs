namespace TajikSpeechRecognition.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;

    public class Text : EntityBase
    {
        public Text() : base() { }

        public Text(EntityContext context) : base(context) { }

        string _value;
        public string Value
        {
            get => _value;
            set => OnPropertySetting(nameof(Value), CleanText(value.ToLower()), ref _value);
        }

        string _id;
        public string Id
        {
            get => _id;
            set => OnPropertySetting(nameof(Id), value, ref _id);
        }

        int _expectedTime;
        public int ExpectedTime
        {
            get => _expectedTime;
            set => OnPropertySetting(nameof(ExpectedTime), value, ref _expectedTime);
        }

        string _description;
        public string Description
        {
            get => _description;
            set => OnPropertySetting(nameof(Description), value, ref _description);
        }

        ObservableCollection<Audio> _audios;
        public virtual ObservableCollection<Audio> Audios
        {
            get => _audios;
            set => OnPropertySetting(nameof(Audios), value, ref _audios);
        }

        string CleanText(string text)
        {
            var newText = "";
            foreach (var char_ in text)
            {
                if (char_ == ' ' || Core.Phonemes.List.ContainsKey(char_.ToString()))
                    newText += char_.ToString();
            }

            return newText.Trim().Replace("  ", " ");
        }

        public override string ToString() => Value;
    }

    public class TextConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<Text>();
            config.ToTable(prefix + "Texts").HasKey(t => t.Guid);
            config.HasMany(t => t.Audios).WithRequired(a => a.Text).WillCascadeOnDelete(true);
        }
    }
}
