using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    /// <summary>Interface for service classes that perform converting operation.</summary>
    public interface IConverterService
    {
        /// <summary>Converts the specified file to another file.</summary>
        /// <param name="sourcePath">The path of source file.</param>
        /// <param name="resultPath">The path of resulting file.</param>
        void Convert(string sourcePath, string resultPath);
    }
}
