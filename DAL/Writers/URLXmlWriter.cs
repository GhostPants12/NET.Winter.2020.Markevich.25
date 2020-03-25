using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using DAL.Interfaces;
using Types;

namespace UrlDAL
{
    public class URLXmlWriter : IWriter<URLContainer>
    {
        /// <summary>Writes the sequence of T to the XML file using XMLWriter.</summary>
        /// <param name="path">The path.</param>
        /// <param name="collection">The collection.</param>
        public virtual void Write(string path, IEnumerable<URLContainer> urls)
        {
            if (urls is null)
            {
                throw new ArgumentNullException($"{nameof(urls)} is null.");
            }

            using (XmlWriter xmlWriter = XmlWriter.Create(path, new XmlWriterSettings()
            {
                Indent = true,
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
