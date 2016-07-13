using System.Threading.Tasks;
using System.Xml.Linq;

namespace MagicCardMarket.Request
{
    public interface IPostRequestHelper
    {
        void Post(string request, XDocument data);

        Task PostAsync(string request, XDocument data);
    }
}
