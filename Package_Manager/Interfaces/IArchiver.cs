using PackageManager.Models;

namespace PackageManager.Interfaces
{
	internal interface IArchiver
	{
		OperationResult DecompressZip(string zipPath, bool isDeleteAfterDecompress);
	}
}
