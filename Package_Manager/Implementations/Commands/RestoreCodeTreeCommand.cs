using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations.Commands
{
	internal class RestoreCodeTreeCommand : ICommand
	{
		private IBackupManager backupManager;

		internal RestoreCodeTreeCommand(IBackupManager backupManager)
		{
			this.backupManager = backupManager;
		}

		public OperationResult Execute()
		{
			return backupManager.RestoreCodeTree();
		}
	}
}
