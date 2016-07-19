using System.Xml.Linq;

namespace MagicCardMarket.Request
{
    public class PagedResult
    {
        public XDocument Data { get; set; }

        public int CurrentIndex { get; set; }

        public int MaxIndex { get; set; }

        public bool IsComplete { get; set; }
    }
}
