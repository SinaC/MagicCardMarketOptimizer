using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //https://api.cardmarket.com/ws/documentation/API_1.1:Order_Management
    public class OrderManagement : HelperBase
    {
        public async Task<Order> GetOrder(int idOrder)
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeSingleAsync<Order>(helper.GetAsync($"order/{idOrder}"));
        }

        //MarkOrderAsSent(idOrder)
        //ConfirmReception(idOrder)
        //Cancel(idOrder)
        //RequestCancellation(idOrder, reason, relistItems)
        //AcceptCancellation(idOrder, relistItems)
        //EvaluateOrder(idOrder, evalutationGrade, itemDescription, packaging, speed, comment, complaint[])

        /// <summary>
        /// Returns a collection of orders specified by the actor parameter (buyer or seller) and the state parameter (bought, paid, sent, received, lost, cancelled). Only orders for the authenticated user are returned.
        /// https://www.mkmapi.eu/ws/documentation/API_1.1:Filter_Orders
        /// </summary>
        /// <param name="actor">
        /// seller: 1
        /// buyer: 2</param>
        /// <param name="state">
        /// bought: 1
        /// paid: 2
        /// sent: 4
        /// received: 8
        /// lost: 32
        /// cancelled: 128
        ///</param>
        /// <returns></returns>
        public async Task<Order[]> GetOrders(int actor, int state)
        {
            IGetRequest helper = new GetPagingRequestHelper();
            return await DeserializeMultipleAsync<Order>(helper.GetAsync($"orders/{actor}/{state}"));
        }

        public void Test()
        {
            //IGetRequest helper = new GetPagingRequestHelper();
            //XDocument doc = helper.GetAsync("orders/1/4").Result;
            //Order[] orders = DeserializeMultipleAsync<Order>(Task.FromResult(doc)).Result;
            //Order[] orders = GetOrders(2, 4).Result;

            Order order = GetOrder(35681911).Result;
        }
    }
}
