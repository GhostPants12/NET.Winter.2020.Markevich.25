using System;
using BLL.Interfaces;

namespace ProtoSOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlService = (IConverter)new DependencyResolver.DependencyResolver().CreateXmlDomServiceProvider().GetService(typeof(IConverter));
            xmlService.Convert("url.txt", "urls.xml");
        }
    }
}
