using System;
using PackageManager.Models;

namespace PackageManager.Interfaces
{
	internal interface IDownloadPackage
	{
		OperationResult DownloadFromSite(Uri address, string fileName);
		OperationResult DownloadFromFtp(Uri address, string fileName, string credentialUserName, string credentialPassword);
	}
}
