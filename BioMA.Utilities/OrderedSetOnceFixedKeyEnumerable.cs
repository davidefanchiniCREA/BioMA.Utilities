using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// Represents a dictionary in which the keys are fixed at instantiation time,
    /// and for which, once a value has been configured for a key, it is
    /// not possibile to overwrite it. Guarantees iteration of the keys in the order
    /// they are configured in the constructor
    /// </summary>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    public class OrderedSetOnceFixedKeyEnumerable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TValue : class
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of this class, with the keys specified.
        /// </summary>
        /// <param name="keys">The keys of this instance</param>
        public OrderedSetOnceFixedKeyEnumerable(params TKey[] keys)
        {
            foreach (TKey _key in keys)
            {
                _keys.Add(_key);
                _internalDictionary.Add(_key, null);
            }
        }

        #endregion

        #region Public members

        /// <summary>
        /// The keys of this instance, in the order they are configured in the constructor
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get { return _keys.Select(a => a); }
        }

        /// <summary>
        /// Sets a value for a key.
        /// If the key is not present or has already a value, throws Exception
        /// </summary>
        /// <param name="key">The key for which to set a value</param>
        /// <param name="value">The value to be set</param>
        /// <exception cref="KeyNotFoundException">if the key is not acceptable</exception>
        /// <exception cref="ItemOverwriteException&lt;TKey&gt;">if the key has already a value</exception>
        public void Set(TKey key, TValue value)
        {
            TValue fake = _internalDictionary[key];
            if (fake != null) throw new ItemOverwriteException<TKey>(key);
            _internalDictionary.Remove(key);
            _internalDictionary.Add(key, value);
            version++;
        }

        public TValue this[TKey key]
        {
            get
            {
                return _internalDictionary[key];
            }
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Returns an enumerator for this instance
        /// </summary>
        /// <returns>enumerator for this instance</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator((Dictionary<TKey, TValue>)_internalDictionary, 2, _keys, version, this);
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator for this instance
        /// </summary>
        /// <returns>enumerator for this instance</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator((Dictionary<TKey, TValue>)_internalDictionary, 2, _keys, version, this);
        }

        #endregion

        #region IEnumerable implementation

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IDictionaryEnumerator, IEnumerator
        {
            internal const int DictEntry = 1;
            internal const int KeyValuePair = 2;
            private Dictionary<TKey, TValue> dictionary;
            private IList<TKey> _keys;
            OrderedSetOnceFixedKeyEnumerable<TKey, TValue> outerObj;
            private int version;
            private int index;
            private KeyValuePair<TKey, TValue> current;
            private int getEnumeratorRetType;
            internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType, IList<TKey> keys, int _version, OrderedSetOnceFixedKeyEnumerable<TKey, TValue> _outerObj)
            {
                this.dictionary = dictionary;
                this.version = _version;
                this.outerObj = _outerObj;
                this.index = 0;
                this.getEnumeratorRetType = getEnumeratorRetType;
                this.current = new KeyValuePair<TKey, TValue>();
                this._keys = keys;
            }

            public bool MoveNext()
            {
                if (this.version != this.outerObj.version)
                {
                    throw new InvalidOperationException("concurrent modification: iterator invalid");
                }
                if (index == _keys.Count)
                {
                    this.current = new KeyValuePair<TKey, TValue>();
                    return false;
                }
                this.current = new KeyValuePair<TKey, TValue>(_keys[index], this.dictionary[_keys[index]]);
                this.index++;
                return true;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return this.current;
                }
            }
            public void Dispose()
            {
            }

            object IEnumerator.Current
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._keys.Count + 1)))
                    {
                        throw new InvalidOperationException("out of bounds iteration");
                    }
                    if (this.getEnumeratorRetType == 1)
                    {
                        return new DictionaryEntry(this.current.Key, this.current.Value);
                    }
                    return new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
                }
            }
            void IEnumerator.Reset()
            {
                if (this.version != this.outerObj.version)
                {
                    throw new InvalidOperationException("concurrent modification: iterator invalid");
                }
                this.index = 0;
                this.current = new KeyValuePair<TKey, TValue>();
            }

            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._keys.Count + 1)))
                    {
                        throw new InvalidOperationException("out of bounds iteration");
                    }
                    return new DictionaryEntry(this.current.Key, this.current.Value);
                }
            }
            object IDictionaryEnumerator.Key
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._keys.Count + 1)))
                    {
                        throw new InvalidOperationException("out of bounds iteration");
                    }
                    return this.current.Key;
                }
            }
            object IDictionaryEnumerator.Value
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._keys.Count + 1)))
                    {
                        throw new InvalidOperationException("out of bounds iteration");
                    }
                    return this.current.Value;
                }
            }
        }

        #endregion

        #region Private members

        private IDictionary<TKey, TValue> _internalDictionary =
            new Dictionary<TKey, TValue>();
        private IList<TKey> _keys = new List<TKey>();
        private int version;

        #endregion
    }
}
