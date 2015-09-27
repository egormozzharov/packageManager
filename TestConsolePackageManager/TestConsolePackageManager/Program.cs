using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using log4net;
using PackageManager.Models;

namespace TestConsolePackageManager
{
	class Program
	{
		static void Main(string[] args)
		{
			SetCultureInfo();

			TestDownloadPackage();
		}

		private static void SetCultureInfo()
		{
			string cultureName = ConfigurationManager.AppSettings["CultureName"];
			if (String.IsNullOrEmpty(cultureName))
			{
				cultureName = "en-US";
			}
			CultureInfo culture = CultureInfo.CreateSpecificCulture(cultureName);
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
		}

		private static ILog log;
		public static ILog Log
		{
			get
			{
				if (log == null)
				{
					log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
					log4net.Config.XmlConfigurator.Configure();
				}
				return log;
			}
		}

		private static void TestDownloadPackage()
		{
			try
			{
				InputPropertyPackage inputProperty = new InputPropertyPackage();
				inputProperty.PackageName.Value = "TehDocName";
				inputProperty.InnovatorDirectory.Value = ConfigurationManager.AppSettings["ArasApplicationWorkspace"];
				inputProperty.PackageDirectory.Value = ConfigurationManager.AppSettings["PackageManagerWorkspace"];
				inputProperty.InnovatorDb.Value = "InnovatorSolutions";
				inputProperty.DownloadPackageModel.FtpUserName = ConfigurationManager.AppSettings["FtpUserName"];
				inputProperty.DownloadPackageModel.FtpPassword = ConfigurationManager.AppSettings["FtpUserName"];
				inputProperty.DownloadPackageModel.PackageUrl = new Uri(ConfigurationManager.AppSettings["PackageUrl"]);
				inputProperty.DownloadPackageModel.ProtocolTypeForDownloadPackage = (ProtocolTypeForDownloadPackage)Enum.Parse(typeof(ProtocolTypeForDownloadPackage), ConfigurationManager.AppSettings["ProtocolType"]);
				inputProperty.SqlConnectionString = ConfigurationManager.AppSettings["SqlConnectionString"];
				inputProperty.AddInstallationModuleType(new List<InstallationModuleType>
				{
					InstallationModuleType.DbModule,
					InstallationModuleType.VaultServerModule,
					InstallationModuleType.InnovatorServerModule,
					InstallationModuleType.ConversionServerModule,
				});

				PackageManager.PackageManagerInstall packageManager = new PackageManager.PackageManagerInstall(Log);
				OperationResult result = packageManager.StartDownload(inputProperty);
				if (result.Status == StatusResult.Warning)
				{
					foreach (string warn in result.WarningMessages)
					{
						Console.WriteLine(warn);
					}
				}
				Console.WriteLine("End.");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			Console.ReadKey();
		}

	}
}
