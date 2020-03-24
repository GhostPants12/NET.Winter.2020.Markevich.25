using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAL.Interfaces
{
    /// <summary>Interface for reader class.</summary>
    /// <typeparam name="T">Type of data to read.</typeparam>
    public interface IReader<T>
    {
        /// <summary>Reads the information from the file from the specified path.</summary>
        /// <param name="path">The path.</param>
        /// <returns>Sequence of T type data that was read from the file.</returns>
        IEnumerable<T> ReadInfo(string path);
    }
}
