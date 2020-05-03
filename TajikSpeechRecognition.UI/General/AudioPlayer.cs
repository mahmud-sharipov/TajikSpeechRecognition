using System.Media;
using System.Windows.Input;
using TajikSpeechRecognition.Core;
using TajikSpeechRecognition.Model;

namespace TajikSpeechRecognition.UI.General
{
    public static class AudioPlayer
    {
        private static SoundPlayer SoundPlayer { get; set; }

        public static ICommand Play => new Command(PlayAudio);

        public static void PlayAudio(object o)
        {
            var audio = o as Audio;
            if (audio != null)
                PlayAudio(audio, $"{AppManager.AudiosDir}/{audio.FileName}.wav");
        }

        public static void PlayAudio(Audio audio, string audioDir)
        {
            if (SoundPlayer != null)
            {
                SoundPlayer.Stop();
                SoundPlayer.Dispose();
            }
            SoundPlayer = new SoundPlayer($"{audioDir}/{audio.FileName}.wav");
            SoundPlayer.Load();
            SoundPlayer.Play();
        }
    }
}
