using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations.Commands
{
	internal class CreateDirectoriesCommand : ICommand
	{
		private IDirectoryCreator directoryCreator;

		internal CreateDirectoriesCommand(IDirectoryCreator directoryCreator)
		{
			this.directoryCreator = directoryCreator;
		}

		public OperationResult Execute()
		{
			return directoryCreator.CreateDirectories();
		}
	}
}
