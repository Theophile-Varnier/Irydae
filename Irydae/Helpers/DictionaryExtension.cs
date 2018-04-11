using System;
using System.Collections.Generic;

namespace Irydae.Helpers
{
    public static class DictionaryExtension
    {
        public static T2 GetOrAddNew<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, Func<T2> constructor)
        {
            T2 res;
            if (!dictionary.TryGetValue(key, out res))
            {
                res = constructor();
            }
            return res;
        }
    }
}