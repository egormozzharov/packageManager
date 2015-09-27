using System.Collections.Generic;
using PackageManager.Models;

namespace PackageManager.Interfaces
{
	internal interface IInstallationModule
	{
		OperationResult InstallModule(string configPath, IList<InstallationModuleType> moduleInstallationTypes);
	}
}
