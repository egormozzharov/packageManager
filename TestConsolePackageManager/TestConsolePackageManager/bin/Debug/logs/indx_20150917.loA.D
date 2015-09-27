2015-09-17 14:20:40,336 [ERROR] (10) - System.ArgumentNullException: Value cannot be null.
Parameter name: path1
   at System.IO.Path.Combine(String path1, String path2)
   at PackageManager.Models.PackageManagerWorkspace.get_PackageManager() in h:\Projects\Aras_Package_Manager\ArasPackageManager\Package_Manager\Models\PackageManagerWorkspace.cs:line 70
   at PackageManager.Implementations.DirectoryCreator..ctor(IFileManager fileManager) in h:\Projects\Aras_Package_Manager\ArasPackageManager\Package_Manager\Implementations\DirectoryCreator.cs:line 19
   at PackageManager.Implementations.ManagersContainer.get_DirectoryCreator() in h:\Projects\Aras_Package_Manager\ArasPackageManager\Package_Manager\Implementations\ManagersContainer.cs:line 46
   at PackageManager.PackageManagerInstall.StartDownload() in h:\Projects\Aras_Package_Manager\ArasPackageManager\Package_Manager\PackageManagerInstall.cs:line 27
