using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PackageManager.Implementations;
using PackageManager.Models;
using PackageManager.Tasks.Common;

namespace PackageManager.Tasks
{
	internal class CopyTask : BaseTask
	{
		private readonly Copy copy;

		internal CopyTask(Copy copy)
			: base(copy.FailOnError)
		{
			this.copy = copy;
		}

		protected override void ExecuteTask()
		{
			IList<string> copyFiles = new List<string>();
			string destPath = Path.Combine(PackageManagerWorkspace.PackagePath, copy.ToDirect);
			
			if (copy.FileSet != null && (copy.FileSet.Include.Any() || copy.FileSet.Exclude.Any())) 
			{
				copyFiles = ParsingFileSet.GetFilesFromFileSet(copy.FileSet);
			}
			if (!String.IsNullOrEmpty(copy.File))
			{
				string filePath = Path.Combine(PackageManagerWorkspace.PackagePath, copy.File);
				copyFiles.Add(filePath);
			}
			foreach (string path in copyFiles)
			{
				FileManager.CopyFileByPath(path, destPath, true);
			}
		}
	}
}
