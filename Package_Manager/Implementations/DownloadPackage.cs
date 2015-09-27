using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using PackageManager.Extensions;
using PackageManager.Helpers;
using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations
{
	internal class DownloadPackage : IDownloadPackage
	{
		internal DownloadPackage() { }

		public OperationResult DownloadFromSite(Uri address, string fileName)
		{
			Logger.Instance.WriteLog.Info("DownloadFromSite started");
			OperationResult result = ValidationValue.GetOperationResultAndCheckValueOnNullOrEmpty(new Dictionary<string, object> { {"address", address}, {"fileName", fileName} });
			if (result.Status == StatusResult.Fail)
			{
				return result;
			}
				
			try
			{
				CheckDirectory(fileName);
				using (WebClient client = new WebClient())
				{
					client.DownloadFile(address, fileName);
				}
				Logger.Instance.WriteLog.Info("DownloadFromSite finished");
			}
			catch (WebException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}

		public OperationResult DownloadFromFtp(Uri address, string fileName, string credentialUserName, string credentialPassword)
		{
			Logger.Instance.WriteLog.Info("DownloadFromFtp started");
			OperationResult result = ValidationValue.GetOperationResultAndCheckValueOnNullOrEmpty(new Dictionary<string, object>
			{
				{"address", address},
				{"fileName", fileName},
				{"credentialUserName", credentialUserName},
				{"credentialPassword", credentialPassword}
			});
			if (result.Status == StatusResult.Fail)
			{
				return result;
			}

			try
			{
				CheckDirectory(fileName);
				using (WebClient request = new WebClient())
				{
					request.Credentials = new NetworkCredential(credentialUserName, credentialPassword);
					byte[] fileData = request.DownloadData(address);

					using (FileStream file = File.Create(fileName))
					{
						file.Write(fileData, 0, fileData.Length);
					}
				}
				Logger.Instance.WriteLog.Info("DownloadFromFtp finished");
			}
			catch (WebException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}

		private static void CheckDirectory(string filePath)
		{
			FileManager.DeleteFile(filePath);
			FileManager.CreateDirectoryIfNotExists(Path.GetDirectoryName(filePath));
		}
	}
}
