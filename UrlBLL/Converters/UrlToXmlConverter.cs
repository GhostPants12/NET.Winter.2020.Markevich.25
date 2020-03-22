using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BLL.Interfaces;
using DAL.Interfaces;
using Types;

namespace UrlBLL
{
    public class UrlToXmlConverter : IConverter
    {
        private IReader reader;
        private IWriter<URLContainer> writer;
        private ITransformer<URLContainer> transformer;
        private ILogger logger;

        public UrlToXmlConverter(IReader reader, IWriter<URLContainer> writer, ITransformer<URLContainer> transformer, ILogger logger)
        {
            this.reader = reader;
            this.writer = writer;
            this.transformer = transformer;
            this.logger = logger;
        }

        public void Convert(string sourcePath, string resultPath)
        {
            List<URLContainer> toWriteList = new List<URLContainer>();
            foreach (var url in reader.ReadInfo(sourcePath))
            {
                try
                {
                    toWriteList.Add(transformer.Transform(url));
                }
                catch (Exception e)
                {
                    logger.Log(e.Message);
                }
            }

            this.writer.WriteToXml(resultPath, new ReadOnlyCollection<URLContainer>(toWriteList));
        }
    }
}
