namespace TajikSpeechRecognition.Model
{
    using System.Data.Entity;
    public class Audio : EntityBase
    {
        public Audio() : base() { }

        public Audio(EntityContext context) : base(context) { }

        string _fileName;
        public string FileName
        {
            get => _fileName;
            set => OnPropertySetting(nameof(FileName), value, ref _fileName);
        }

        Text _text;
        public virtual Text Text
        {
            get => _text;
            set => OnPropertySetting(nameof(Text), value, ref _text);
        }

        Speaker _speaker;
        public virtual Speaker Speaker
        {
            get => _speaker;
            set => OnPropertySetting(nameof(Speaker), value, ref _speaker);
        }

        public override string ToString() => FileName;
    }

    public class AudioConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<Audio>();
            config.ToTable(prefix + "Audios").HasKey(a => a.Guid);
            config.HasRequired(a => a.Speaker).WithMany(s => s.Audios).WillCascadeOnDelete(true);
            config.HasRequired(a => a.Text).WithMany(t => t.Audios).WillCascadeOnDelete(true);
        }
    }
}
