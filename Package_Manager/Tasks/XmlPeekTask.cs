using System.IO;
using System.Xml;
using PackageManager.Implementations;
using PackageManager.Interfaces;
using PackageManager.Models;
using PackageManager.Tasks.Common;

namespace PackageManager.Tasks
{
	internal class XmlPeekTask : BaseTask
	{
		private readonly XmlPeek xmlPeek;
		private readonly IConfigurationFileManager configurationFileManager;

		public XmlPeekTask(XmlPeek xmlPeek, IConfigurationFileManager configurationFileManager)
			: base(xmlPeek.FailOnError)
		{
			this.xmlPeek = xmlPeek;
			this.configurationFileManager = configurationFileManager;
		}

		protected override void ExecuteTask()
		{
			string xmlPath = Path.Combine(PackageManagerWorkspace.ArasApplicationPath, xmlPeek.File);
			XmlNode node = GetNodeByXPath(xmlPath, xmlPeek.XPath, xmlPeek.Namespaces, xmlPeek.NodeIndex);
			if (node != null)
			{
				configurationFileManager.AddOrUpdatePropertyConfig(xmlPeek.Property, node.Value);
			}
		}

		private static XmlNode GetNodeByXPath(string xmlPath, string xPath, XmlNamespaces xmlNamespaces, int nodeIndex)
		{
			XmlDocument doc = XmlService.LoadDocument(xmlPath);
			XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(doc.NameTable);
			if (xmlNamespaces != null)
			{
				foreach (XmlNamespace nms in xmlNamespaces.XmlNamespace)
				{
					xmlnsManager.AddNamespace(nms.Prefix, nms.UniformResourceIdentifier);
				}
			}
			XmlNodeList result = doc.SelectNodes(xPath, xmlnsManager);
			if (result != null && result.Count > 1)
			{
				return result[nodeIndex];
			}
			return result != null ? result[0] : null;
		}
	}
}
