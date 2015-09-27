using System.IO;
using PackageManager.Extensions;
using PackageManager.Models;
using PackageManager.Interfaces;

namespace PackageManager.Implementations
{
	internal class Archiver : IArchiver
	{
		internal Archiver() { }

		public OperationResult DecompressZip(string zipPath, bool isDeleteAfterDecompress)
		{
			Logger.Instance.WriteLog.Info("Decompress Zip started");
			OperationResult result = new OperationResult { Status = StatusResult.Success };
			try
			{
				FileManager.DecompressZip(zipPath, isDeleteAfterDecompress);
				Logger.Instance.WriteLog.Info("Decompress Zip finished");
			}
			catch (IOException e)
			{
				result.GetErrorFromException(e);
			}
			return result;
		}
	}
}
