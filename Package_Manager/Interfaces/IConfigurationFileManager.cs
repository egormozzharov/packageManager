using PackageManager.Models;

namespace PackageManager.Interfaces
{
	internal interface IConfigurationFileManager
	{
		void AddOrUpdatePropertyConfig(string key, string newValue);
		PackageConfig PackageConfig { get; }
	}
}
