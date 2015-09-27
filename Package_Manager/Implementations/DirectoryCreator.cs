using System.Collections.Generic;
using System.IO;
using PackageManager.Extensions;
using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations
{
	internal class DirectoryCreator : IDirectoryCreator
	{
		internal DirectoryCreator() { }

		private readonly IList<string> directoriesList = new List<string>()
		{
			PackageManagerWorkspace.PackageManager, 
			PackageManagerWorkspace.PackagePath,
			PackageManagerWorkspace.BackupFolderPath,
			PackageManagerWorkspace.LogsPath,
			PackageManagerWorkspace.NextBackupFolder,
		};

		public OperationResult CreateDirectories()
		{
			Logger.Instance.WriteLog.Info("CreateDirectories started");
			OperationResult result = new OperationResult() {Status = StatusResult.Success};
			try
			{
				foreach (string directoryPath in directoriesList)
				{
					FileManager.CreateDirectoryIfNotExists(directoryPath);
				}
				Logger.Instance.WriteLog.Info("CreateDirectories finished");
			}
			catch (IOException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}
	}
}
