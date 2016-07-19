using System.Threading.Tasks;

namespace MagicCardMarket.Request
{
    public interface IGetPagingRequest
    {
        PagedResult Get(string request, int from);

        Task<PagedResult> GetAsync(string request, int from);
    }
}
