using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;

namespace TajikSpeechRecognition.UI.Services
{
    public static class AudioService
    {
        public static Tuple<bool, string> SaveAudio(Audio audio, DataProvider provider)
        {
            if (!File.Exists($"{AppManager.AudiosDir}/{audio.FileName}.wav"))
                return new Tuple<bool, string>(false, "Audio doesn't recorded");
            else if (audio.Speaker == null && audio.Text == null)
                return new Tuple<bool, string>(false, "Speaker and text are required");
            else if (audio.Speaker == null)
                return new Tuple<bool, string>(false, "Speaker is required");
            else if (audio.Text == null)
                return new Tuple<bool, string>(false, "Text is required");

            provider.Add(audio);
            provider.SaveChanges();
            return new Tuple<bool, string>(true, "");
        }

        public static void RemoveAudio(Audio audio, DataProvider provider)
        {
            provider.Delete(audio);
            provider.SaveChanges();
            File.Delete($"{AppManager.AudiosDir}/{audio.FileName}.wav");
        }
    }
}
