using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BLL.Interfaces;
using DAL.Interfaces;
using Types;

namespace UrlBLL
{
    public class UrlToXmlConverterService : IConverterService
    {
        private IReader<string> reader;
        private IWriter<URLContainer> writer;
        private ITransformer<URLContainer> transformer;
        private ILogger logger;

        public UrlToXmlConverterService(IReader<string> reader, IWriter<URLContainer> writer, ITransformer<URLContainer> transformer, ILogger logger)
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
                    logger.LogError(e.Message);
                }
            }

            this.writer.WriteToXml(resultPath, toWriteList);
        }
    }
}
