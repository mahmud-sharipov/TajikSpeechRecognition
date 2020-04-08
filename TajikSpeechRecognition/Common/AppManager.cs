using System.IO;

namespace TajikSpeechRecognition
{
    public static class AppManager
    {
        public static string CurrentDir => Directory.GetCurrentDirectory().Replace(@"\", "/");

        public static string SphinxDir { get; set; } = CurrentDir + "/sphinx";

        public static string OutputDir { get; set; } = CurrentDir + "/Models";

        public static string TempDir { get; set; } = CurrentDir + "/TEMP";

        public static string ModelName { get; set; } = "tajik";

        public static string AudiosDir { get; set; } = CurrentDir + "/Audios";

    }
}
