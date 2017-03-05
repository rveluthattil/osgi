using System.Collections.Generic;

namespace UIShell.Document
{
    public class AdapterFactoryCatalog<T> : IAdapterFactoryCatalog<T>
    {
        private readonly List<IAdapterFactory<T>> factories = new List<IAdapterFactory<T>>();

        #region IAdapterFactoryCatalog<T> Members

        public IList<IAdapterFactory<T>> Factories
        {
            get { return factories.AsReadOnly(); }
        }

        public IAdapterFactory<T> GetFactory(object element)
        {
            for (int i = factories.Count - 1; i >= 0; i--)
            {
                if (factories[i].Supports(element))
                {
                    return factories[i];
                }
            }

            return null;
        }

        public void RegisterFactory(IAdapterFactory<T> factory)
        {
            //Guard.ArgumentNotNull(factory, "factory");

            factories.Add(factory);
        }

        #endregion
    }
}