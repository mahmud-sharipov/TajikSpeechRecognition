namespace TajikSpeechRecognition.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    public class Speaker : EntityBase
    {
        public Speaker() : base() { }

        public Speaker(EntityContext context) : base(context) { }

        string _name;
        public string Name
        {
            get => _name;
            set => OnPropertySetting(nameof(Name), value, ref _name);
        }

        int _age;
        public int Age
        {
            get => _age;
            set => OnPropertySetting(nameof(Age), value, ref _age);
        }

        bool _isMale;
        public bool IsMale
        {
            get => _isMale;
            set => OnPropertySetting(nameof(IsMale), value, ref _isMale);
        }

        private ObservableCollection<Audio> _audios = new ObservableCollection<Audio>();
        public virtual ObservableCollection<Audio> Audios
        {
            get => _audios;
            set => OnPropertySetting(nameof(Audios), value, ref _audios);
        }

        public override string ToString() => Name;
    }

    public class SpeakerConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<Speaker>();
            config.ToTable(prefix + "Speakers").HasKey(s => s.Guid);
            config.HasMany(t => t.Audios).WithRequired(s => s.Speaker).WillCascadeOnDelete(true);
        }
    }
}