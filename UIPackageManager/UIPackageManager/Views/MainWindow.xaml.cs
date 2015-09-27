using System.Windows;
using UIPackageManager.ViewModels;

namespace UIPackageManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        public MainWindow()
		{
			MainWindowViewModel viewModel = new MainWindowViewModel();
	        this.DataContext = viewModel;
			InitializeComponent();
	        this.Closing += viewModel.OnWindowClosing;
		}
	}
}
