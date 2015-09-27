using System.Windows;

namespace UIPackageManager.ViewModels
{
	public class PackagePartSelectionViewModel : ViewModelBase
	{
		public PackagePartSelectionViewModel(RelayCommand<string> navCommand)
		{
			NavigationViewModel = new NavigationViewModel(navCommand, "PackageList", "RequiredProperties");
		}

		public override int Width
		{
			get { return 500; }
		}

		public override int Height
		{
			get { return 500; }
		}

		public NavigationViewModel NavigationViewModel { get; private set; }
	}
}
