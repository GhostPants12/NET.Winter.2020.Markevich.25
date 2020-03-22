using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using DAL.Interfaces;
using Types;

namespace UrlDAL
{
    public class URLXmlDom : IWriter<URLContainer>
    {
        public void WriteToXml(string path, ReadOnlyCollection<URLContainer> collection)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration( "1.0", "UTF-8", null );
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore( xmlDeclaration, root );
            XmlElement urlAddresses = doc.CreateElement(string.Empty, "urlAddresses", string.Empty);
            doc.AppendChild(urlAddresses);
            foreach (var url in collection)
            {
                XmlElement urlAddress = doc.CreateElement(string.Empty, "urlAddress", string.Empty);
                XmlElement host = doc.CreateElement(String.Empty, "host", string.Empty);
                XmlAttribute name = doc.CreateAttribute("name");
                XmlText nameText = doc.CreateTextNode(url.Host);
                name.AppendChild(nameText);
                host.Attributes.Append(name);
                urlAddress.AppendChild(host);
                if (url.GetPath()?.Count >= 1)
                {
                    XmlElement uri = doc.CreateElement(string.Empty, "uri", String.Empty);
                    foreach (var segm in url.GetPath())
                    {
                        XmlElement segment = doc.CreateElement("segment");
                        segment.AppendChild(doc.CreateTextNode(segm));
                        uri.AppendChild(segment);
                    }

                    urlAddress.AppendChild(uri);
                }

                if (url.GetParameters()?.Count >= 1)
                {
                    XmlElement parameters = doc.CreateElement(string.Empty, "parameters", String.Empty);
                    foreach (var param in url.GetParameters())
                    {
                        XmlElement parameter = doc.CreateElement("parameter");
                        XmlAttribute value = doc.CreateAttribute("value");
                        value.AppendChild(doc.CreateTextNode(param.Value));
                        XmlAttribute key = doc.CreateAttribute("key");
                        key.AppendChild(doc.CreateTextNode(param.Key));
                        parameter.Attributes.Append(value);
                        parameter.Attributes.Append(key);
                        parameters.AppendChild(parameter);
                    }

                    urlAddress.AppendChild(parameters);
                }

                urlAddresses.AppendChild(urlAddress);
            }

            doc.Save(path);
        }
    }
}
