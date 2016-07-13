using System.Xml.Linq;

namespace MagicCardMarket.Cache
{
    public interface ICache
    {
        bool Contains(string category, int id);
        void Set(string category, int id, XDocument xml);
        XDocument Get(string category, int id);
    }
}
