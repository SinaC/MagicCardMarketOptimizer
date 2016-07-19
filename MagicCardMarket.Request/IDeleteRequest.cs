using System.Threading.Tasks;
using System.Xml.Linq;

namespace MagicCardMarket.Request
{
    public interface IDeleteRequest
    {
        void Delete(string request, XDocument data);

        Task DeleteAsync(string request);

        Task DeleteAsync(string request, XDocument data);
    }
}
