using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// Read-only wrapper for <see cref="IDictionary{K, T}">IDictionary&lt;TKey, TValue&gt;</see>, backed by the modifiable 
    /// <see cref="IDictionary{K, T}">IDictionary&lt;TKey, TValue&gt;</see>
    /// passed to the constructor. This means that if the wrapped <see cref="IDictionary{K, T}">IDictionary&lt;TKey, TValue&gt;</see>
    /// is modified, modifications are reflected in this instance.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    [Serializable]
    public sealed class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> _dictionary;

        /// <summary>
        /// Creates a new read-only <see cref="IDictionary{K, T}">IDictionary&lt;TKey, TValue&gt;</see> wrapper over a modifiable
        /// <see cref="IDictionary{K, T}">IDictionary&lt;TKey, TValue&gt;</see>.
        /// </summary>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> source)
        {
            _dictionary = source;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}">IEnumerator&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;</see> that can be used to iterate
        /// through the collection.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (KeyValuePair<TKey, TValue> item in _dictionary)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>
        /// contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>.</param>
        /// <returns><c>true</c> if the <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see> contains
        ///  an element with the key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if
        /// the key is found; otherwise, the default value for the type of the value
        ///  parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if this <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>
        ///  contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Determines whether this  <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see> contains
        /// a specific value.
        /// </summary>
        /// <param name="item">The object to locate in this  <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>.</param>
        /// <returns><c>true</c>  if item is found in  this  <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>;
        /// otherwise, <c>false</c>.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the this  <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see> to an
        /// <see cref="System.Array">array</see>, starting at a particular <see cref="System.Array">array</see> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="System.Array">array</see> that is the destination of the elements
        /// copied from this  <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>. The <see cref="System.Array">array</see> must
        /// have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get.</param>
        /// <returns>The element with the specified key.</returns>
        public TValue this[TKey key]
        {
            get
            {
                return _dictionary[key];
            }
        }

        /// <summary>
        ///  Gets an <see cref="ICollection{K}">ICollection&lt;TKey&gt;</see> containing the keys of this
        ///  <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                ReadOnlyCollection<TKey> keys = new ReadOnlyCollection<TKey>(new List<TKey>(_dictionary.Keys));
                return (ICollection<TKey>)keys;
            }
        }

        /// <summary>
        ///  Gets an <see cref="ICollection{K}">ICollection&lt;TKey&gt;</see> containing the values of this
        ///  <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                ReadOnlyCollection<TValue> values = new ReadOnlyCollection<TValue>(new List<TValue>(_dictionary.Values));
                return (ICollection<TValue>)values;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in this
        ///  <see cref="IDictionary{TKey,TValue}">IDictionary&lt;TKey,TValue&gt;</see>.
        /// </summary>
        public int Count
        {
            get
            {
                return _dictionary.Count;
            }
        }

        /// <summary>
        /// <c>true</c>.
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }


        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new NotSupportedException();
        }


        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new NotSupportedException();
        }


        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return this[key];
            }
            set
            {
                throw new NotSupportedException();
            }
        }


        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }


        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }


        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new NotSupportedException();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
