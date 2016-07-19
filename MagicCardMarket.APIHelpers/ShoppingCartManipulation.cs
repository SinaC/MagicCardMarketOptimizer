using System;
using System.Linq;
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
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeSingleAsync<ShoppingCarts>(helper.GetAsync("shoppingcart"), "response");
        }

        public async Task<ShoppingCarts> AddArticleInShoppingCart(int idArticle, int amount)
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
            IPutRequest helper = new PutRequestHelper();
            return await DeserializeSingleAsync<ShoppingCarts>(helper.PutAsync("shoppingcart", Serialize(request)), "response");
        }

        public async Task<ShoppingCarts> RemoveArticlesFromShoppingCart(params Tuple<int, int>[] articleIdAndCount)
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
            IPutRequest helper = new PutRequestHelper();
            return await DeserializeSingleAsync<ShoppingCarts>(helper.PutAsync("shoppingcart", Serialize(request)), "response");
        }

        public async Task EmptyShoppingCart()
        {
            IDeleteRequest helper = new DeleteRequestHelper();
            await helper.DeleteAsync("shoppingcart", null);
        }

//CheckOutShoppingCart()
//ChangeShippingAddress(name, extra, street, zip, city, country)

        public async Task<ShippingMethod[]> GetShippingMethods(int idReservation)
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeMultipleAsync<ShippingMethod>(helper.GetAsync($"shoppingcart/shippingmethod/{idReservation}"));
        }

        public async Task<ShoppingCarts> ChangeShippingMethod(int idReservation, int idShippingMethod)
        {
            ChangeShippingMethodRequest request = new ChangeShippingMethodRequest
            {
                ShippingMethodId = idShippingMethod
            };
            IPutRequest helper = new PutRequestHelper();
           return await DeserializeSingleAsync<ShoppingCarts>(helper.PutAsync($"shoppingcart/shippingmethod/{idReservation}", Serialize(request)), "response");
        }

        public void Test()
        {
            //ShoppingCarts carts = AddArticleInShoppingCart(238672691, 1).Result;
            //ShippingMethod[] methods = GetShippingMethods(carts.Carts[0].ReservationId).Result;

            //ChangeShippingMethodRequest request = new ChangeShippingMethodRequest
            //{
            //    ShippingMethodId = methods.Last().Id
            //};
            //XmlSerializer serializer = new XmlSerializer(typeof(ChangeShippingMethodRequest));
            //XDocument payload = new XDocument();
            //using (var writer = payload.CreateWriter())
            //    serializer.Serialize(writer, request);
            //IPutRequestHelper helper = new PutRequestHelper();

            //ShoppingCarts cart = DeserializeSingleAsync<ShoppingCarts>(helper.PutAsync($"shoppingcart/shippingmethod/{carts.Carts[0].ReservationId}", payload), "response").Result;
            //ShoppingCarts cart = ChangeShippingMethod(carts.Carts[0].ReservationId, methods.First().Id).Result;
        }
    }
}
