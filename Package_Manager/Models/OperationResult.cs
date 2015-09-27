using System.Collections.Generic;

namespace PackageManager.Models
{
	public class OperationResult
	{
		public OperationResult()
		{
			WarningMessages = new List<string>();
			Status = StatusResult.Success; // Default value
		}

		public StatusResult Status { get; set; }
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
		public IList<string> WarningMessages { get; private set; }

		public void AddWarningMessage(string message)
		{
			WarningMessages.Add(message);
		}

		public void AddWarningMessage(IList<string> messages)
		{
			((List<string>)WarningMessages).AddRange(messages);
		}
	}

	public class OperationResult<T> : OperationResult
	{
		public T Result { get; set; }
	}
}
