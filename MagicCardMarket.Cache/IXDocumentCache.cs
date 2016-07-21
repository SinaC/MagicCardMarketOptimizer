using System.Xml.Linq;

namespace MagicCardMarket.Cache
{
    public interface IXDocumentCache
    {
        bool Contains(string category, int id);
        void Set(string category, int id, XDocument data);
        XDocument Get(string category, int id);
        void Remove(string category, int id);
        void Clear(string category);
    }
}
