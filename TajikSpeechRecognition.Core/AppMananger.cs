using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajikSpeechRecognition.Core
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
