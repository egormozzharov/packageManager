using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PackageManager.Extensions;
using PackageManager.Interfaces;
using PackageManager.Models;
using PackageManager.Resources;
using PackageManager.Tasks;

namespace PackageManager.Implementations
{
	internal class InstallationModule : IInstallationModule
	{
		private readonly IConfigurationFileManager configurationFileManager;

		internal InstallationModule(IConfigurationFileManager configurationFileManager)
		{
			this.configurationFileManager = configurationFileManager;
		}

		public OperationResult InstallModule(string configPath, IList<InstallationModuleType> moduleInstallationTypes)
		{
			OperationResult result = new OperationResult();
			OperationResult newResult;

			IEnumerable<ModuleInstallation> moduleInstallations = configurationFileManager.PackageConfig.Installation.Module.Where(x => moduleInstallationTypes.Contains(x.InstallationModuleType));
			if (!moduleInstallations.Any())
			{
				return result.GetWarningFromString(CommonResources.MsgInstallationModuleNotExistInConfigFile);
			}

			newResult = XmlPeek(configurationFileManager.PackageConfig);
			result = result.СombineResult(newResult);

			newResult = XmlPoke(configurationFileManager.PackageConfig);
			result = result.СombineResult(newResult);

			foreach (ModuleInstallation module in moduleInstallations)
			{
				Logger.Instance.WriteLog.Info(string.Format(CultureInfo.InvariantCulture, "Installation module ({0}) started", module.Id));

				newResult = DeleteFiles(module);
				result = result.СombineResult(newResult);

				newResult = CopyingFile(module);
				result = result.СombineResult(newResult);

				newResult = ProgramExecution(module);
				result = result.СombineResult(newResult);

				newResult = ImportIntoDb(module);
				result = result.СombineResult(newResult);

				Logger.Instance.WriteLog.Info(string.Format(CultureInfo.InvariantCulture, "Installation module ({0}) finished", module.Id));
			}
			return result;
		}

		private OperationResult XmlPeek(PackageConfig packageConfig)
		{
			if (!packageConfig.XmlPeek.Any())
			{
				Logger.Instance.WriteLog.Info(CommonResources.MsgPropertyNotExistInConfigFile.GetStringByFormat("XmlPeek"));
				return null;
			}
			Logger.Instance.WriteLog.Info("XmlPeek started");
			OperationResult result = new OperationResult();
			foreach (XmlPeek peek in packageConfig.XmlPeek)
			{
				OperationResult newResult = new XmlPeekTask(peek, configurationFileManager).Execute();
				result = result.СombineResult(newResult);
			}
			Logger.Instance.WriteLog.Info("XmlPeek finished");
			return result;
		}

		private static OperationResult XmlPoke(PackageConfig packageConfig)
		{
			if (!packageConfig.XmlPoke.Any())
			{
				Logger.Instance.WriteLog.Info(CommonResources.MsgPropertyNotExistInConfigFile.GetStringByFormat("XmlPoke"));
				return null;
			}
			Logger.Instance.WriteLog.Info("XmlPoke started");
			OperationResult result = new OperationResult();
			foreach (XmlPoke poke in packageConfig.XmlPoke)
			{
				OperationResult newResult = new XmlPokeTask(poke).Execute();
				result = result.СombineResult(newResult);
			}
			Logger.Instance.WriteLog.Info("XmlPoke finished");
			return result;
		}

		private static OperationResult CopyingFile(ModuleInstallation module)
		{
			if (!module.Copy.Any())
			{
				Logger.Instance.WriteLog.Info(CommonResources.MsgPropertyNotExistInConfigFile.GetStringByFormat("Copy"));
				return null;
			}
			Logger.Instance.WriteLog.Info("CopyingFile started");
			OperationResult result = new OperationResult();
			foreach (Copy item in module.Copy)
			{
				OperationResult newResult = new CopyTask(item).Execute();
				result = result.СombineResult(newResult);
			}
			Logger.Instance.WriteLog.Info("CopyingFile finished");
			return result;
		}

		private static OperationResult DeleteFiles(ModuleInstallation module)
		{
			if (!module.Delete.Any())
			{
				Logger.Instance.WriteLog.Info(CommonResources.MsgPropertyNotExistInConfigFile.GetStringByFormat("Delete"));
				return null;
			}
			Logger.Instance.WriteLog.Info("DeleteFile started");
			OperationResult result = new OperationResult();
			foreach (Delete item in module.Delete)
			{
				OperationResult newResult = new DeleteTask(item).Execute();
				result = result.СombineResult(newResult);
			}
			Logger.Instance.WriteLog.Info("DeleteFile finished");
			return result;
		}

		private static OperationResult ProgramExecution(ModuleInstallation module)
		{
			if (!module.Exec.Any())
			{
				Logger.Instance.WriteLog.Info(CommonResources.MsgPropertyNotExistInConfigFile.GetStringByFormat("Exec"));
				return null;
			}
			Logger.Instance.WriteLog.Info("Program execution started");
			OperationResult result = new OperationResult();
			foreach (Exec item in module.Exec)
			{
				OperationResult newResult = new ExecTask(item).Execute();
				result = result.СombineResult(newResult);
			}
			Logger.Instance.WriteLog.Info("Program execution finished");
			return result;
		}

		private static OperationResult ImportIntoDb(ModuleInstallation module)
		{
			if (!module.Import.Any())
			{
				Logger.Instance.WriteLog.Info(CommonResources.MsgPropertyNotExistInConfigFile.GetStringByFormat("Import"));
				return null;
			}
			Logger.Instance.WriteLog.Info("ImportIntoDb started");
			OperationResult result = new OperationResult();
			foreach (Import item in module.Import)
			{
				OperationResult newResult = new ImportTask(item).Execute();
				result = result.СombineResult(newResult);
			}
			Logger.Instance.WriteLog.Info("ImportIntoDb finished");
			return result;
		}
	}
}
