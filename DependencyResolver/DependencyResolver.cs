using System;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Types;
using UrlBLL;
using UrlDAL;

namespace DependencyResolver
{
    public class DependencyResolver
    {
        public IServiceProvider CreateXmlServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConverter, UrlToXmlConverter>()
                .AddSingleton<ITransformer<URLContainer>, URLTransformer>() 
                .AddSingleton<ILogger, WrongURLLogger>()
                .AddSingleton<IReader, URLReader>()
                .AddSingleton<IWriter<URLContainer>, URLXmlWriter>()
                .BuildServiceProvider();
            return serviceProvider;
        }

        public IServiceProvider CreateXmlDomServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConverter, UrlToXmlConverter>()
                .AddSingleton<ITransformer<URLContainer>, URLTransformer>() 
                .AddSingleton<ILogger, WrongURLLogger>()
                .AddSingleton<IReader, URLReader>()
                .AddSingleton<IWriter<URLContainer>, URLXmlDom>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
