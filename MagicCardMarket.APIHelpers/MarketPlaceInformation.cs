using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MagicCardMarket.Cache;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //c
    public class MarketPlaceInformation : HelperBase
    {
        private static readonly Lazy<ICache<Article[]>> LazyArticlesByProductIdCache = new Lazy<ICache<Article[]>>(() => new MemoryCache<Article[]>());
        protected static ICache<Article[]> ArticlesByProductIdCache = LazyArticlesByProductIdCache.Value;

        public async Task<Game[]> GetGamesAsync()
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await DeserializeMultipleAsync<Game>(helper.GetAsync("games"));
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
            return await DeserializeSingleAsync<MetaProduct>(GetWithCacheAsync("metaproduct", idMetaProduct, () => helper.GetAsync($"metaproduct/{idMetaProduct}")));
        }

        //SearchMetaProducts(name, idGame, idLanguage)

        public async Task<Product> GetProductAsync(int idProduct)
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await DeserializeSingleAsync<Product>(GetWithCacheAsync("product", idProduct, () => helper.GetAsync($"product/{idProduct}")));
        }

//SearchProducts(name, idGame, idLanguage, isExact) **

        public async Task<Expansion[]> GetExpansions(int idGame)
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await DeserializeMultipleAsync<Expansion>(helper.GetAsync($"expansion/{idGame}"));
        }

        //public async Task<Expansion> GetExpansion(int idGame, string name)
        //{

        //}
        public async Task<ExpansionCards> GetExpansionCards(int idGame, string expansionName)
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await DeserializeSingleAsync<ExpansionCards>(/*GetWithCacheAsync($"expansions", expansionName)*/helper.GetAsync($"expansion/{idGame}/{expansionName}"), "response");
        }

        public async Task<Article[]> GetArticlesAsync(int idProduct)
        {
            //Article[] articles;
            //if (ArticlesByProductIdCache.Contains(idProduct))
            //{
            //    Log.Log.Default.WriteLine(LogLevels.Info, $"CACHE HIT GetArticlesAsync: idProduct={idProduct}");
            //    articles = await Task.FromResult(ArticlesByProductIdCache.Get(idProduct));
            //}
            //else
            //{
            //    GetPagingRequestHelper helper = new GetPagingRequestHelper();
            //    articles = await DeserializeMultipleAsync<Article>(helper.GetAsync($"articles/{idProduct}"));
            //    ArticlesByProductIdCache.Set(idProduct, articles);
            //}
            //return articles;
            GetPagingRequestHelper helper = new GetPagingRequestHelper();
            return await DeserializeMultipleAsync<Article>(GetWithCacheAsync("article", idProduct, () => helper.GetAsync($"articles/{idProduct}")));
        }

//GetUser(idUser)
//GetArticles(idUser) **
    }
}
