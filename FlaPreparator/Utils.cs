using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace BTXTTool {
  class Utils	{
    public static void RunCommand(string filepath, String arguments) {
        Process process = new Process();
        process.StartInfo.FileName = System.Environment.CurrentDirectory + "\\" + filepath;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.WorkingDirectory = System.Environment.CurrentDirectory;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        process.WaitForExit();
        var exitCode = process.ExitCode;
    }
  }
}
