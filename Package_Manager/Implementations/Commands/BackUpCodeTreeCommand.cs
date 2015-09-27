using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations.Commands
{
	internal class BackUpCodeTreeCommand : ICommand
	{
		private IBackupManager backupManager;

		internal BackUpCodeTreeCommand(IBackupManager backupManager)
		{
			this.backupManager = backupManager;
		}

		public OperationResult Execute()
		{
			return backupManager.BackUpCodeTree();
		}
	}
}
