using System.Collections.Generic;
using UIPackageManager.Models;

namespace UIPackageManager.ViewModels
{
	public class PackageListViewModel : ViewModelBase
	{
		public PackageListViewModel(RelayCommand<string> navCommand)
		{
			NavigationViewModel = new NavigationViewModel(navCommand, "InnovatorSettings", "PackagePartSelection");
		}

		public IList<ArasPackage> Packages
		{
			get
			{
				return new List<ArasPackage>()
				{
					new ArasPackage() { Name = "Package1"},
					new ArasPackage() { Name = "Package2"},
					new ArasPackage() { Name = "Package3"},
				};
			}
		}

		public override int Width
		{
			get { return 1100; }
		}

		public override int Height
		{
			get { return 600; }
		}

		public NavigationViewModel NavigationViewModel { get; private set; }
	}
}
