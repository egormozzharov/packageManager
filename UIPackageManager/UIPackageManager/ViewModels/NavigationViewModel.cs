
namespace UIPackageManager.ViewModels
{
	public class NavigationViewModel
	{
		public NavigationViewModel(RelayCommand<string> navCommand, string prevPageName, string nextPageName)
		{
			this.NavCommand = navCommand;
			this.NextPageName = nextPageName;
			this.PrevPageName = prevPageName;
		}

		public RelayCommand<string> NavCommand { get; private set; }

		public string NextPageName { get; private set; }

		public string PrevPageName { get; set; }
	}
}
