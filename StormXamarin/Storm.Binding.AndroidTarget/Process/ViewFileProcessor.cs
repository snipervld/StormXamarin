﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XmlAttribute = Storm.Binding.AndroidTarget.Model.XmlAttribute;
using XmlElement = Storm.Binding.AndroidTarget.Model.XmlElement;

namespace Storm.Binding.AndroidTarget.Process
{
	class ViewFileProcessor
	{
		private const string VIEW_ID_FORMAT = "AutoGenerated_ViewId_{0}";
		private readonly Dictionary<string, string> _nsDictionary = new Dictionary<string, string>();

		public List<IdViewObject> Views = new List<IdViewObject>();

		private int _viewObjectId = 0;

		public XmlElement Read(string fileName, Dictionary<string, string> viewComponents)
		{
			Stack<XmlElement> elements = new Stack<XmlElement>();
			XmlElement current = null;

			using (XmlReader reader = XmlReader.Create(fileName))
			{
				bool continueWithoutRead = false;
				while (continueWithoutRead || reader.Read())
				{
					continueWithoutRead = false;
					if (reader.NodeType == XmlNodeType.Element)
					{
						bool isAutoClose = reader.IsEmptyElement;

						string elementName = reader.Name;
						if (viewComponents.ContainsKey(elementName))
						{
							elementName = viewComponents[elementName];
						}

						XmlElement childElement = new XmlElement
						{
							Name = elementName,
						};

						if (current != null)
						{
							current.Children.Add(childElement);
							if (!isAutoClose)
							{
								elements.Push(current);
							}
						}

						if (reader.HasAttributes)
						{
							reader.MoveToFirstAttribute();
							do
							{
								childElement.Attributes.Add(new XmlAttribute { FullName = reader.Name, Value = reader.Value });
							} while (reader.MoveToNextAttribute());
						}

						reader.Read();
						continueWithoutRead = !reader.HasValue;

						if (!isAutoClose)
						{
							current = childElement;
						}
					}
					else if (reader.NodeType == XmlNodeType.EndElement)
					{
						if (elements.Any())
						{
							current = elements.Pop();
						}
					}
				}
			}
			return current;
		}

		public void Write(XmlElement root, string outputFile)
		{
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				Indent = true,
				IndentChars = "\t",
				NewLineChars = "\n",
				NewLineHandling = NewLineHandling.Replace
			};
			_nsDictionary.Clear();
			using (XmlWriter writer = XmlWriter.Create(outputFile, settings))
			{
				writer.WriteStartDocument();

				WriteElement(writer, root);

				writer.WriteEndDocument();
			}
		}

		private void WriteElement(XmlWriter writer, XmlElement element)
		{
			if (element.Name != "Resources")
			{
				writer.WriteStartElement(ToLowerNamespace(element.Name));
				foreach (XmlAttribute attr in element.Attributes)
				{
					WriteAttribute(writer, attr);
				}

				foreach (XmlElement child in element.Children)
				{
					WriteElement(writer, child);
				}
				writer.WriteEndElement();
			}
		}

		private void WriteAttribute(XmlWriter writer, XmlAttribute attribute)
		{
			if (attribute.FullName.Contains(":"))
			{
				string[] splitted = attribute.FullName.Split(':');
				string ns = splitted[0];
				string name = splitted[1];

				if (ns == "xmlns")
				{
					_nsDictionary.Add(name, attribute.Value);
				}
				else
				{
					writer.WriteAttributeString(ns, name, _nsDictionary[ns], attribute.Value);
				}
			}
			else
			{
				writer.WriteAttributeString(attribute.FullName, attribute.Value);
			}
		}

		private string ToLowerNamespace(string input)
		{
			if (input.Contains("."))
			{
				int lastPosition = input.LastIndexOf('.');
				string namespaceName = input.Substring(0, lastPosition);
				string className = input.Substring(lastPosition);

				input = namespaceName.ToLowerInvariant() + className;
			}
			return input;
		}

		public void Display(XmlElement element, int indent = 0)
		{
			string indentString = new string(' ', indent * 2);
			Console.Write("{0}<{1}", indentString, element.Name);
			if (element.Attributes.Any())
			{
				Console.WriteLine();
				string attributeIndent = new string(' ', (indent + 2) * 2);

				foreach (XmlAttribute attr in element.Attributes)
				{
					Console.WriteLine("{0}{1}=\"{2}\"", attributeIndent, attr.FullName, attr.Value);
				}

				Console.WriteLine(element.Children.Any() ? "{0}>" : "{0}/>", attributeIndent);
			}
			else
			{
				Console.WriteLine(element.Children.Any() ? ">" : "/>");
			}

			if (element.Children.Any())
			{
				foreach (XmlElement child in element.Children)
				{
					Display(child, indent + 1);
				}
				Console.WriteLine("{0}</{1}>", indentString, element.Name);
			}
		}

		public Tuple<List<XmlAttribute>, List<IdViewObject>> ExtractBindingInformations(XmlElement element)
		{
			Views.Clear();

			List<XmlAttribute> attributes = _ExtractBindingInformations(element);

			return new Tuple<List<XmlAttribute>, List<IdViewObject>>(attributes, Views.ToList());
		}

		private List<XmlAttribute> _ExtractBindingInformations(XmlElement element)
		{
			List<XmlAttribute> bindings = new List<XmlAttribute>();
			if ("Resources".Equals(element.Name, StringComparison.OrdinalIgnoreCase))
			{
				return bindings;
			}

			string id = null;
			bool isFragment = "fragment".Equals(element.Name, StringComparison.CurrentCultureIgnoreCase);

			foreach (XmlAttribute attribute in element.Attributes)
			{
				string attributeName = attribute.LocalName.ToLowerInvariant();
				if (attributeName == "id")
				{
					if (id != null)
					{
						throw new Exception("Multiple id for same element");
					}

					id = attribute.Value;
					attribute.Value = "@+id/" + id;

					if (!isFragment)
					{
						Views.Add(new IdViewObject(element.Name, id));
					}
				}
				else if (attribute.Value.Trim().StartsWith("{Binding") || attributeName == "commandparameter")
				{
					bindings.Add(attribute);
				}
			}

			if (id == null && bindings.Any())
			{
				XmlAttribute attribute = new XmlAttribute();
				id = string.Format(VIEW_ID_FORMAT, _viewObjectId++);
				attribute.Value = "@+id/" + id;
				attribute.FullName = "android:id";

				element.Attributes.Add(attribute);
				if (!isFragment)
				{
					Views.Add(new IdViewObject(element.Name, id));
				}
			}

			foreach (XmlAttribute attribute in bindings)
			{
				attribute.AttachedId = id;
				element.Attributes.Remove(attribute);
			}

			foreach (XmlElement child in element.Children)
			{
				bindings.AddRange(_ExtractBindingInformations(child));
			}

			return bindings;
		}
	}
}
