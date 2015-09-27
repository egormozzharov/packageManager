using System;
using PackageManager.Extensions;
using PackageManager.Models;

namespace PackageManager.Tasks.Common
{
	internal abstract class BaseTask
	{
		protected OperationResult OperationResult = new OperationResult();
		private readonly bool failOnError;

		internal BaseTask(bool failOnError)
		{
			this.failOnError = failOnError;
		}

		protected abstract void ExecuteTask();

		public OperationResult Execute()
		{
			try
			{
				ExecuteTask();
			}
			catch (Exception ex)
			{
				if (failOnError)
				{
					throw;
				}
				else
				{
					OperationResult.GetWarningFromException(ex);
				}
			}
			return OperationResult;
		}
	}
}
