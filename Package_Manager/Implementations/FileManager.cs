using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using PackageManager.Models;

namespace PackageManager.Implementations
{
	internal static class FileManager
	{
		internal static void DecompressZip(string zipPath, bool isDeleteAfterDecompress)
		{
			string extractPath = Regex.Replace(zipPath.ToUpperInvariant(), ".ZIP", "");
			DecompressZip(zipPath, extractPath, isDeleteAfterDecompress);
		}

		internal static void DecompressZip(string zipPath, string extractPath, bool isDeleteAfterDecompress)
		{
			try
			{
				using (ZipArchive archive = ZipFile.OpenRead(zipPath))
				{
					foreach (ZipArchiveEntry entry in archive.Entries)
					{
						string pathForDelete = Path.Combine(PackageManagerWorkspace.PackagePath, entry.FullName);
						if (String.IsNullOrEmpty(entry.Name))
							DeleteDirectory(pathForDelete);
						else
							DeleteFile(pathForDelete);
					}
				}
				ZipFile.ExtractToDirectory(zipPath, extractPath);

				if (isDeleteAfterDecompress)
				{
					DeleteDirectory(zipPath);
				}
			}
			catch (IOException e)
			{
				Logger.Instance.WriteLog.Error(e);
				throw;
			}
		}

		internal static void DeleteFile(string filePath)
		{
			try
			{
				if (File.Exists(filePath))
					File.Delete(filePath);
			}
			catch (IOException e)
			{
				Logger.Instance.WriteLog.Error(e);
				throw;
			}
		}

		internal static void DeleteDirectory(string directoryPath)
		{
			try
			{
				if (Directory.Exists(directoryPath))
					Directory.Delete(directoryPath, true);
			}
			catch (IOException e)
			{
				Logger.Instance.WriteLog.Error(e);
				throw;
			}
		}

		internal static void CopyFiles(string sourcePath, string targetPath, string[] fileNames, bool overwriteFile)
		{
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);
			}
			foreach (string fileName in fileNames)
			{
				string sourceFile = Path.Combine(sourcePath, fileName);
				string destFile = Path.Combine(targetPath, fileName);

				if (!File.Exists(sourceFile))
					throw new FileNotFoundException("Source file does not exist or could not be found: " + sourceFile);

				File.Copy(sourceFile, destFile, overwriteFile);
			}
		}

		internal static void CopyFileByName(string sourcePath, string targetPath, string fileName, bool overwriteFile)
		{
			string[] fileNames = new[] { fileName };
			CopyFiles(sourcePath, targetPath, fileNames, overwriteFile);
		}

		internal static void CopyFileByPath(string filePath, string targetPath, bool overwriteFile)
		{
			DirectoryInfo dirInfo = new DirectoryInfo(filePath);
			string sourcePath = Path.GetDirectoryName(filePath);

			CopyFileByName(sourcePath, targetPath, dirInfo.Name, overwriteFile);
		}

		internal static void CreateDirectoryIfNotExists(string directoryPath)
		{
			if (directoryPath == null)
				throw new DirectoryNotFoundException("Source directory does not exist or could not be found.");

			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}
		}

		internal static void CreateFileIfNotExists(string filePath)
		{
			if (!File.Exists(filePath))
			{
				CreateDirectoryIfNotExists(Path.GetDirectoryName(filePath));
				new StreamWriter(filePath, true).Close();
			}
		}

		internal static bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		internal static bool DirectroryExists(string dirPath)
		{
			return Directory.Exists(dirPath);
		}

		internal static int CountFileInDirectory(string pathDirecotry)
		{
			int countFile = 0;
			if (DirectroryExists(pathDirecotry))
			{
				countFile = Directory.GetFiles(pathDirecotry).Length;
			}
			return countFile;
		}
	}
}
