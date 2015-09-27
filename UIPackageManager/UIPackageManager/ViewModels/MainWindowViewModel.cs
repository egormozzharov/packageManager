using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using PackageManager.Models;

namespace UIPackageManager.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private static ObservableCollection<ViewModelBase> _workspaces;
		private InputPropertyPackage _inputPropertyPackage;

		public MainWindowViewModel()
		{
			_inputPropertyPackage = new InputPropertyPackage();
			CurrentViewModel = ShowInnovatorSettings;
		}

		public ObservableCollection<ViewModelBase> Workspaces
		{
			get
			{
				if (_workspaces == null)
				{
					_workspaces = new ObservableCollection<ViewModelBase>();
				}
				return _workspaces;
			}
		}

		private ViewModelBase currentViewModel;
		public ViewModelBase CurrentViewModel
		{
			get
			{
				return currentViewModel;
			}
			set
			{
				SetProperty(ref currentViewModel, value);
			}
		}

		public void OnNav(string destination)
		{
			//MessageBox.Show("");
			switch (destination)
			{
				case "InnovatorSettings":
					CurrentViewModel = ShowInnovatorSettings;
					break;
				case "PackageList":
					CurrentViewModel = ShowPackageList;
					break;
				case "PackagePartSelection":
					CurrentViewModel = ShowPackagePartSelection;
					break;
				case "RequiredProperties":
					CurrentViewModel = ShowRequiredProperties;
					break;
				case "ProgresPage":
					CurrentViewModel = ShowProgresPageViewModel;
					break;
			}
		}

		public void OnWindowClosing(object sender, EventArgs e)
		{
			MessageBox.Show("Are you sure you want to cansel installation?");
		}

		private InnovatorSettingsViewModel ShowInnovatorSettings
		{
			get
			{
				InnovatorSettingsViewModel workspace = ViewModelExist<InnovatorSettingsViewModel>();
				if (workspace == null)
				{
					workspace = new InnovatorSettingsViewModel(_inputPropertyPackage, new RelayCommand<string>(OnNav));
					Workspaces.Add(workspace);
				}
				return workspace;
			}
		}

		private PackageListViewModel ShowPackageList
		{
			get
			{
				PackageListViewModel workspace = ViewModelExist<PackageListViewModel>();
				if (workspace == null)
				{
					workspace = new PackageListViewModel(new RelayCommand<string>(OnNav));
					Workspaces.Add(workspace);
				}
				return workspace;
			}
		}

		private PackagePartSelectionViewModel ShowPackagePartSelection
		{
			get
			{
				PackagePartSelectionViewModel workspace = ViewModelExist<PackagePartSelectionViewModel>();
				if (workspace == null)
				{
					workspace = new PackagePartSelectionViewModel(new RelayCommand<string>(OnNav));
					Workspaces.Add(workspace);
				}
				return workspace;
			}
		}

		private RequiredPropertiesViewModel ShowRequiredProperties
		{
			get
			{
				RequiredPropertiesViewModel workspace = ViewModelExist<RequiredPropertiesViewModel>();
				if (workspace == null)
				{
					workspace = new RequiredPropertiesViewModel(new RelayCommand<string>(OnNav));
					Workspaces.Add(workspace);
				}
				return workspace;
			}
		}

		private ProgresPageViewModel ShowProgresPageViewModel
		{
			get
			{
				ProgresPageViewModel workspace = ViewModelExist<ProgresPageViewModel>();
				if (workspace == null)
				{
					workspace = new ProgresPageViewModel(new RelayCommand<string>(OnNav));
					Workspaces.Add(workspace);
				}
				return workspace;
			}
		}

		private T ViewModelExist<T>() where T: class 
		{
			T workspace = Workspaces.FirstOrDefault(vm => vm is T) as T;
			return workspace;
		}
	}
}
