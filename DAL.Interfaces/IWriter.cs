using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace DAL.Interfaces
{
    /// <summary>Interface for writer class.</summary>
    /// <typeparam name="T">Type of value to write.</typeparam>
    public interface IWriter<T>
    {
        /// <summary>Writes the sequence of T to the file with the specified path.</summary>
        /// <param name="path">The path.</param>
        /// <param name="collection">The collection.</param>
        void Write(string path, IEnumerable<T> collection);
    }
}
