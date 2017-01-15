using System;
using System.Collections.Generic;
using System.Linq;

namespace Eternity.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool None<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return !source.Any(predicate);
        }

        #region Navigation

        public static TType Previous<TType>(this IList<TType> source, TType element)
        {
            if (source.Contains(element))
            {
                var index = source.IndexOf(element);

                return source[index > 0 ? index-1 : 0];
            }
            return source.FirstOrDefault();
        }

        public static TType Next<TType>(this IList<TType> source, TType element)
        {
            if (source.Contains(element))
            {
                var index = source.IndexOf(element);

                return source[index < source.Count-1 ? index + 1 : 0];
            }
            return source.FirstOrDefault();
        }

        #endregion Navigation

        public static DateTime ClosestTo(this IEnumerable<DateTime> source, DateTime matchingDate)
        {
            return source.OrderBy(d => Math.Abs((d - matchingDate).Ticks))
                .FirstOrDefault();
        }
    }
}
