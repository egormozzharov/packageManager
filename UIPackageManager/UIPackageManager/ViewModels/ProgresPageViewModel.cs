namespace UIPackageManager.ViewModels
{
	public class ProgresPageViewModel : ViewModelBase
	{
		public ProgresPageViewModel(RelayCommand<string> navCommand)
		{
			NavigationViewModel = new NavigationViewModel(navCommand, "RequiredProperties", "");
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
