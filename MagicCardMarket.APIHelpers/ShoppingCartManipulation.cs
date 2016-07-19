using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
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
            return await DeserializeSingleAsync<ShoppingCarts>(helper.GetAsync("shoppingcart"), "response");
        }

        public async Task<ShoppingCarts> AddArticleInShoppingCart(int idArticle , int amount)
        {
            ShoppingCartRequest request = new ShoppingCartRequest
            {
                Action = "add",
                Articles = new[]
                {
                    new ShoppingCartArticle
                    {
                        Id = idArticle,
                        Amount = amount
                    }, 
                }
            };
            XmlSerializer serializer = new XmlSerializer(typeof(ShoppingCartRequest));
            XDocument document = new XDocument();
            using (var writer = document.CreateWriter())
                serializer.Serialize(writer, request);
            IPutRequestHelper helper = new PutRequestHelper();
            return await DeserializeSingleAsync<ShoppingCarts>(helper.PutAsync("shoppingcart", document), "response");
        }

        public async Task<ShoppingCarts> RemoveArticlesFromShoppingCart(params Tuple<int,int>[] articleIdAndCount)
        {
            ShoppingCartRequest request = new ShoppingCartRequest
            {
                Action = "remove",
                Articles = articleIdAndCount.Select(x => new ShoppingCartArticle
                {
                    Id = x.Item1,
                    Amount = x.Item2
                }).ToArray()
            };
            XmlSerializer serializer = new XmlSerializer(typeof(ShoppingCartRequest));
            XDocument document = new XDocument();
            using (var writer = document.CreateWriter())
                serializer.Serialize(writer, request);
            IPutRequestHelper helper = new PutRequestHelper();
            return await DeserializeSingleAsync<ShoppingCarts>(helper.PutAsync("shoppingcart", document), "response");
        }

        public async Task EmptyShoppingCart()
        {
            IDeleteRequestHelper helper = new DeleteRequestHelper();
            await helper.DeleteAsync("shoppingcart", null);
        }

//CheckOutShoppingCart()
//ChangeShippingAddress(name, extra, street, zip, city, country)
//GetShippingMethods(idReservation)
//ChangeShippingMethod(idReservation, idShippingMethod)
    }
}
