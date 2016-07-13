namespace MagicCardMarket.Cache
{
    public interface ICache<T>
    {
        bool Contains(int id);
        void Set(int id, T data);
        T Get(int id);
        void Remove(int id);
        void Clear();
    }
}
