using System;
using BLL.Interfaces;

namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlService = (IConverter)new DependencyResolver.DependencyResolver().CreateXmlDomServiceProvider().GetService(typeof(IConverter));
            xmlService.Convert("url.txt", "urlsDOM.xml");
            xmlService = (IConverter)new DependencyResolver.DependencyResolver().CreateXmlServiceProvider().GetService(typeof(IConverter));
            xmlService.Convert("url.txt", "urls.xml");
        }
    }
}
