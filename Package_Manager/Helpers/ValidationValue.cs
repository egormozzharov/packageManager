using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using PackageManager.Models;

namespace PackageManager.Helpers
{
	internal class ValidationValue
	{
		private ValidationValue() { }

		public static string CheckValueOnNullOrEmpty(Dictionary<string, object> values)
		{
			StringBuilder error = new StringBuilder();
			foreach (var item in values)
			{
				if (item.Value is string && String.IsNullOrEmpty((string)item.Value))
				{
					error.AppendLine(string.Format(CultureInfo.InvariantCulture, "Value cannot be null or empty. Parameter: '{0}'.", item.Key));
				}
				else if (item.Value == null)
				{
					error.AppendLine(string.Format(CultureInfo.InvariantCulture, "Value cannot be null. Parameter: '{0}'.", item.Key));
				}
			}
			return error.ToString();
		}

		public static OperationResult GetOperationResultAndCheckValueOnNullOrEmpty(Dictionary<string, object> values)
		{
			OperationResult result = new OperationResult();
			result.ErrorMessage = CheckValueOnNullOrEmpty(values);
			if (result.ErrorMessage.Length > 0)
			{
				result.Status = StatusResult.Fail;
			}
			return result;
		}
	}
}
