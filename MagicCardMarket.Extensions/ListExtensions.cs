using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicCardMarket.Extensions
{
    public static class ListExtensions
    {
        public static bool AddIfNotExists<T>(this List<T> list, T item, Func<T,T,bool> equalsFunc)
        {
            if (list.Any(x => equalsFunc(x,item)))
                return false;
            list.Add(item);
            return true;
        }
    }
}
