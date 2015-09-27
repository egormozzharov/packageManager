using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations.Commands
{
	internal class CheckStructureCommand : ICommand
	{
		private IPackageStructure packageStructure;

		internal CheckStructureCommand(IPackageStructure packageStructure)
		{
			this.packageStructure = packageStructure;
		}

		public OperationResult Execute()
		{
			return packageStructure.CheckStructure();
		}
	}
}
