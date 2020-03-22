using System;
using System.Collections.ObjectModel;
using System.Xml;
using DAL.Interfaces;
using Types;

namespace UrlDAL
{
    public class URLXmlWriter : IWriter<URLContainer>
    {
        public void WriteToXml(string path, ReadOnlyCollection<URLContainer> urls)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(path, new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
            }))
            {
                xmlWriter.WriteStartElement("urlAddresses");
                foreach (var urlAddress in urls)
                {
                    xmlWriter.WriteStartElement("urlAddress");
                    xmlWriter.WriteStartElement("host");
                    xmlWriter.WriteAttributeString("name", urlAddress.Host);
                    xmlWriter.WriteEndElement();
                    if (urlAddress.GetPath()?.Count >= 1)
                    {
                        xmlWriter.WriteStartElement("uri");
                        foreach (var segment in urlAddress.GetPath())
                        {
                            xmlWriter.WriteElementString("segment", segment);
                        }

                        xmlWriter.WriteEndElement();
                    }

                    if (urlAddress.GetParameters()?.Count >= 1)
                    {
                        xmlWriter.WriteStartElement("parameters");
                        foreach (var parameter in urlAddress.GetParameters())
                        {
                            xmlWriter.WriteStartElement("parameter");
                            xmlWriter.WriteAttributeString("value", parameter.Value);
                            xmlWriter.WriteAttributeString("key", parameter.Key);
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }
        }
    }
}
