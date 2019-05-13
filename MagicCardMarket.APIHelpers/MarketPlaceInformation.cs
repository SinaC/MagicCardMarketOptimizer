using System;
using System.Threading.Tasks;
using MagicCardMarket.Cache;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

namespace MagicCardMarket.APIHelpers
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Market_Place_Information
    public class MarketPlaceInformation : HelperBase
    {
        private static readonly Lazy<ICache<Article[]>> LazyArticlesByProductIdCache = new Lazy<ICache<Article[]>>(() => new MemoryCache<Article[]>());
        protected static ICache<Article[]> ArticlesByProductIdCache = LazyArticlesByProductIdCache.Value;

        public async Task<Game[]> GetGamesAsync()
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeMultipleAsync<Game>(helper.GetAsync("games"));
        }

        public async Task<MetaProduct> GetMetaProductAsync(int idMetaProduct, bool forceReload = false)
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await DeserializeSingleAsync<MetaProduct>(GetWithCacheAsync("metaproduct", idMetaProduct, forceReload, () => helper.GetAsync($"metaproduct/{idMetaProduct}")));
        }

        //GetMetaProducts(name, idGame, idLanguage)

        public async Task<Product> GetProductAsync(int idProduct, bool forceReload = false)
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeSingleAsync<Product>(GetWithCacheAsync("product", idProduct, forceReload, () => helper.GetAsync($"product/{idProduct}")));
        }

        public async Task<Product[]> SearchProductsAsync(string name, int idGame, int idLanguage, bool isExact)
        {
            IGetRequest helper = new GetPagingRequestHelper();
            return await DeserializeMultipleAsync<Product>(helper.GetAsync($"products/{name}/{idGame}/{idLanguage}/{isExact}"));
        }

        public async Task<Expansion[]> GetExpansionsAsync(int idGame)
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeMultipleAsync<Expansion>(helper.GetAsync($"expansion/{idGame}"));
        }

        public async Task<ExpansionCards> GetExpansionCardsAsync(int idGame, string expansionName)
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeSingleAsync<ExpansionCards>(/*GetWithCacheAsync($"expansions", expansionName)*/helper.GetAsync($"expansion/{idGame}/{expansionName}"), "response");
        }

        public async Task<Article[]> GetArticlesAsync(int idProduct, bool forceReload = false)
        {
            // Memory cache
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

            // Disk cache
            IGetRequest helper = new GetPagingRequestHelper();
            return await DeserializeMultipleAsync<Article>(GetWithCacheAsync("article", idProduct, forceReload, () => helper.GetAsync($"articles/{idProduct}")));
        }

        public async Task<User> GetUserAsync(string name)
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeSingleAsync<User>(helper.GetAsync($"user/{name}"));
        }

        public async Task<User> GetUserAsync(int idUser)
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeSingleAsync<User>(helper.GetAsync($"user/{idUser}"));
        }

        public async Task<Article[]> GetArticlesByUserAsync(int idUser)
        {
            IGetRequest helper = new GetPagingRequestHelper();
            return await DeserializeMultipleAsync<Article>(helper.GetAsync($"articles/user/{idUser}"));
        }

        public void Test()
        {
            //User user = GetUserAsync("pgeffe76").Result;
            //GetArticles(idUser) **
            //1679796
            //IGetRequest helper = new GetPagingRequestHelper();
            //XDocument document = helper.GetAsync("articles/user/1679796").Result;
            //Article[] articles = DeserializeMultipleAsync<Article>(Task.FromResult(document)).Result;
        }
    }
}
