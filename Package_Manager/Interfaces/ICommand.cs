using PackageManager.Models;

namespace PackageManager.Interfaces
{
	internal interface ICommand
	{
		OperationResult Execute();
	}
}
