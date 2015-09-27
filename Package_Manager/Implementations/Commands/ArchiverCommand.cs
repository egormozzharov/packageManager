using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations.Commands
{
	internal class DecompressZipCommand : ICommand
	{
		private IArchiver archiver;

		internal DecompressZipCommand(IArchiver archiver)
		{
			this.archiver = archiver;
		}

		public OperationResult Execute()
		{
			return archiver.DecompressZip(PackageManagerWorkspace.PackageZipPath, false);
		}
	}
}
