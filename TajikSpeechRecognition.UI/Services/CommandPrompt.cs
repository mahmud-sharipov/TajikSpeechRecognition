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
        internal static Process ActiveProcess { get; set; }

        public static void Run(string script)
        {
            try
            {
                var process = CreateNewProcess(script, null);
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

        public static void Run(string script, Action<string> onDataReceived)
        {
            try
            {
                UIManager.MainDispatcher.Invoke(() => ActiveProcess = CreateNewProcess(script, onDataReceived));
                ActiveProcess.Start();
                ActiveProcess.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                UIManager.LogManager.LogError(ex.Message);
            }
        }

        static Process CreateNewProcess(string script, Action<string> onDataReceived = null)
        {
            var process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = script;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
          //  startInfo.WindowStyle= ProcessWindowStyle.Normal;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo = startInfo;
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (string.IsNullOrEmpty(e.Data))
                    return;
                UIManager.LogManager.LogInfo(e.Data);
                onDataReceived?.Invoke(e.Data);
            });
            process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (string.IsNullOrEmpty(e.Data))
                    return;
                UIManager.LogManager.LogError(e.Data);
                onDataReceived?.Invoke(e.Data);
            });
            return process;
        }
    }
}
