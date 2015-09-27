using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using PackageManager.Interfaces;
using PackageManager.Models;
using PackageManager.Resources;

namespace PackageManager.Implementations
{
	internal class ConfigurationFileManager : IConfigurationFileManager
	{
		internal ConfigurationFileManager() { }

		private static Regex rxMacro = new Regex(@"\$\{(\S+)\}", RegexOptions.IgnoreCase);

		private PropertyConfigModel propertyConfigModel;
		private PackageConfig packageConfig;

		private PropertyConfigModel PropertyConfigModel
		{
			get
			{
				if (propertyConfigModel == null)
				{
					// get all the properties from configuration file 
					IDictionary<string, string> properties = GetPropertyConfigDictionary(PackageManagerWorkspace.PackageConfig);
					propertyConfigModel = new PropertyConfigModel(properties);
					// update the properties based on the input data
					foreach (KeyValuePair<string, string> prop in PackageManagerParameters.Instance.InputProperty.GetAllProperties)
					{
						propertyConfigModel.AddOrUpdatePropertyConfig(prop.Key, prop.Value);
					}
				}
				return propertyConfigModel;
			}
		}

		public void AddOrUpdatePropertyConfig(string key, string newValue)
		{
			PropertyConfigModel.AddOrUpdatePropertyConfig(key, newValue);
		}

		public PackageConfig PackageConfig
		{
			get
			{
				if (packageConfig == null)
				{
					packageConfig = XmlService.GetObjectFromXml<PackageConfig>(PackageManagerWorkspace.PackageConfig);
				}
				if (PropertyConfigModel.WasChanged)
				{
					packageConfig = ModifyConfigByProperty(packageConfig, PropertyConfigModel.PropertyConfigDictionary);
					PropertyConfigModel.WasChanged = false;
				}
				return packageConfig;
			}
		}

		private static IDictionary<string, string> GetPropertyConfigDictionary(string configPath)
		{
			IDictionary<string, string> result = new Dictionary<string, string>();
			XmlNodeList nodeList = XmlService.GetNodeListByXPath(configPath, "/package/property");
			if (nodeList == null)
			{
				return result;
			}
			foreach (XmlNode node in nodeList)
			{
				if (node.Attributes != null)
				{
					string key = node.Attributes["name"].Value;
					string value = node.Attributes["value"].Value;
					result.Add(new KeyValuePair<string, string>(key, value));
				}
			}
			return result;
		}

		#region Modify PackageConfig

		private static PackageConfig ModifyConfigByProperty(PackageConfig configObject, IDictionary<string, string> properties)
		{
			if (!properties.Any())
			{
				return configObject;
			}
			string configString = XmlService.XmlSerializeToString(configObject);
			configString = ModifyConfigByProperty(configString, properties);
			return XmlService.XmlDeserializeFromString<PackageConfig>(configString);
		}

		private static string ModifyConfigByProperty(string configString, IDictionary<string, string> properties)
		{
			StringBuilder result = new StringBuilder();
			string[] conf = configString.Split('\n');

			foreach (string line in conf)
			{
				string lineResult = line;
				if (rxMacro.Matches(lineResult).Count > 0)
				{
					ReplecaValueIntoLine(ref lineResult, properties.ToList());
				}
				result.AppendLine(lineResult);
			}
			return result.ToString();
		}

		private static void ReplecaValueIntoLine(ref string lineResult, IList<KeyValuePair<string, string>> properties)
		{
			for (int i = 0, count = properties.Count; i < count; i++)
			{
				KeyValuePair<string, string> property = properties[i];
				string valueOld = GetStringExpression(property.Key);
				if (lineResult.Contains(valueOld))
				{
					lineResult = lineResult.Replace(valueOld, property.Value);
					if (rxMacro.Matches(lineResult).Count == 0)
					{
						break;
					}
					i = -1;
				}

				if (i + 1 == count)
				{
					throw new ArgumentException(CommonResources.MsgConfigFileHasUnknownVariable + lineResult);
				}
			}
		}

		private static string GetStringExpression(string property)
		{
			StringBuilder bldr = new StringBuilder();
			bldr.Append("${");
			bldr.Append(property);
			bldr.Append("}");
			return bldr.ToString();
		}

		#endregion Modify PackageConfig
	}
}
