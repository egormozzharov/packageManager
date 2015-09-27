namespace PackageManager.Models
{
	public enum ProtocolTypeForDownloadPackage
	{
		Ftp, Http
	}

	public enum TypeProperty
	{
		Text, File, Directory, Url
	}

	public enum StatusResult
	{
		Success, Fail, Warning
	}

	public enum InstallationModuleType
	{
		DbModule,
		InnovatorServerModule,
		VaultServerModule,
		ConversionServerModule,
		OtherModule
	}
}
