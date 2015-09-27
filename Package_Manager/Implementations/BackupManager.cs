using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LibGit2Sharp;
using Microsoft.SqlServer.Management.Common;
using PackageManager.Extensions;
using PackageManager.Helpers;
using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations
{
	internal class BackupManager : IBackupManager
	{
		private readonly IDbService dbService;
		private readonly IGitWrapper gitWrapper;

		internal BackupManager(IDbService dbService, IGitWrapper gitWrapper)
		{
			this.dbService = dbService;
			this.gitWrapper = gitWrapper;
		}

		public OperationResult BackUpCodeTree()
		{
			Logger.Instance.WriteLog.Info("BackUpCodeTree started");
			OperationResult result = new OperationResult() {Status = StatusResult.Success};
			try
			{
				gitWrapper.CommitChanges();
				Logger.Instance.WriteLog.Info("BackUpCodeTree finished");
			}
			catch(LibGit2SharpException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}

		public OperationResult RestoreCodeTree()
		{
			Logger.Instance.WriteLog.Info("RestoreCodeTree started");
			OperationResult result = new OperationResult() { Status = StatusResult.Success };
			try
			{
				gitWrapper.RevertLastCommit();
				Logger.Instance.WriteLog.Info("RestoreCodeTree finished");
			}
			catch (LibGit2SharpException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}

		public OperationResult BackupDatabase(string sqlConnectionString, string destinationPath)
		{
			Logger.Instance.WriteLog.Info("BackupDatabase started");
			OperationResult result = ValidationValue.GetOperationResultAndCheckValueOnNullOrEmpty(new Dictionary<string, object>()
			{
				{"sqlConnectionString", sqlConnectionString},
				{"destinationPath", destinationPath}
			});
			SqlConnectionStringBuilder sqlConnection = new SqlConnectionStringBuilder(sqlConnectionString);
			try
			{
				dbService.BackupDatabase(sqlConnection, destinationPath);
				Logger.Instance.WriteLog.Info("BackupDatabase finished");
			}
			catch (SqlServerManagementException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}

		public OperationResult RestoreDatabase(string sqlConnectionString, String backUpFile)
		{
			Logger.Instance.WriteLog.Info("RestoreDatabase started");
			OperationResult result = ValidationValue.GetOperationResultAndCheckValueOnNullOrEmpty(new Dictionary<string, object>()
			{
				{"sqlConnectionString", sqlConnectionString},
				{"backUpFile", backUpFile}
			});
			SqlConnectionStringBuilder sqlConnection = new SqlConnectionStringBuilder(sqlConnectionString);
			try
			{
				dbService.RestoreDatabase(sqlConnection, backUpFile);
				Logger.Instance.WriteLog.Info("RestoreDatabase finished");
			}
			catch (SqlServerManagementException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}
	}
}
