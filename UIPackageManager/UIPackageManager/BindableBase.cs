using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UIPackageManager
{
	public class BindableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { }; 

		protected virtual void SetProperty<T>(ref T member, T val,
			[CallerMemberName] string propertyName = null)
		{
			if (!object.Equals(member, val))
			{
				member = val;
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
