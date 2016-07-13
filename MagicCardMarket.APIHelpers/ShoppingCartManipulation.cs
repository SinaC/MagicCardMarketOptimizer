using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Shopping_Cart_Manipulation
    public class ShoppingCartManipulation : HelperBase
    {
        public async Task<ShoppingCarts> GetShoppingCartAsync()
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await GetSingleAsync<ShoppingCarts>(helper.GetAsync("shoppingcart"));
        }

//AddArticleInShoppingCart(idArticle, amount)
//RemoveArticlesFromShoppingCart({ idArticle, amount}
//        [])
//EmptyShoppingCart()
//CheckOutShoppingCart()
//ChangeShippingAddress(name, extra, street, zip, city, country)
//GetShippingMethods(idReservation)
//ChangeShippingMethod(idReservation, idShippingMethod)
    }
}
