using System.Collections.Generic;
using System.Threading;

namespace MagicCardMarket.Cache
{
    public class MemoryCache<T> : ICache<T>
    {
        private readonly Dictionary<int, T> _cache = new Dictionary<int, T>();
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        #region ICache

        public bool Contains(int id)
        {
            _lock.EnterReadLock();
            bool found = _cache.ContainsKey(id);
            _lock.ExitReadLock();
            return found;
        }

        public void Set(int id, T data)
        {
            _lock.EnterWriteLock();
            if (_cache.ContainsKey(id))
                _cache[id] = data;
            else
                _cache.Add(id, data);
            _lock.ExitWriteLock();
        }

        public T Get(int id)
        {
            T value;
            _lock.EnterReadLock();
            bool found = _cache.TryGetValue(id, out value);
            _lock.ExitReadLock();
            return found ? value : default(T);
        }

        public void Remove(int id)
        {
            _lock.EnterWriteLock();
            if (_cache.ContainsKey(id))
                _cache.Remove(id);
            _lock.ExitWriteLock();
        }

        public void Clear()
        {
            _lock.EnterWriteLock();
            _cache.Clear();
            _lock.ExitWriteLock();
        }

        #endregion
    }
}
