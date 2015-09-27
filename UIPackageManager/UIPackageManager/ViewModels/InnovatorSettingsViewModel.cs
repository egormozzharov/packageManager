using PackageManager.Models;

namespace UIPackageManager.ViewModels
{
	public class InnovatorSettingsViewModel : ViewModelBase
	{
		private readonly InputPropertyPackage _inputPropertyPackage;

		public InnovatorSettingsViewModel(InputPropertyPackage inputPropertyPackage, RelayCommand<string> navCommand)
		{
			_inputPropertyPackage = inputPropertyPackage;
			NavigationViewModel = new NavigationViewModel(navCommand, "", "PackageList");
		}

		public override int Width
		{
			get { return 500; }
		}

		public override int Height
		{
			get { return 500; }
		}

		public string InnovatorUrl
		{
			get { return _inputPropertyPackage.InnovatorUrl.Value; }
			set
			{
				if (value == _inputPropertyPackage.InnovatorUrl.Value)
					return;

				_inputPropertyPackage.InnovatorUrl.Value = value;
				base.OnPropertyChanged("InnovatorUrl");
			}
		}

		public string InnovatorDb
		{
			get { return _inputPropertyPackage.InnovatorDb.Value; }
			set
			{
				if (value == _inputPropertyPackage.InnovatorDb.Value)
					return;

				_inputPropertyPackage.InnovatorDb.Value = value;
				base.OnPropertyChanged("InnovatorDb");
			}
		}

		public string UserName
		{
			get { return _inputPropertyPackage.UserName.Value; }
			set
			{
				if (value == _inputPropertyPackage.UserName.Value)
					return;

				_inputPropertyPackage.UserName.Value = value;
				base.OnPropertyChanged("UserName");
			}
		}

		public string UserPassword
		{
			get { return _inputPropertyPackage.UserPassword.Value; }
			set
			{
				if (value == _inputPropertyPackage.UserPassword.Value)
					return;

				_inputPropertyPackage.UserPassword.Value = value;
				base.OnPropertyChanged("UserName");
			}
		}

		public NavigationViewModel NavigationViewModel { get; private set; }


	}
}
