using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using PackageManager.Interfaces;

namespace PackageManager.Implementations
{
	class GitWrapper : IGitWrapper
	{
		internal GitWrapper(string name, string email, string repositoryRoot)
		{
			this.repositoryRoot = repositoryRoot;
			Commiter = new Signature(name, email, DateTime.Now);
		}

		private readonly string repositoryRoot;
		private readonly string commitMessage = "Backup commit";

		private string GitFolderPath
		{
			get { return Path.Combine(repositoryRoot + ".git"); }
		}

		private Signature Commiter { get; set; }

		private bool HasUncommittedChanges
		{
			get
			{
				using (Repository)
				{
					RepositoryStatus status = Repository.RetrieveStatus();
					return status.IsDirty;
				}
			}
		}

		private Repository Repository
		{
			get
			{
				if (!Repository.IsValid(GitFolderPath))
				{
					Repository.Init(GitFolderPath);
				}
				return new Repository(GitFolderPath);
			}
		}

		public string CommitChanges()
		{
			string commitSha = null;
			if (HasUncommittedChanges)
			{
				using (Repository)
				{
					AddAllFilesToIndex();
					Commit commit = Repository.Commit(commitMessage, author: Commiter, committer: Commiter);
					commitSha = commit.Sha;
				}
			}
			return commitSha;
		}

		public void RevertLastCommit()
		{
			using (Repository)
			{
				Repository.Revert(Repository.Head.Tip, Commiter);
			}
		}

		public void RevertToCommit(string commitSha)
		{
			using (Repository)
			{
				Commit commit = Repository.Lookup<Commit>(commitSha);
				IEnumerable<Commit> commitsToRevert = Repository.Commits.Where(c => c.Committer.When > commit.Committer.When);
				foreach (Commit commitToRevert in commitsToRevert)
				{
					Repository.Revert(commitToRevert, Commiter);
				}
			}
		}

		private void AddAllFilesToIndex()
		{
			Repository.Stage("*");
		}
	}
}
