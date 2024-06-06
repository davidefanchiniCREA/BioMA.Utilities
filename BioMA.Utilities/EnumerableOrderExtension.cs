using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace JRC.IPSC.MARS.Utilities
{
    public enum SortDirection
    {
        Ascending, Descending
    }

    /// <summary>
    /// Extension methods for ordering enumerable collections.
    /// </summary>
    public static class EnumerableOrderExtension
    {
        /// <summary>
        /// Orders the elements of a sequence in ascending or descending order based on a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the key selector function.</typeparam>
        /// <param name="source">The sequence to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TSource}"/> whose elements are sorted according to the specified key and sort direction.</returns>
        public static IOrderedEnumerable<TSource> Order<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            SortDirection sortDirection)
        {
            // method name on IEnumerable/IOrderedEnumerable to call later
            string MethodName = null;

            // do we already have at least one sort on this collection?
            if (source is IOrderedEnumerable<TSource>)
            {
                if (sortDirection == SortDirection.Ascending)
                    MethodName = "ThenBy";
                else
                    MethodName = "ThenByDescending";
            }
            else // first sort on this collection
            {
                if (sortDirection == SortDirection.Ascending)
                    MethodName = "OrderBy";
                else
                    MethodName = "OrderByDescending";
            }
            MethodInfo method = typeof(Enumerable).GetMethods()
                .Single(m => m.Name == MethodName && m.MakeGenericMethod(typeof(int), typeof(int)).GetParameters().Length == 2);

            return method.MakeGenericMethod(typeof(TSource), typeof(TKey))
                .Invoke(source, new object[] { source, keySelector }) as IOrderedEnumerable<TSource>;
        }
    }
}
