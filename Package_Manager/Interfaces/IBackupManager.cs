using System;
using PackageManager.Models;

namespace PackageManager.Interfaces
{
	internal interface IBackupManager
	{
		OperationResult BackUpCodeTree();
		OperationResult RestoreCodeTree();
		OperationResult BackupDatabase(string sqlConnectionString, string destinationPath);
		OperationResult RestoreDatabase(string sqlConnectionString, String backUpFile);
	}
}
