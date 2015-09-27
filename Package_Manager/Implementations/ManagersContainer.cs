using System;
using log4net;
using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations
{
	internal class ManagersContainer : IManagersContainer
	{
		internal ManagersContainer(ILog log)
		{
			if (log == null)
			{
				throw new ArgumentNullException("log");
			}
			Logger.Instance.Initialize(log);
		}

		private IDownloadPackage downloadPackage;
		public IDownloadPackage DownloadPackage
		{
			get { return downloadPackage ?? (downloadPackage = new DownloadPackage()); }
		}

		private IBackupManager backupManager;
		public IBackupManager BackupManager
		{
			get { return backupManager ?? (backupManager = new BackupManager(DbService, GitWrapper)); }
		}

		private IDbService dbService;
		public IDbService DbService
		{
			get { return dbService ?? (dbService = new DbService()); }
		}

		private IDirectoryCreator directoryCreator;
		public IDirectoryCreator DirectoryCreator
		{
			get { return directoryCreator ?? (directoryCreator = new DirectoryCreator()); }
		}

		private IPackageStructure packageStructure;
		public IPackageStructure PackageStructure
		{
			get { return packageStructure ?? (packageStructure = new PackageStructure()); }
		}

		private IGitWrapper gitWrapper;
		public IGitWrapper GitWrapper
		{
			get { return gitWrapper ?? (gitWrapper = new GitWrapper("Aras", "info@aras.com", PackageManagerWorkspace.ArasApplicationPath)); }
		}

		private IArchiver archiver;
		public IArchiver Archiver
		{
			get { return archiver ?? (archiver = new Archiver()); }
		}

		private IInstallationModule installationModule;
		public IInstallationModule InstallationModule
		{
			get { return installationModule ?? (installationModule = new InstallationModule(ConfigurationFileManager)); }
		}

		private IConfigurationFileManager configurationFileManager;
		public IConfigurationFileManager ConfigurationFileManager
		{
			get { return configurationFileManager ?? (configurationFileManager = new ConfigurationFileManager()); }
		}
	}
}
