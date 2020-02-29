using JML.Utility.Exceptions;
using System;
using System.Collections.Generic;

namespace JML.Utility.CollectionExtensions
{
    public static class DisctinctBy
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> sourceCollection,
            Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            sourceCollection.NotNull();
            keySelector.NotNull();

            return sourceCollection.DistinctBySelector(keySelector, comparer);
        }


        private static IEnumerable<T> DistinctBySelector<T, TKey>(this IEnumerable<T> sourceCollection,
            Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var keyTable = new HashSet<TKey>(comparer);

            foreach (var item in sourceCollection)
            {
                if (keyTable.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }
    }
}
