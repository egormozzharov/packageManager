using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PackageManager.Models;

namespace PackageManager.Tasks.Common
{
	internal class ParsingFileSet
	{
		private const string FindTypeFile = "*.";
		private const string FindFolderFile = "**";

		internal static IList<string> GetFilesFromFileSet(FileSet fileSet)
		{
			List<string> fileList = new List<string>();
			if (fileSet != null && (fileSet.Include.Any() || fileSet.Exclude.Any()))
			{
				string baseDirectory = String.IsNullOrEmpty(fileSet.BaseDirectory)
					? PackageManagerWorkspace.PackagePath
					: Path.Combine(PackageManagerWorkspace.PackagePath, fileSet.BaseDirectory);
				foreach (Include incl in fileSet.Include)
				{
					fileList.AddRange(FileList(incl.Name, baseDirectory));
				}
				foreach (string pathFile in fileSet.Exclude.SelectMany(exc => FileList(exc.Name, baseDirectory)))
				{
					fileList.Remove(pathFile);
				}
			}
			return fileList;
		}

		private static IList<string> FileList(string pattern, string baseDirectory)
		{
			IList<string> result = new List<string>();
			string typeFile = "*.*"; // default all type

			string pathBefore = pattern;
			if (pattern.Contains(FindFolderFile))
			{
				pathBefore = pattern.Substring(0, pattern.IndexOf(FindFolderFile, StringComparison.Ordinal));
			}
			string pathForFind = Path.Combine(baseDirectory, pathBefore);

			if (pattern.Contains(FindTypeFile) && !pattern.Contains(typeFile))
			{
				typeFile = pattern.Substring(pattern.IndexOf(FindTypeFile, StringComparison.Ordinal));
				pathForFind = pathForFind.Substring(0, pathForFind.IndexOf(FindTypeFile, StringComparison.Ordinal));
			}

			// if 'pathForFind' directory looking for files
			if (PathIsDirecotry(pathForFind))
			{
				result = Directory.GetFiles(pathForFind, typeFile, SearchOption.AllDirectories);
			}
			else
			{
				result.Add(pathForFind);
			}
			return ReplaceSlashInPath(result);
		}

		public static bool PathIsDirecotry(string path)
		{
			FileAttributes attr = File.GetAttributes(path);
			return (attr & FileAttributes.Directory) == FileAttributes.Directory;
		}

		private static IList<string> ReplaceSlashInPath(IList<string> listPath)
		{
			for (int i = 0; i < listPath.Count; i++)
			{
				listPath[i] = listPath[i].Replace("\\", "/");
			}
			return listPath;
		}
	}
}
