using System;
using System.Collections.Generic;

namespace PackageManager.Models
{
	public class InputPropertyPackage
	{
		public InputPropertyPackage()
		{
			InstallationModuleTypes = new List<InstallationModuleType>();
			DownloadPackageModel = new DownloadPackageModel();

			_innovatorUrl = new PropertyConfig("Innovator.URL", TypeProperty.Url);
			_innovatorDb = new PropertyConfig("Innovator.DB", TypeProperty.Text);
			_userName = new PropertyConfig("User.Name", TypeProperty.Text);
			_userPassword = new PropertyConfig("User.Password", TypeProperty.Text);
			_innovatorDirectory = new PropertyConfig("Innovator.Dir", TypeProperty.Directory);
			_vaultDirectory = new PropertyConfig("Vault.Dir", TypeProperty.Directory);
			_conversionDirectory = new PropertyConfig("Conversion.Dir", TypeProperty.Directory);
			_packageName = new PropertyConfig("Package.Name", TypeProperty.Text);
			_packageDirectory = new PropertyConfig("Package.Dir", TypeProperty.Directory);
			_packageVersion = new PropertyConfig("Package.Version", TypeProperty.Text);
		}

		internal IDictionary<string, string> GetAllProperties
		{
			get
			{
				IDictionary<string, string> prop = new Dictionary<string, string>();
				prop.Add(_innovatorUrl.Name, _innovatorUrl.Value);
				prop.Add(_innovatorDb.Name, _innovatorDb.Value);
				prop.Add(_userName.Name, _userName.Value);
				prop.Add(_userPassword.Name, _userPassword.Value);
				prop.Add(_innovatorDirectory.Name, _innovatorDirectory.Value);
				prop.Add(_vaultDirectory.Name, _vaultDirectory.Value);
				prop.Add(_conversionDirectory.Name, _conversionDirectory.Value);
				prop.Add(_packageName.Name, _packageName.Value);
				prop.Add(_packageDirectory.Name, _packageDirectory.Value);
				prop.Add(_packageVersion.Name, _packageVersion.Value);
				return prop;
			}
		}

		public DownloadPackageModel DownloadPackageModel { get; set; }
		public string SqlConnectionString { get; set; }
		public IList<InstallationModuleType> InstallationModuleTypes { get; private set; }

		public void AddInstallationModuleType(InstallationModuleType installationModuleType)
		{
			InstallationModuleTypes.Add(installationModuleType);
		}
		public void AddInstallationModuleType(IList<InstallationModuleType> installationModuleTypes)
		{
			((List<InstallationModuleType>)InstallationModuleTypes).AddRange(installationModuleTypes);
		}

		private readonly PropertyConfig _innovatorUrl;
		public PropertyConfig InnovatorUrl
		{
			get { return _innovatorUrl; }
			set { if (value != null) _innovatorUrl.Value = value.Value; }
		}

		private readonly PropertyConfig _innovatorDb;
		public PropertyConfig InnovatorDb
		{
			get { return _innovatorDb; }
			set { if (value != null) _innovatorDb.Value = value.Value; }
		}

		private readonly PropertyConfig _userName;
		public PropertyConfig UserName
		{
			get { return _userName; }
			set { if (value != null) _userName.Value = value.Value; }
		}

		private readonly PropertyConfig _userPassword;
		public PropertyConfig UserPassword
		{
			get { return _userPassword; }
			set { if (value != null) _userPassword.Value = value.Value; }
		}

		private readonly PropertyConfig _innovatorDirectory;
		public PropertyConfig InnovatorDirectory
		{
			get { return _innovatorDirectory; }
			set { if (value != null) _innovatorDirectory.Value = value.Value; }
		}

		private readonly PropertyConfig _vaultDirectory;
		public PropertyConfig VaultDirectory
		{
			get { return _vaultDirectory; }
			set { if (value != null)  _vaultDirectory.Value = value.Value; }
		}

		private readonly PropertyConfig _conversionDirectory;
		public PropertyConfig ConversionDirectory
		{
			get { return _conversionDirectory; }
			set { if (value != null) _conversionDirectory.Value = value.Value; }
		}

		private readonly PropertyConfig _packageName;
		public PropertyConfig PackageName
		{
			get { return _packageName; }
			set { if (value != null) _packageName.Value = value.Value; }
		}

		private readonly PropertyConfig _packageDirectory;
		public PropertyConfig PackageDirectory
		{
			get { return _packageDirectory; }
			set { if (value != null) _packageDirectory.Value = value.Value; }
		}

		private readonly PropertyConfig _packageVersion;
		public PropertyConfig PackageVersion
		{
			get { return _packageVersion; }
			set { if (value != null) _packageVersion.Value = value.Value; }
		}
	}

	public class PropertyConfig
	{
		private PropertyConfig() { }

		public PropertyConfig(string name, TypeProperty typeProperty)
		{
			Name = name;
			TypeProperty = typeProperty;
		}

		public PropertyConfig(string name, TypeProperty typeProperty, string label)
			: this(name, typeProperty)
		{
			_label = label;
		}

		public string Name { get; private set; }
		public string Value { get; set; }
		public TypeProperty TypeProperty { get; private set; }

		private string _label;
		public string Label
		{
			get { return String.IsNullOrEmpty(_label) ? Name : _label; }
			set { _label = value; }
		}
	}
}
