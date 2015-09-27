using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using PackageManager.Extensions;
using PackageManager.Interfaces;
using PackageManager.Models;
using PackageManager.Resources;

namespace PackageManager.Implementations
{
	internal class PackageStructure : IPackageStructure
	{
		private string[] folders = { "files", "imports", "scripts" };
		private const string nameSchemaInResource = "PackageManager.XMLSchema.PackageConfig.xsd";
		private StringBuilder packageConfigError;

		public PackageStructure() { }

		public OperationResult CheckStructure()
		{
			Logger.Instance.WriteLog.Info("Checking package started");
			OperationResult result = new OperationResult { Status = StatusResult.Success };
			try
			{
				if (!FileManager.FileExists(PackageManagerWorkspace.PackageConfig))
				{
					result.ErrorMessage = CommonResources.MsgConfigFileMissing;
				}
				if (!CheckPackageStructure())
				{
					result.ErrorMessage += CommonResources.MsgPackageEmpty;
				}
				if (string.IsNullOrEmpty(result.ErrorMessage))
				{
					result.ErrorMessage = ValidationByXmlScheme(PackageManagerWorkspace.PackageConfig, nameSchemaInResource);
				}

				if (result.ErrorMessage.Length > 0)
				{
					result.Status = StatusResult.Fail;
				}

				Logger.Instance.WriteLog.Info("Checking package finished");
			}
			catch (IOException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}

		public bool CheckPackageStructure()
		{
			int countFile = 0;
			foreach (string folder in folders)
			{
				string path = Path.Combine(PackageManagerWorkspace.PackagePath, folder);
				countFile += FileManager.CountFileInDirectory(path);
			}
			return countFile != 0;
		}

		private string ValidationByXmlScheme(string xmlPath, string nameSchemaInResource)
		{
			packageConfigError = new StringBuilder();
			try
			{
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.ValidationType = ValidationType.Schema;
				settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandle);
				settings.Schemas.Add(GetXmlSchema(nameSchemaInResource));
				settings.IgnoreWhitespace = true;
				using (StreamReader stringReader = new StreamReader(xmlPath))
				{
					XmlReader reader = XmlReader.Create(stringReader, settings);
					while (reader.Read()) { }
				}
			}
			catch (XmlException ex)
			{
				packageConfigError.AppendLine(ex.Message);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog.Error("Error: " + ex.Message);
				throw;
			}
			return packageConfigError.ToString();
		}

		private XmlSchema GetXmlSchema(string nameSchemaInResource)
		{
			XmlSchema schema;
			Assembly assembly = Assembly.GetExecutingAssembly();

			using (Stream stream = assembly.GetManifestResourceStream(nameSchemaInResource))
			{
				XmlReader reader = XmlReader.Create(stream);
				schema = XmlSchema.Read(reader, new ValidationEventHandler(ValidationEventHandle));
			}
			return schema;
		}

		private void ValidationEventHandle(object sender, ValidationEventArgs e)
		{
			if (e.Severity == XmlSeverityType.Warning)
			{
				Logger.Instance.WriteLog.Warn(e.Message);
			}
			else if (e.Severity == XmlSeverityType.Error)
			{
				Logger.Instance.WriteLog.Error(e.Message);
				packageConfigError.AppendLine(e.Message);
			}
		}
	}
}