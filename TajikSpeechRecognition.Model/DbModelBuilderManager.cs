using System.Data.Entity;

namespace TajikSpeechRecognition.Model
{
    public static class DbModelBuilderManager
    {
        public static string ModelPrefix => "";
        public static string UIPrefix => "";
        public static void BuildModels(DbModelBuilder modelBuilder)
        {
            EntityBaseConfiguration.Configure(modelBuilder, ModelPrefix);

            WordConfiguration.Configure(modelBuilder, ModelPrefix);
            SpeakerConfiguration.Configure(modelBuilder, ModelPrefix);
            TextConfiguration.Configure(modelBuilder, ModelPrefix);
            AudioConfiguration.Configure(modelBuilder, ModelPrefix);
        }
    }
}
