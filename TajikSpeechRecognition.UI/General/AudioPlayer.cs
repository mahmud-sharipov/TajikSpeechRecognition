using Microsoft.Win32;
using System;
using System.Media;
using System.Windows.Input;
using System.Windows.Media;
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
            {
                if (SoundPlayer != null)
                {
                    SoundPlayer.Stop();
                    SoundPlayer.Dispose();
                }
                SoundPlayer = new SoundPlayer($"{AppManager.AudiosDir}/{audio.FileName}.wav");
                SoundPlayer.Load();
                SoundPlayer.Play();

            }
        }
    }
}
