using System;
using System.Collections.Generic;

namespace UIShell.PresentationCore
{
    /// <summary>
    /// 对Build出结果分类的Builder,如Toolbar的种类可能有 标准、帮助等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CatagorizedBuilder<T> : AbstractBuilder<T> where T : new()
    {
        public Dictionary<string, T> _catagorizedItems = new Dictionary<string, T>();

        public CatagorizedBuilder()
        {
            DefaultCatagory = Guid.NewGuid().ToString();
        }

        public IEnumerable<T> Items
        {
            get { return _catagorizedItems.Values; }
        }

        protected virtual string DefaultCatagory { get; private set; }

        public override void Reset()
        {
            foreach (var catagorizedItem in _catagorizedItems)
            {
                this.OnItemRemoved(catagorizedItem.Value);
            }

            _catagorizedItems.Clear();
        }

        public T GetOrCreate(string key)
        {
            T result;
            if (string.IsNullOrEmpty(key))
            {
                key = DefaultCatagory;
            }
            if (_catagorizedItems.TryGetValue(key, out result))
            {
                return result;
            }
            T newItem = _catagorizedItems[key] = new T();
            OnItemAdded(newItem);
            return newItem;
        }
    }
}