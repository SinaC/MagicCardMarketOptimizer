namespace MagicCardMarket.Cache
{
    public interface ICache
    {
        bool Contains(string category, int id);
        void Set(string category, int id, string xml);
        string Get(string category, int id);
    }
}
