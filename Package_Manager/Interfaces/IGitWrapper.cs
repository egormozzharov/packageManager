namespace PackageManager.Interfaces
{
	internal interface IGitWrapper
	{
		string CommitChanges();
		void RevertLastCommit();
		void RevertToCommit(string commitSha);
	}
}
