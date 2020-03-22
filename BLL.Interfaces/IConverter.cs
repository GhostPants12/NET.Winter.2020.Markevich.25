using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IConverter
    {
        void Convert(string sourcePath, string resultPath);
    }
}
