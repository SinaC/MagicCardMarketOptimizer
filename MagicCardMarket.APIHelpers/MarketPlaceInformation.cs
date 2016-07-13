using System.Threading.Tasks;
using System.Xml.Linq;
using MagicCardMarket.Cache;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Market_Place_Information
    public class MarketPlaceInformation : HelperBase
    {
        protected ICache<Article[]> ArticlesByProductIdCache = new MemoryCache<Article[]>();

        public async Task<Game[]> GetGamesAsync()
        {
            GetRequestHelper helper = new GetRequestHelper();
            //return await DeserializeMultipleAsync<Game>(helper.GetAsync("games"));
            return DeserializeMultiple<Game>(await helper.GetAsync("games"));
        }

        public async Task<MetaProduct> GetMetaProductAsync(int idMetaProduct)
        {
            //XDocument document;
            //if (Cache.Contains("metaproduct", idMetaProduct))
            //    document = Cache.Get("metaproduct", idMetaProduct);
            //else
            //{
            //    GetRequestHelper helper = new GetRequestHelper();
            //    document = await helper.GetAsync($"metaproduct/{idMetaProduct}");
            //    Cache.Set("metaproduct", idMetaProduct, document);
            //}
            //return DeserializeSingle<MetaProduct>(document);
            GetRequestHelper helper = new GetRequestHelper();
            //return await DeserializeSingleAsync<MetaProduct>(GetWithCacheAsync("metaproduct", idMetaProduct, () => helper.GetAsync($"metaproduct/{idMetaProduct}")));
            return DeserializeSingle<MetaProduct>(await GetWithCacheAsync("metaproduct", idMetaProduct, () => helper.GetAsync($"metaproduct/{idMetaProduct}")));
        }

//SearchMetaProducts(name, idGame, idLanguage)

        public async Task<Product> GetProductAsync(int idProduct)
        {
            //XDocument document;
            //if (Cache.Contains("product", idProduct))
            //    document = Cache.Get("product", idProduct);
            //else
            //{
            //    GetRequestHelper helper = new GetRequestHelper();
            //    document = await helper.GetAsync($"product/{idProduct}");
            //    Cache.Set("product", idProduct, document);
            //}
            //return DeserializeSingle<Product>(document);
            GetRequestHelper helper = new GetRequestHelper();
            //return await DeserializeSingleAsync<Product>(GetWithCacheAsync("product", idProduct, helper.GetAsync($"product/{idProduct}")));
            //return DeserializeSingle<Product>(GetWithCache("product", idProduct, await helper.GetAsync($"product /{ idProduct}")));
            return DeserializeSingle<Product>(await GetWithCacheAsync("product", idProduct, () => helper.GetAsync($"product/{idProduct}")));
        }

//SearchProducts(name, idGame, idLanguage, isExact) **
//GetExpansions(idGame) // TO TEST
//GetExpansion(idGame, name) // TO TEST

        public async Task<Article[]> GetArticlesAsync(int idProduct)
        {
            Article[] articles;
            if (ArticlesByProductIdCache.Contains(idProduct))
                articles = await Task.FromResult(ArticlesByProductIdCache.Get(idProduct));
            else
            {
                GetPagingRequestHelper helper = new GetPagingRequestHelper();
                //articles = await DeserializeMultipleAsync<Article>(helper.GetAsync($"articles/{idProduct}"));
                articles = DeserializeMultiple<Article>(await helper.GetAsync($"articles/{idProduct}"));
                ArticlesByProductIdCache.Set(idProduct, articles);
            }
            return articles;
        }

//GetUser(idUser)
//GetArticles(idUser) **
    }
}
