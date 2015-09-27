using System.IO;
using System.Xml;
using System.Xml.XPath;
using PackageManager.Implementations;
using PackageManager.Models;
using PackageManager.Tasks.Common;

namespace PackageManager.Tasks
{
	internal class XmlPokeTask : BaseTask
	{
		private readonly XmlPoke xmlPoke;

		public XmlPokeTask(XmlPoke xmlPoke)
			: base(xmlPoke.FailOnError)
		{
			this.xmlPoke = xmlPoke;
		}

		protected override void ExecuteTask()
		{
			string xmlPath = Path.Combine(PackageManagerWorkspace.ArasApplicationPath, xmlPoke.File);
			ModifyNodeValueByXPath(xmlPath, xmlPoke.XPath, xmlPoke.Value, xmlPoke.Namespaces, xmlPoke.PreserveWhiteSpace);
		}

		public static void ModifyNodeValueByXPath(string xmlPath, string xPath, string newValue, XmlNamespaces xmlNamespaces, bool preserveWhitespace = false)
		{
			XmlDocument doc = XmlService.LoadDocument(xmlPath, preserveWhitespace);
			XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(doc.NameTable);
			if (xmlNamespaces != null)
			{
				foreach (XmlNamespace nms in xmlNamespaces.XmlNamespace)
				{
					xmlnsManager.AddNamespace(nms.Prefix, nms.UniformResourceIdentifier);
				}
			}
			XPathNavigator node = doc.CreateNavigator().SelectSingleNode(xPath, xmlnsManager);
			if (node != null)
			{
				node.SetValue(newValue);
				doc.Save(xmlPath);
			}
		}
	}
}
