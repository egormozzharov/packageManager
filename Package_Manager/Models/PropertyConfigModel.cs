using System.Collections.Generic;

namespace PackageManager.Models
{
	internal class PropertyConfigModel
	{
		internal PropertyConfigModel()
		{
			PropertyConfigDictionary = new Dictionary<string, string>();
		}

		internal PropertyConfigModel(IDictionary<string, string> properties)
		{
			PropertyConfigDictionary = new Dictionary<string, string>(properties);
			WasChanged = true;
		}

		internal bool WasChanged { get; set; }
		internal IDictionary<string, string> PropertyConfigDictionary { get; private set; }

		internal void AddOrUpdatePropertyConfig(string key, string newValue)
		{
			if (PropertyConfigDictionary.ContainsKey(key) && PropertyConfigDictionary[key] == newValue)
			{
				return;
			}

			PropertyConfigDictionary[key] = newValue;
			WasChanged = true;
		}
	}
}
