using System;
using System.Collections.Generic;

namespace MagicCardMarket.Extensions
{
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _getEquals;

        public LambdaComparer(Func<T, T, bool> equals)
        {
            _getEquals = equals;
        }

        public bool Equals(T x, T y)
        {
            return _getEquals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
