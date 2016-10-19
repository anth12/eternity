﻿using System;
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
    }
}
