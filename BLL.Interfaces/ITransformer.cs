using System;

namespace BLL.Interfaces
{
    /// <summary>Interface for transformer class.</summary>
    /// <typeparam name="T">Type to transform to.</typeparam>
    public interface ITransformer<T>
    {
        /// <summary>Transforms the specified value to another type.</summary>
        /// <param name="value">The value.</param>
        /// <returns>Transformed value of another type.</returns>
        T Transform(string value);
    }
}
