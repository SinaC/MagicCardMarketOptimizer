using System.Threading.Tasks;
using System.Xml.Linq;

namespace MagicCardMarket.Request
{
    public interface IPutRequestHelper
    {
        void Put(string request, XDocument data);

        Task PutAsync(string request, XDocument data);
    }
}
