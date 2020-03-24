using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Types;

namespace UrlBLL
{
    public class UrlToXmlConverterService : IConverterService
    {
        private IReader<string> reader;
        private IWriter<URLContainer> writer;
        private ITransformer<URLContainer> transformer;
        private NLog.ILogger logger;

        /// <summary>Initializes a new instance of the <see cref="UrlToXmlConverterService"/> class.</summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
        /// <param name="transformer">The transformer.</param>
        public UrlToXmlConverterService(IReader<string> reader, IWriter<URLContainer> writer, ITransformer<URLContainer> transformer)
        {
            this.reader = reader;
            this.writer = writer;
            this.transformer = transformer;
            this.logger = new WrongURLLogger().Logger;
        }

        /// <summary>Converts the specified file that contains URL addresses to XML file.</summary>
        /// <param name="sourcePath">The path of source file.</param>
        /// <param name="resultPath">The path of resulting file.</param>
        public void Convert(string sourcePath, string resultPath)
        {
            List<URLContainer> toWriteList = new List<URLContainer>();
            foreach (var url in this.reader.ReadInfo(sourcePath))
            {
                try
                {
                    toWriteList.Add(this.transformer.Transform(url));
                }
                catch (Exception e)
                {
                    this.logger.Error(e.Message);
                }
            }

            this.writer.Write(resultPath, toWriteList);
        }
    }
}
