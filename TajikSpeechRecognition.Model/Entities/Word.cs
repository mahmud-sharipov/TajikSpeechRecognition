using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajikSpeechRecognition.Model
{
    public class Word : EntityBase
    {
        public Word() : base() { }

        public Word(EntityContext context) : base(context) { }

        string _value;
        public string Value
        {
            get => _value;
            set => OnPropertySetting(nameof(Value), value, ref _value);
        }

        string _phonemes;
        public string Phonemes
        {
            get => _phonemes;
            set => OnPropertySetting(nameof(Phonemes), value, ref _phonemes);
        }

        public override string ToString() => Value;
    }

    public class WordConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<Word>();
            config.ToTable(prefix + "Word").HasKey(a => a.Guid);

        }
    }
}
