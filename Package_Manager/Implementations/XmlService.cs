using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PackageManager.Implementations
{
	internal static class XmlService 
	{
		internal static XmlNodeList GetNodeListByXPath(string xmlPath, string xPath)
		{
			XmlDocument doc = LoadDocument(xmlPath);
			XmlNodeList result = doc.SelectNodes(xPath);
			return result;
		}

		internal static XmlDocument LoadDocument(string xmlPath, bool preserveWhitespace = false)
		{
			XmlDocument document = new XmlDocument();
			document.PreserveWhitespace = preserveWhitespace;
			document.Load(xmlPath);
			return document;
		}

		#region XmlSerialize/XmlDeserialize

		public static T GetObjectFromXml<T>(string xmlPath) where T : class
		{
			XmlSerializer reader = new XmlSerializer(typeof(T));
			using (StreamReader stringReader = new StreamReader(xmlPath))
			{
				T result = (T)reader.Deserialize(stringReader);
				return result;
			}
		}

		internal static string XmlSerializeToString(object objectInstance)
		{
			XmlSerializer serializer = new XmlSerializer(objectInstance.GetType());
			StringBuilder sb = new StringBuilder();

			using (TextWriter writer = new StringWriter(sb, CultureInfo.InvariantCulture))
			{
				serializer.Serialize(writer, objectInstance);
				return sb.ToString();
			}
		}

		internal static T XmlDeserializeFromString<T>(string objectData)
		{
			return (T)XmlDeserializeFromString(objectData, typeof(T));
		}

		private static object XmlDeserializeFromString(string objectData, Type type)
		{
			XmlSerializer serializer = new XmlSerializer(type);
			object result;

			using (TextReader reader = new StringReader(objectData))
			{
				result = serializer.Deserialize(reader);
			}
			return result;
		}

		#endregion // XmlSerialize/XmlDeserialize
	}
}
