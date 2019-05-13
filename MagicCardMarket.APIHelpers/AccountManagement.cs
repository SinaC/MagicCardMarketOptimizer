using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //https://api.cardmarket.com/ws/documentation/API_1.1:Account_Management
    public class AccountManagement : HelperBase
    {
        public async Task<Account> GetAccountAsync(bool forceReload)
        {
            IGetRequest helper = new GetRequestHelper();
            //return await DeserializeSingleAsync<Account>(helper.GetAsync("account"));
            return await DeserializeSingleAsync<Account>(GetWithCacheAsync("account", 1, forceReload, () => helper.GetAsync("account")));
        }

        //SetVacation(isOnVacation)
        //SetLanguage(idLanguage)

        public async Task<MessageThread[]> GetMessageThreadOverviewAsync()
        {
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeMultipleAsync<MessageThread>(helper.GetAsync("account/messages"));
        }

        public async Task<MessageThread> GetMessageThreadAsync(int idOtherUser)
        {
            // account/messages/id returns a MessageThread without thread tag
            IGetRequest helper = new GetRequestHelper();
            return await DeserializeSingleAsync<MessageThread>(helper.GetAsync($"account/messages/{idOtherUser}"), "thread");
        }

        //WriteMessage(idOtherUser, messageContent)
        //DeleteMessage(idOtherUser)
        //DeleteMessage(idOtherUser, idMessage)
    }
}
