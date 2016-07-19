using System.Threading.Tasks;
using System.Xml.Linq;

namespace MagicCardMarket.Request
{
    public interface IPostRequestHelper
    {
        XDocument Post(string request, XDocument data);

        Task<XDocument> PostAsync(string request, XDocument data);
    }
}
