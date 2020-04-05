using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
