using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAL.Interfaces
{
    public interface IReader<T>
    {
        IEnumerable<T> ReadInfo(string path);
    }
}
