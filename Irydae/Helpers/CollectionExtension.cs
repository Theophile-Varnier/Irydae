using System;
using System.Collections.Generic;
using System.Linq;

namespace Irydae.Helpers
{
    public static class CollectionExtension
    {
        public static T GetOrAddNew<T>(this ICollection<T> collection, Func<T, bool> predicate, Func<T> constructor) where T : class
        {
            T res = collection.FirstOrDefault(predicate) ?? constructor();
            return res;
        }
    }
}