namespace MonitorControl.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides extension methods for <see cref="ICollection{T}"/>.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds each item in <paramref name="additions"/> to <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="collection">The collection to which items should be added.</param>
        /// <param name="additions">The collection of items to be added.</param>
        /// <remarks>
        /// This is an <i>O(n)</i> operation where <i>n</i> is <code>additions.Count</code>.
        /// </remarks>
        public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> additions)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (additions == null)
                throw new ArgumentNullException("additions");

            if (collection.IsReadOnly)
                throw new InvalidOperationException("collection is read only");

            foreach (var item in additions)
                collection.Add(item);
        }

        /// <summary>
        /// Removes all items sepcified by <paramref name="predicate"/> and returns them.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="collection">The collection from which to remove items.</param>
        /// <param name="predicate">The predicate determining which items to remove.</param>
        /// <returns>A collection of the removed items.</returns>
        /// <remarks>
        ///	This is an <i>O(nk)</i> operation where <i>n</i> is <code>collection.Count</code> and <i>k</i> is the
        ///	operational time of the implementation of <see cref="ICollection{T}.Remove"/>.
        /// </remarks>
        public static IEnumerable<T> RemoveAll<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            if (collection.IsReadOnly)
                throw new InvalidOperationException("collection is read only");

            // we can't possibly remove more than the entire list.
            var removals = new List<T>(collection.Count);

            // this is an O(n + m * k) operation where n is collection.Count, 
            // m is removals.Count, and K is the removal operation time. Because 
            // we know n >= m, this is an O(n + n * k) operation or just O(n * k).

            foreach (var item in collection)
                if (predicate(item))
                    removals.Add(item);

            foreach (var item in removals)
                collection.Remove(item);

            return removals;
        }

        /// <summary>
        /// Removes all items in <paramref name="removals"/> from <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collections.</typeparam>
        /// <param name="collection">The collection from which to remove items.</param>
        /// <param name="removals">The collection of items to remove.</param>
        /// <remarks>
        /// This is an <i>O(nk)</i> operation where <i>n</i> is <code>removals.Count</code> and
        /// <i>k</i> is the operational time of the implementation of <see cref="ICollection{T}.Remove"/>.
        /// </remarks>
        public static void RemoveAll<T>(this ICollection<T> collection, IEnumerable<T> removals)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (removals == null)
                throw new ArgumentNullException("removals");

            if (collection.IsReadOnly)
                throw new InvalidOperationException("collection is read only");

            foreach (var item in removals)
                collection.Remove(item);
        }

        public static void RemoveAll<T>(this Collection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            while (collection.Count > 0)
                collection.RemoveAt(collection.Count - 1);
        }

    }
}
