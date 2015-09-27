using System.Globalization;

namespace PackageManager.Extensions
{
	internal static class StringExtensions
	{
		static internal string GetStringByFormat(this string str, params object[] param)
		{
			string result = string.Format(CultureInfo.InvariantCulture, str, param);
			return result;
		}
	}
}
