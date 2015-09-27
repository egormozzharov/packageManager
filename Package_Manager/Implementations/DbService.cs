using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using PackageManager.Interfaces;

namespace PackageManager.Implementations
{
	internal class DbService : IDbService
	{
		internal DbService() { }

		public void BackupDatabase(SqlConnectionStringBuilder sqlConnection, string destinationPath)
		{
			ServerConnection serverConnection = null;
			try
			{
				serverConnection = new ServerConnection(sqlConnection.DataSource, sqlConnection.UserID, sqlConnection.Password);
				Server sqlServer = new Server(serverConnection);
				Backup backupDatabase = new Backup()
				{
					Action = BackupActionType.Database,
					Database = sqlConnection.InitialCatalog,
				};
				BackupDeviceItem backupItem = new BackupDeviceItem(destinationPath, DeviceType.File);
				backupDatabase.Devices.Add(backupItem);
				backupDatabase.SqlBackup(sqlServer);
			}
			finally
			{
				if (serverConnection != null && serverConnection.IsOpen)
				{
					serverConnection.Disconnect();
				}
			}
		}

		public void RestoreDatabase(SqlConnectionStringBuilder sqlConnection, String backUpFile)
		{
			ServerConnection serverConnection = null;
			try
			{
				if (!FileManager.FileExists(backUpFile))
				{
					throw new FileNotFoundException();
				}
				serverConnection = new ServerConnection(sqlConnection.DataSource, sqlConnection.UserID, sqlConnection.Password);
				Server sqlServer = new Server(serverConnection);
				Restore restoreDatabase = new Restore()
				{
					Action = RestoreActionType.Database,
					Database = sqlConnection.InitialCatalog,
				};
				BackupDeviceItem backupItem = new BackupDeviceItem(backUpFile, DeviceType.File);
				restoreDatabase.Devices.Add(backupItem);
				restoreDatabase.ReplaceDatabase = true;
				restoreDatabase.SqlRestore(sqlServer);
			}
			finally
			{
				if (serverConnection != null && serverConnection.IsOpen)
				{
					serverConnection.Disconnect();
				}
			}
		}
	}
}
