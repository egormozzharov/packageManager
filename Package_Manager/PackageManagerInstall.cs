using System;
using System.Collections.Generic;
using log4net;
using PackageManager.Models; 
using PackageManager.Implementations;
using PackageManager.Implementations.Commands;
using PackageManager.Interfaces;

namespace PackageManager
{
	public class PackageManagerInstall
	{
		private readonly IManagersContainer managersContainer;

		public PackageManagerInstall(ILog log)
		{
			managersContainer = new ManagersContainer(log);
			Logger.Instance.WriteLog.Info("Installation package started");
		}

		public OperationResult StartDownload(InputPropertyPackage inputPropertyPackage)
		{
			double totalMemoryBefore = (double)GC.GetTotalMemory(false) / (1024 * 1024);
			Logger.Instance.WriteLog.InfoFormat("Total memory {0:N1} MBytes", totalMemoryBefore);

			OperationResult result = new OperationResult();
			try
			{
				result = PackageManagerParameters.Instance.LoadingInitialData(inputPropertyPackage);
				IList<ICommand> commandsList = CreateCommands();
				foreach (ICommand command in commandsList)
				{
					result = command.Execute();
					if (result.Status == StatusResult.Fail)
					{
						Logger.Instance.WriteLog.Error(command + ": " + result.ErrorMessage);
						return result;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog.Error(ex);
				throw;
			}
			Logger.Instance.WriteLog.Info("Installation package finished");

			double totalMemoryAfter = (double)GC.GetTotalMemory(false) / (1024 * 1024);
			Logger.Instance.WriteLog.InfoFormat("Memory footprint {0:N1} MBytes", totalMemoryAfter - totalMemoryBefore);

			return result;
		}

		private IList<ICommand> CreateCommands()
		{
			return new List<ICommand>()
			{
				new CreateDirectoriesCommand(managersContainer.DirectoryCreator),
				new DownloadPackageCommand(managersContainer.DownloadPackage),
				new DecompressZipCommand(managersContainer.Archiver),
				new CheckStructureCommand(managersContainer.PackageStructure),
				new BackupDatabaseCommand(managersContainer.BackupManager),
				new BackUpCodeTreeCommand(managersContainer.BackupManager),
				new InstallModuleCommand(managersContainer.InstallationModule),
				new BackUpCodeTreeCommand(managersContainer.BackupManager),
				new RestoreCodeTreeCommand(managersContainer.BackupManager),
			};
		} 
	}
}
