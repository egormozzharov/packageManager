using System;
using System.Data.SqlClient;

namespace PackageManager.Interfaces
{
	internal interface IDbService
	{
		void BackupDatabase(SqlConnectionStringBuilder sqlConnection, string destinationPath);
		void RestoreDatabase(SqlConnectionStringBuilder sqlConnection, String backUpFile);
	}
}
