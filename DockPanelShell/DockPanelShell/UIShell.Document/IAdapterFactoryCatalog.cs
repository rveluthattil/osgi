using System;
using System.Collections.Generic;

namespace UIShell.Document
{
    /// <summary>
    /// Represents an catalog for adapter factories of the type T.
    /// </summary>
    public interface IAdapterFactoryCatalog<T>
    {
        /// <summary>
        /// A list of the contained factories.
        /// </summary>
        IList<IAdapterFactory<T>> Factories { get; }

        /// <summary>
        /// Returns a factory for the passed object element.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if the object element is not supported by any of the registered factories.
        /// </exception>
        IAdapterFactory<T> GetFactory(object element);

        /// <summary>
        /// Registers a new factory in the catalog.
        /// </summary>
        void RegisterFactory(IAdapterFactory<T> factory);
    }
}