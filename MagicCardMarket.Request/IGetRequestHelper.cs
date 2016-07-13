using System.Threading.Tasks;
using System.Xml.Linq;

namespace MagicCardMarket.Request
{
    public interface IGetRequestHelper
    {
        XDocument Get(string request);

        Task<XDocument> GetAsync(string request);
    }
}
