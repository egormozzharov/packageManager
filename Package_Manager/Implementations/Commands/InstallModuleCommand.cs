using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations.Commands
{
	internal class InstallModuleCommand : ICommand
	{
		private IInstallationModule installationModule;

		internal InstallModuleCommand(IInstallationModule installationModule)
		{
			this.installationModule = installationModule;
		}

		public OperationResult Execute()
		{
			return installationModule.InstallModule(PackageManagerWorkspace.PackageConfig, PackageManagerParameters.Instance.InputProperty.InstallationModuleTypes);
		}
	}
}
