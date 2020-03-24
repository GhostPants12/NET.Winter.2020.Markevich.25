using System;
using BLL.Interfaces;

namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlService = (IConverterService)new DependencyResolver.DependencyResolver().CreateXmlDomServiceProvider().GetService(typeof(IConverterService));
            xmlService.Convert("url.txt", "urlsDOM.xml");
            xmlService = (IConverterService)new DependencyResolver.DependencyResolver().CreateXmlServiceProvider().GetService(typeof(IConverterService));
            xmlService.Convert("url.txt", "urls.xml");
        }
    }
}
