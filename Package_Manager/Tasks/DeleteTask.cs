using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PackageManager.Implementations;
using PackageManager.Models;
using PackageManager.Tasks.Common;

namespace PackageManager.Tasks
{
	internal class DeleteTask : BaseTask
	{
		private readonly Delete delete;

		internal DeleteTask(Delete delete)
			: base(delete.FailOnError)
		{
			this.delete = delete;
		}

		protected override void ExecuteTask()
		{
			IList<string> deleteFiles = new List<string>();
			if (delete.FileSet != null && (delete.FileSet.Include.Any() || delete.FileSet.Exclude.Any()))
			{
				deleteFiles = ParsingFileSet.GetFilesFromFileSet(delete.FileSet);
			}
			if (!String.IsNullOrEmpty(delete.File))
			{
				string filePath = Path.Combine(PackageManagerWorkspace.PackagePath, delete.File);
				deleteFiles.Add(filePath);
			}
			foreach (string path in deleteFiles)
			{
				FileManager.DeleteFile(path);
			}
		}
	}
}
