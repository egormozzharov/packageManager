using System.Collections.Generic;
using PackageManager.Helpers;

namespace PackageManager.Models
{
	internal class PackageManagerParameters
	{
		private PackageManagerParameters()
		{
		}

		private static PackageManagerParameters instance;
		internal static PackageManagerParameters Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new PackageManagerParameters();
				}
				return instance;
			}
		}

		internal InputPropertyPackage InputProperty { get; private set; }

		internal OperationResult LoadingInitialData(InputPropertyPackage inputProperty)
		{
			Logger.Instance.WriteLog.Info("LoadingInitialData started");
			OperationResult result = ValidationValue.GetOperationResultAndCheckValueOnNullOrEmpty(new Dictionary<string, object>
			{
				// TODO: to check inputProperty on the basis of installed modules

				{"PackageDirectory", inputProperty.PackageDirectory.Value},
				{"PackageName", inputProperty.PackageName.Value},
				{"InnovatorDb", inputProperty.InnovatorDb.Value},
				{"InnovatorDirectory", inputProperty.InnovatorDirectory.Value}
			});
			InputProperty = inputProperty;
			Logger.Instance.WriteLog.Info("LoadingInitialData finished");
			return result;
		}
	}
}
