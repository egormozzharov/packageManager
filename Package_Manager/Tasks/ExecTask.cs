using System;
using System.Diagnostics;
using System.IO;
using PackageManager.Implementations;
using PackageManager.Models;
using PackageManager.Tasks.Common;

namespace PackageManager.Tasks
{
	internal class ExecTask : BaseTask
	{
		private readonly Exec exec;
		private static string outputPathForExec = string.Empty;

		public ExecTask(Exec exec) : base(exec.FailOnError)
		{
			this.exec = exec;
		}

		protected override void ExecuteTask()
		{
			outputPathForExec = exec.Output;
			FileManager.CreateFileIfNotExists(exec.Output);
			using (Process process = new Process())
			{
				if (String.IsNullOrEmpty(exec.WorkingDirect))
				{
					process.StartInfo.WorkingDirectory = PackageManagerWorkspace.PackagePath;
				}
				else
				{
					FileManager.CreateDirectoryIfNotExists(exec.WorkingDirect);
					process.StartInfo.WorkingDirectory = exec.WorkingDirect;
				}
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.FileName = string.IsNullOrEmpty(exec.BaseDirectory)
					? exec.Program
					: Path.Combine(PackageManagerWorkspace.PackagePath, exec.BaseDirectory, exec.Program);
				;
				process.StartInfo.Arguments = exec.CommandLine;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
				process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
				process.Start();
				if (exec.Timeout > 0)
				{
					process.WaitForExit(exec.Timeout);
					if (process.HasExited == false)
					{
						if (process.Responding)
							process.CloseMainWindow();
						else
							process.Kill();

						throw new TimeoutException("TimeOut program execution: " + exec.Program);
					}
				}
				else
				{
					process.WaitForExit();
				}
			}
		}

		private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
		{
			using (StreamWriter writer = new StreamWriter(outputPathForExec, true))
			{
				writer.WriteLine(DateTime.Now + ": " + outLine.Data);
			}
		}
	}
}
