namespace PackageManager.Interfaces
{
	internal interface IManagersContainer
	{
		IDownloadPackage DownloadPackage { get; }
		IBackupManager BackupManager { get; }		
		IDbService DbService { get; }
		IDirectoryCreator DirectoryCreator { get; }
		IPackageStructure PackageStructure { get; }
		IGitWrapper GitWrapper { get; }
		IArchiver Archiver { get; }
		IInstallationModule InstallationModule { get; }
		IConfigurationFileManager ConfigurationFileManager { get; }
	}
}
