using System.Collections.Generic;

namespace MagicCardMarket.ConsoleApp
{
    public static class ListExtensions
    {
        public static void AddIfNotExists<T>(this List<T> list, T item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }
    }
}
