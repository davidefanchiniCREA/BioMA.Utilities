using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// Thrown by <see cref="OrderedSetOnceFixedKeyEnumerable{TKey, TValue}.Set(TKey, TValue)">
    /// OrderedSetOnceFixedKeyEnumerable.Set(TKey, TValue)</see> when an attempnt is made
    /// to overwrite a Value already set under a given key.
    /// </summary>
    /// <typeparam name="T">Type of the key</typeparam>
    [Serializable]
    public class ItemOverwriteException<T> : ApplicationException
    {
        private T _ItemKey;
        /// <summary>
        /// The key for which an overwrite has been attempted.
        /// </summary>
        public T ItemKey { get { return _ItemKey; } }
        public ItemOverwriteException(T ItemKey) { _ItemKey = ItemKey; }
        public ItemOverwriteException(string message, T ItemKey) :
            base(message) { _ItemKey = ItemKey; }
        public ItemOverwriteException(string message, Exception inner, T ItemKey) :
            base(message, inner) { _ItemKey = ItemKey; }
    }
}
