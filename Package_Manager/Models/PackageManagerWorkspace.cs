using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PackageManager.Models
{
	internal static class PackageManagerWorkspace
	{
		private const string FolderNameForPackageManager = "PackageManager";
		private const string FolderNameForBackup = "Backup";
		private const string FolderNameForLogs = "Logs";
		private const string DateTimeFormatPattern = "yyyy-MM-dd HH-mm-ss";
		private const string ConfigFileName = "package.config";

		public static string PackageManagerPath
		{
			get { return PackageManagerParameters.Instance.InputProperty.PackageDirectory.Value; }
		}

		public static string ArasApplicationPath
		{
			get { return PackageManagerParameters.Instance.InputProperty.InnovatorDirectory.Value; }
		}

		public static string PackageManager 
		{
			get { return Path.Combine(PackageManagerPath, FolderNameForPackageManager); }
		}

		public static string PackagePath
		{
			get { return Path.Combine(PackageManager, PackageManagerParameters.Instance.InputProperty.PackageName.Value); }
		}

		public static string PackageZipPath
		{
			get { return PackagePath + ".zip"; }
		}

		public static string PackageConfig
		{
			get { return Path.Combine(PackagePath, ConfigFileName); }
		}

		public static string BackupFolderPath
		{
			get { return Path.Combine(PackagePath, FolderNameForBackup); }
		}

		public static string LogsPath
		{
			get { return Path.Combine(PackagePath, FolderNameForLogs); }
		}

		public static string BackupFileName
		{
			get { return String.Format(CultureInfo.InvariantCulture, "{0}.{1}", PackageManagerParameters.Instance.InputProperty.InnovatorDb.Value, "bak"); }
		}

		public static string CurrentBackupPath
		{
			get
			{
				string currentBackupFolder = Directory.GetDirectories(BackupFolderPath).OrderByDescending(folder => folder).FirstOrDefault();
				return Path.Combine(currentBackupFolder, BackupFileName);
			}
		}

		public static string NextBackupFolder
		{
			get { return Path.Combine(BackupFolderPath, DateTime.Now.ToString(DateTimeFormatPattern, CultureInfo.InvariantCulture)); }
		}
	}
}
