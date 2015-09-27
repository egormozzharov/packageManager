using PackageManager.Interfaces;
using PackageManager.Models;

namespace PackageManager.Implementations.Commands
{
	internal class DownloadPackageCommand : ICommand
	{
		private IDownloadPackage downloadPackage;

		internal DownloadPackageCommand(IDownloadPackage downloadPackage)
		{
			this.downloadPackage = downloadPackage;
		}

		public OperationResult Execute()
		{
			OperationResult result;
			if (PackageManagerParameters.Instance.InputProperty.DownloadPackageModel.ProtocolTypeForDownloadPackage == ProtocolTypeForDownloadPackage.Ftp)
			{
				result =
					downloadPackage.DownloadFromFtp(PackageManagerParameters.Instance.InputProperty.DownloadPackageModel.PackageUrl,
						PackageManagerWorkspace.PackageZipPath,
						PackageManagerParameters.Instance.InputProperty.DownloadPackageModel.FtpUserName,
						PackageManagerParameters.Instance.InputProperty.DownloadPackageModel.FtpPassword);
			}
			else
			{
				result =
					downloadPackage.DownloadFromSite(PackageManagerParameters.Instance.InputProperty.DownloadPackageModel.PackageUrl,
						PackageManagerWorkspace.PackageZipPath);
			}
			return result;
		}
	}
}
