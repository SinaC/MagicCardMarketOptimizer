using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Account_Management
    public class AccountManagement : HelperBase
    {
        public async Task<Account> GetAccountAsync()
        {
            GetRequestHelper helper = new GetRequestHelper();
            //return await DeserializeSingleAsync<Account>(helper.GetAsync("account"));
            return DeserializeSingle<Account>(await helper.GetAsync("account"));
        }

        //SetVacation(isOnVacation)
        //SetLanguage(idLanguage)

        public async Task<MessageThread[]> GetMessageThreadOverview()
        {
            GetRequestHelper helper = new GetRequestHelper();
            //return await DeserializeMultipleAsync<MessageThread>(helper.GetAsync("account/messages"));
            return DeserializeMultiple<MessageThread>(await helper.GetAsync("account/messages"));
        }

        public async Task<MessageThread> GetMessageThread(int idOtherUser)
        {
            GetRequestHelper helper = new GetRequestHelper();
            //return await DeserializeSingleAsync<MessageThread>(helper.GetAsync($"account/messages/{idOtherUser}"));
            return DeserializeSingle<MessageThread>(await helper.GetAsync($"account/messages/{idOtherUser}"));
        }

        //WriteMessage(idOtherUser, messageContent)
        //DeleteMessage(idOtherUser)
        //DeleteMessage(idOtherUser, idMessage)
    }
}
