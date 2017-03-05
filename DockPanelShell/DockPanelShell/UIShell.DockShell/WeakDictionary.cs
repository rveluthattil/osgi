//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Collections.Generic;

//using DockShell.Properties;

namespace DockShell
{
    /// <summary>
    /// Represents a dictionary which stores the keys and values as weak references instead of strong
    /// references. Null values are supported.
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class WeakDictionary<TKey, TValue>
    {
        private readonly Dictionary<object, WeakReference> _inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakDictionary{TKey, TValue}"/> class.
        /// </summary>
        public WeakDictionary()
        {
            var comparer = new WeakReferenceComparer<TKey>();
            _inner = new Dictionary<object, WeakReference>(comparer);
        }

        /// <summary>
        /// Returns a count of the number of items in the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                CleanAbandonedItems();
                return _inner.Count;
            }
        }

        /// <summary>
        /// Retrieves a value from the dictionary.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>The value in the dictionary.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the key does exist in the dictionary.
        /// Since the dictionary contains weak references, the key may have been removed by the
        /// garbage collection of the object.</exception>
        public TValue this[TKey key]
        {
            get
            {
                TValue local;
                if (!TryGet(key, out local))
                {
                    throw new KeyNotFoundException();
                }
                return local;
            }
        }

        /// <summary>
        /// Adds a new item to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException("key");

            TValue local;
            if (TryGet(key, out local))
            {
                throw new ArgumentException("Resources.KeyAlreadyPresentInDictionary");
            }

            var weakKey = new WeakKeyReference(key);

            var weakValue = new WeakReference(EncodeNullObject(value));

            _inner.Add(weakKey, weakValue);
        }

        private object EncodeNullObject(object value)
        {
            if (value == null)
            {
                return typeof (NullObject);
            }
            return value;
        }

        private TObject DecodeNullObject<TObject>(object innerValue)
        {
            if (innerValue == typeof (NullObject))
            {
                return default(TObject);
            }
            return (TObject) innerValue;
        }

        /// <summary>
        /// Determines if the dictionary contains the value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(TValue value)
        {
            foreach (WeakReference weakValue in _inner.Values)
            {
                if (weakValue.IsAlive && weakValue.Target == (object) value)
                {
                    return true;
                }
            }
            return false;
        }

        private void CleanAbandonedItems()
        {
            var list = new List<object>();
            foreach (var pair in _inner)
            {
                var key = (WeakReference) pair.Key;
                if (!key.IsAlive || !pair.Value.IsAlive)
                {
                    list.Add(key);
                }
            }
            foreach (TKey local in list)
            {
                _inner.Remove(local);
            }
        }

        /// <summary>
        /// Determines if the dictionary contains a value for the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            TValue local;
            return TryGet(key, out local);
        }

        /// <summary>
        /// Removes an item from the dictionary.
        /// </summary>
        /// <param name="key">The key of the item to be removed.</param>
        /// <returns>Returns true if the key was in the dictionary; return false otherwise.</returns>
        public bool Remove(TKey key)
        {
            return _inner.Remove(key);
        }

        /// <summary>
        /// Attempts to get a value from the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>Returns true if the value was present; false otherwise.</returns>
        public bool TryGet(TKey key, out TValue value)
        {
            WeakReference reference;
            value = default(TValue);
            if (!_inner.TryGetValue(key, out reference))
            {
                return false;
            }
            object innerValue = reference.Target;
            if (innerValue == null)
            {
                _inner.Remove(key);
                return false;
            }
            value = DecodeNullObject<TValue>(innerValue);
            return true;
        }

        #region Nested type: NullObject

        private class NullObject
        {
        }

        #endregion

        #region Nested type: WeakKeyReference

        /// <summary>
        /// Mantains the HashCode to be used when the key reference is no longer alive.
        /// </summary>
        private class WeakKeyReference : WeakReference
        {
            public WeakKeyReference(object key)
                : base(key)
            {
                HashCode = key.GetHashCode();
            }

            public int HashCode { get; set; }
        }

        #endregion

        #region Nested type: WeakReferenceComparer

        /// <summary>
        /// Compares strong references with WeakKeyReference targets.
        /// </summary>
        /// <remarks>
        /// Although the inner dictionary key holds WeakReferences, object is used as the key type.
        /// This allows to access the inner dictionary using the TKey strong reference.
        /// <example>
        /// _inner.Add(new WeakKeyReference(strongKey), weakValue));
        /// _inner[strongKey] == weakValue;
        /// </example>
        /// The WeakReferenceComparer is used by the inner dictionary to compare keys and 
        /// recognize weak references keys from its strong references.
        /// </remarks>
        /// <typeparam name="T">The type of the key.</typeparam>
        private class WeakReferenceComparer<T> : IEqualityComparer<object>
        {
            #region IEqualityComparer<object> Members

            public new bool Equals(object x, object y)
            {
                bool xIsDead, yIsDead;
                T first = GetTarget(x, out xIsDead);
                T second = GetTarget(y, out yIsDead);

                if (xIsDead)
                    return yIsDead ? x == y : false;

                if (yIsDead)
                    return false;

                return first.Equals(second);
            }

            public int GetHashCode(object obj)
            {
                var weakKey = obj as WeakKeyReference;
                if (weakKey != null)
                {
                    return weakKey.HashCode;
                }
                return obj.GetHashCode();
            }

            #endregion

            private static T GetTarget(object obj, out bool isDead)
            {
                var wref = obj as WeakKeyReference;
                T target;
                if (wref != null)
                {
                    target = (T) wref.Target;
                    isDead = !wref.IsAlive;
                }
                else
                {
                    target = (T) obj;
                    isDead = false;
                }
                return target;
            }
        }

        #endregion
    }
}