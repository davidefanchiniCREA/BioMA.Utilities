using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// Read-only wrapper for <see cref="HashSet{T}">HashSet&lt;T&gt;</see>, backed by the modifiable <see cref="HashSet{T}">HashSet&lt;T&gt;</see>
    /// passed to the constructor. This means that if the wrapped <see cref="HashSet{T}">HashSet&lt;T&gt;</see> is modified, modifications are
    /// reflected in this instance.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [Serializable]
    public sealed class ReadOnlyHashSet<T> :
         ICollection<T>
    {
        private HashSet<T> fHashSet;

        /// <summary>
        /// Creates a new read-only hashset wrapper over a modifiable hashset.
        /// </summary>
        /// <param name="modifiableHashset">The modifiable <see cref="HashSet{T}">HashSet&lt;T&gt;</see> to wrap.</param>
        public ReadOnlyHashSet(HashSet<T> modifiableHashset)
        {
            if (modifiableHashset == null)
                throw new ArgumentNullException("modifiableHashset");

            fHashSet = modifiableHashset;
        }

        /// <summary>
        /// Gets the number of items in this <see cref="HashSet{T}">HashSet&lt;T&gt;</see>.
        /// </summary>
        public int Count
        {
            get
            {
                return fHashSet.Count;
            }
        }

        /// <summary>
        /// Returns an enumerator to iterate through all items in this
        /// <see cref="HashSet{T}">HashSet&lt;T&gt;</see>.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return fHashSet.GetEnumerator();
        }

        /// <summary>
        /// Returns true if the given item exists in this <see cref="HashSet{T}">HashSet&lt;T&gt;</see>, otherwise
        /// false.
        /// </summary>
        public bool Contains(T item)
        {
            return fHashSet.Contains(item);
        }

        /// <summary>
        /// Copies the values of this <see cref="HashSet{T}">HashSet&lt;T&gt;</see> to an array.
        /// </summary>
        public void CopyTo(T[] array)
        {
            fHashSet.CopyTo(array);
        }

        /// <summary>
        /// Copies the values of this <see cref="HashSet{T}">HashSet&lt;T&gt;</see> to an array, beginning
        /// at the given destination index.
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            fHashSet.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copies the given count items from this hashset to an array,
        /// beginning at the given destination index.
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex, int count)
        {
            fHashSet.CopyTo(array, arrayIndex, count);
        }

        /// <summary>
        /// Determines wheter this <see cref="HashSet{T}">HashSet&lt;T&gt;</see> is a proper subset of the other
        /// collection.
        /// </summary>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return fHashSet.IsProperSubsetOf(other);
        }

        /// <summary>
        /// Determines wheter this <see cref="HashSet{T}">HashSet&lt;T&gt;</see> is a proper superset of the other
        /// collection.
        /// </summary>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return fHashSet.IsProperSupersetOf(other);
        }

        /// <summary>
        /// Determines wheter this <see cref="HashSet{T}">HashSet&lt;T&gt;</see> is a subset of the other
        /// collection.
        /// </summary>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return fHashSet.IsSubsetOf(other);
        }

        /// <summary>
        /// Determines wheter this <see cref="HashSet{T}">HashSet&lt;T&gt;</see> is a superset of the other
        /// collection.
        /// </summary>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return fHashSet.IsSupersetOf(other);
        }

        /// <summary>
        /// Determines wheter this <see cref="HashSet{T}">HashSet&lt;T&gt;</see> overlaps the other collection.
        /// </summary>
        public bool Overlaps(IEnumerable<T> other)
        {
            return fHashSet.Overlaps(other);
        }

        #region ICollection<T> Members

        /// <summary>
        /// Throws <see cref="NotSupportedException">NotSupportedException</see>.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Throws <see cref="NotSupportedException">NotSupportedException</see>.
        /// </summary>
        public void Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// <c>true</c>.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Throws <see cref="NotSupportedException">NotSupportedException</see>.
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}