namespace UIPackageManager.ViewModels
{
	public class RequiredPropertiesViewModel : ViewModelBase
	{
		public RequiredPropertiesViewModel(RelayCommand<string> navCommand)
		{
			NavigationViewModel = new NavigationViewModel(navCommand, "PackagePartSelection", "ProgresPage");
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
