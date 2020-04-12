using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TajikSpeechRecognition.UI.General;

namespace TajikSpeechRecognition.UI.Services
{
    public static class CommandPrompt
    {
        public static void Run(string script)
        {
            try
            {
                var process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = script;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo = startInfo;
                process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        UIManager.LogManager.LogInfo(e.Data);
                });
                process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        UIManager.LogManager.LogError(e.Data);
                });
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }
    }
}
