using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAL.Interfaces
{
    public interface IReader
    {
        IEnumerable<string> ReadInfo(string path);
    }
}
