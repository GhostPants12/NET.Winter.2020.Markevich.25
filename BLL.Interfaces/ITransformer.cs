using System;

namespace BLL.Interfaces
{
    public interface ITransformer<T>
    {
        T Transform(string url);
    }
}
