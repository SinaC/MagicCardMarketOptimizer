using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //https://api.cardmarket.com/ws/documentation/API_1.1:Stock_Management
    public class StockManagement : HelperBase
    {
        public async Task<Article[]> GetStockAsync(bool forceReload)
        {
            IGetRequest helper = new GetRequestHelper();
            //return await DeserializeMultipleAsync<WantsList>(helper.GetAsync("wantslist"));
            return await DeserializeMultipleAsync<Article>(GetWithCacheAsync("stock", 1, forceReload, () => helper.GetAsync("stock")));
        }
        //GetStock() **
        //AddArticleInStock(idProduct, idLanguage, comments, count, price, condition, isFoil, isSigned, IsPlayset)
        //ChangeArticleInStock({idArticle, idLanguage, comments, count, price, condition, isFoil, isSigned, IsPlayset}[])
        //DeleteArticleInStock({idArticle, count}[])
        //GetArticlesInStockInShoppingCarts()
        //GetArticleInStock(articleId)
        //SearchArticlesInStock(name, idGame)
        //IncreaseStock(articleId, count)
        //DecreaseStock(articleId, count)
    }
}
