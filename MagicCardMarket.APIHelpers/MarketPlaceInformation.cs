using System.Threading.Tasks;
using System.Xml.Linq;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods

namespace MagicCardMarket.APIHelpers
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Market_Place_Information
    public class MarketPlaceInformation : HelperBase
    {
        public async Task<Game[]> GetGamesAsync()
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await GetMultipleAsync<Game>(helper.GetAsync("games"));
        }

        public async Task<MetaProduct> GetMetaProductAsync(int idMetaProduct)
        {
            Task<XDocument> documentTask;
            if (Cache.Contains("metaproduct", idMetaProduct))
                documentTask = Task.FromResult(Cache.Get("metaproduct", idMetaProduct));
            else
            {
                GetRequestHelper helper = new GetRequestHelper();
                documentTask = helper.GetAsync($"metaproduct/{idMetaProduct}");
            }
            return await GetSingleAsync<MetaProduct>(documentTask);
        }

//SearchMetaProducts(name, idGame, idLanguage)

        public async Task<Product> GetProductAsync(int idProduct)
        {
            Task<XDocument> documentTask;
            if (Cache.Contains("product", idProduct))
                documentTask = Task.FromResult(Cache.Get("product", idProduct));
            else
            {
                GetRequestHelper helper = new GetRequestHelper();
                documentTask = helper.GetAsync($"product/{idProduct}");
            }
            return await GetSingleAsync<Product>(documentTask);
        }

//SearchProducts(name, idGame, idLanguage, isExact) **
//GetExpansions(idGame) // TO TEST
//GetExpansion(idGame, name) // TO TEST

        public async Task<Article[]> GetArticlesAsync(int idProduct)
        {
            GetPagingRequestHelper helper = new GetPagingRequestHelper();
            return await GetMultipleAsync<Article>(helper.GetAsync($"articles/{idProduct}"));
        }

//GetUser(idUser)
//GetArticles(idUser) **
    }
}
