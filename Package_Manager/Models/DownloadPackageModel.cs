using System;

namespace PackageManager.Models
{
	public class DownloadPackageModel
	{
		public Uri PackageUrl { get; set; }
		public ProtocolTypeForDownloadPackage ProtocolTypeForDownloadPackage { get; set; }
		public string FtpUserName { get; set; }
		public string FtpPassword { get; set; }
	}
}
