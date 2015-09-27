using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations.Commands
{
	internal class BackupDatabaseCommand : ICommand
	{
		private IBackupManager backupManager;

		internal BackupDatabaseCommand(IBackupManager backupManager)
		{
			this.backupManager = backupManager;
		}

		public OperationResult Execute()
		{
			return backupManager.BackupDatabase(PackageManagerParameters.Instance.InputProperty.SqlConnectionString, PackageManagerWorkspace.CurrentBackupPath);
		}
	}
}
