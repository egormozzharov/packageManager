using System;
using PackageManager.Models;

namespace PackageManager.Extensions
{
	internal static class OperationResultExtensions
	{
		static internal OperationResult GetErrorFromString(this OperationResult result, string msg)
		{
			result.ErrorMessage = msg;
			result.Status = StatusResult.Fail;
			Logger.Instance.WriteLog.Error(msg);
			return result;
		}

		static internal OperationResult GetWarningFromString(this OperationResult result, string msg)
		{
			result.AddWarningMessage(msg);
			result.Status = StatusResult.Warning;
			Logger.Instance.WriteLog.Warn(msg);
			return result;
		}

		static internal OperationResult GetErrorFromException(this OperationResult result, Exception e)
		{
			Logger.Instance.WriteLog.Error(e);
			return GetErrorFromString(result, e.Message);
		}

		static internal OperationResult GetWarningFromException(this OperationResult result, Exception e)
		{
			Logger.Instance.WriteLog.Error(e);
			return GetWarningFromString(result, e.Message);
		}

		static internal OperationResult СombineResult(this OperationResult result, OperationResult newResult)
		{
			if (newResult == null)
			{
				return result;
			}
			if (newResult.Status == StatusResult.Warning)
			{
				result.AddWarningMessage(newResult.WarningMessages);
				result.Status = newResult.Status;
			}
			if (newResult.Status == StatusResult.Fail)
			{
				result.ErrorMessage = newResult.ErrorMessage;
				result.Status = newResult.Status;
			}
			return result;
		}
	}
}
