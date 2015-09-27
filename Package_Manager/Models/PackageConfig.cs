using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace PackageManager.Models
{
	[XmlRoot(ElementName = "package")]
	public class PackageConfig
	{
		public PackageConfig()
		{
			Property = new Collection<PropertyPackage>();
			FileSet = new Collection<FileSet>();
			XmlPeek = new Collection<XmlPeek>();
			XmlPoke = new Collection<XmlPoke>();
			TypePackage = "aia"; // DefaultValue
		}

		[XmlElement(ElementName = "dependencies")]
		public DependenciesFrom DependenciesFrom { get; set; }

		[XmlElement(ElementName = "property")]
		public Collection<PropertyPackage> Property { get; private set; }

		[XmlElement(ElementName = "installation")]
		public Installation Installation { get; set; }

		[XmlElement(ElementName = "fileset")]
		public Collection<FileSet> FileSet { get; private set; }

		[XmlElement(ElementName = "xmlpeek")]
		public Collection<XmlPeek> XmlPeek { get; private set; }

		[XmlElement(ElementName = "xmlpoke")]
		public Collection<XmlPoke> XmlPoke { get; private set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string TypePackage { get; set; }

		[XmlAttribute(AttributeName = "owner")]
		public string Owner { get; set; }

		[XmlAttribute(AttributeName = "description")]
		public string Description { get; set; }

		[XmlAttribute(AttributeName = "copyright")]
		public string Copyright { get; set; }
	}

	[XmlRoot(ElementName = "innovator")]
	public class Innovator
	{
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }
	}

	[XmlRoot(ElementName = "or")]
	public class DependenceOr
	{
		public DependenceOr()
		{
			Innovator = new Collection<Innovator>();
			Application = new Collection<Application>();
		}

		[XmlElement(ElementName = "innovator")]
		public Collection<Innovator> Innovator { get; private set; }

		[XmlElement(ElementName = "application")]
		public Collection<Application> Application { get; private set; }
	}

	[XmlRoot(ElementName = "name")]
	public class Name
	{
		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }

		[XmlAttribute(AttributeName = "dbvar")]
		public string DbVar { get; set; }

		[XmlAttribute(AttributeName = "path")]
		public string Path { get; set; }
	}

	[XmlRoot(ElementName = "version")]
	public class Version
	{
		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }

		[XmlAttribute(AttributeName = "dbvar")]
		public string DbVar { get; set; }

		[XmlAttribute(AttributeName = "path")]
		public string Path { get; set; }
	}

	[XmlRoot(ElementName = "application")]
	public class Application
	{
		public Application()
		{
			Version = new Collection<Version>();
			Condition = "Exist";
		}

		[XmlElement(ElementName = "name")]
		public Name Name { get; set; }

		[XmlElement(ElementName = "version")]
		public Collection<Version> Version { get; private set; }

		[XmlAttribute(AttributeName = "condition")]
		public string Condition { get; set; }
	}

	[XmlRoot(ElementName = "dependencies")]
	public class DependenciesFrom
	{
		public DependenciesFrom()
		{
			DependenceOr = new Collection<DependenceOr>();
		}

		[XmlElement(ElementName = "or")]
		public Collection<DependenceOr> DependenceOr { get; private set; }

		[XmlElement(ElementName = "innovator")]
		public Innovator Innovator { get; set; }

		[XmlElement(ElementName = "application")]
		public Application Application { get; set; }
	}

	[XmlRoot(ElementName = "property")]
	public class PropertyPackage
	{
		public PropertyPackage()
		{
			TypeValue = "text"; // Default value
			LabelProperty = Name; // Default value
		}

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string TypeValue { get; set; }

		[XmlAttribute(AttributeName = "label")]
		public string LabelProperty { get; set; }
	}

	[XmlRoot(ElementName = "import")]
	public class Import : FailOnErrorAndBackupProperty
	{
		[XmlAttribute(AttributeName = "dir")]
		public string Direct { get; set; }
	}

	[XmlRoot(ElementName = "exec")]
	public class Exec : FailOnErrorProperty
	{
		public Exec()
		{
			Timeout = 0; // DefaultValue
		}

		[XmlAttribute(AttributeName = "program")]
		public string Program { get; set; }

		[XmlAttribute(AttributeName = "basedir")]
		public string BaseDirectory { get; set; }

		[XmlAttribute(AttributeName = "commandline")]
		public string CommandLine { get; set; }

		[XmlAttribute(AttributeName = "workingdir")]
		public string WorkingDirect { get; set; }

		[XmlAttribute(AttributeName = "timeout")]
		public int Timeout { get; set; }

		[XmlAttribute(AttributeName = "output")]
		public string Output { get; set; }
	}

	[XmlRoot(ElementName = "copy")]
	public class Copy : FailOnErrorAndBackupProperty
	{
		[XmlAttribute(AttributeName = "file")]
		public string File { get; set; }

		[XmlAttribute(AttributeName = "todir")]
		public string ToDirect { get; set; }

		[XmlElement(ElementName = "fileset")]
		public FileSet FileSet { get; set; }
	}

	[XmlRoot(ElementName = "delete")]
	public class Delete : FailOnErrorAndBackupProperty
	{
		[XmlAttribute(AttributeName = "file")]
		public string File { get; set; }

		[XmlElement(ElementName = "fileset")]
		public FileSet FileSet { get; set; }
	}

	[XmlRoot(ElementName = "module")]
	public class ModuleInstallation
	{
		public ModuleInstallation()
		{
			Import = new Collection<Import>();
			Exec = new Collection<Exec>();
			Copy = new Collection<Copy>();
			Delete = new Collection<Delete>();
		}

		[XmlElement(ElementName = "import")]
		public Collection<Import> Import { get; private set; }

		[XmlElement(ElementName = "exec")]
		public Collection<Exec> Exec { get; private set; }

		[XmlElement(ElementName = "copy")]
		public Collection<Copy> Copy { get; private set; }

		[XmlElement(ElementName = "delete")]
		public Collection<Delete> Delete { get; private set; }

		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		public InstallationModuleType InstallationModuleType
		{
			get
			{
				switch (Id)
				{
					case "DB":
						return InstallationModuleType.DbModule;
					case "IS":
						return InstallationModuleType.InnovatorServerModule;
					case "VS":
						return InstallationModuleType.VaultServerModule;
					case "CS":
						return InstallationModuleType.ConversionServerModule;
				}
				return InstallationModuleType.OtherModule;
			}
		}
	}

	[XmlRoot(ElementName = "installation")]
	public class Installation
	{
		public Installation()
		{
			Module = new Collection<ModuleInstallation>();
		}

		[XmlElement(ElementName = "module")]
		public Collection<ModuleInstallation> Module { get; private set; }
	}

	[XmlRoot(ElementName = "include")]
	public class Include
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "exclude")]
	public class Exclude
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "fileset")]
	public class FileSet
	{
		public FileSet()
		{
			Include = new Collection<Include>();
			Exclude = new Collection<Exclude>();
		}

		[XmlElement(ElementName = "include")]
		public Collection<Include> Include { get; private set; }

		[XmlElement(ElementName = "exclude")]
		public Collection<Exclude> Exclude { get; private set; }

		[XmlAttribute(AttributeName = "basedir")]
		public string BaseDirectory { get; set; }
	}

	[XmlRoot(ElementName = "xmlpeek")]
	public class XmlPeek : FailOnErrorProperty
	{
		public XmlPeek()
		{
			NodeIndex = 0; // DefaultValue
		}

		[XmlAttribute(AttributeName = "file")]
		public string File { get; set; }

		[XmlAttribute(AttributeName = "xpath")]
		public string XPath { get; set; }

		[XmlAttribute(AttributeName = "property")]
		public string Property { get; set; }

		[XmlAttribute(AttributeName = "nodeindex")]
		public int NodeIndex { get; set; }

		[XmlElement(ElementName = "namespaces")]
		public XmlNamespaces Namespaces { get; set; }
	}

	[XmlRoot(ElementName = "xmlpoke")]
	public class XmlPoke : FailOnErrorAndBackupProperty
	{
		[XmlAttribute(AttributeName = "file")]
		public string File { get; set; }

		[XmlAttribute(AttributeName = "xpath")]
		public string XPath { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }

		[XmlAttribute(AttributeName = "preservewhitespace")]
		public bool PreserveWhiteSpace { get; set; }

		[XmlElement(ElementName = "namespaces")]
		public XmlNamespaces Namespaces { get; set; }
	}

	[XmlRoot(ElementName = "namespace")]
	public class XmlNamespace
	{
		[XmlAttribute(AttributeName = "prefix")]
		public string Prefix { get; set; }

		[XmlAttribute(AttributeName = "uri")]
		public string UniformResourceIdentifier { get; set; }
	}

	[XmlRoot(ElementName = "namespaces")]
	public class XmlNamespaces
	{
		public XmlNamespaces()
		{
			XmlNamespace = new Collection<XmlNamespace>();
		}

		[XmlElement(ElementName = "namespace")]
		public Collection<XmlNamespace> XmlNamespace { get; private set; }
	}

	public class FailOnErrorProperty
	{
		public FailOnErrorProperty()
		{
			FailOnError = true; // DefaultValue
		}

		[XmlAttribute(AttributeName = "failonerror")]
		public bool FailOnError { get; set; }
	}
	
	public class BackupProperty 
	{
		public BackupProperty()
		{
			Backup = true; // DefaultValue
		}

		[XmlAttribute(AttributeName = "backup")]
		public bool Backup { get; set; }
	}

	public class FailOnErrorAndBackupProperty : FailOnErrorProperty
	{
		public FailOnErrorAndBackupProperty()
		{
			Backup = true; // DefaultValue
		}

		[XmlAttribute(AttributeName = "backup")]
		public bool Backup { get; set; }
	}
}
