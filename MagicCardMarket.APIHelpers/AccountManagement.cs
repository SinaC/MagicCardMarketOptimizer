using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    public class AccountManagement : HelperBase
    {
        public async Task<Account> GetAccountAsync()
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await GetSingleAsync<Account>(helper.GetAsync("account"));
        }

        //SetVacation(isOnVacation)
        //SetLanguage(idLanguage)

        public async Task<MessageThread[]> GetMessageThreadOverview()
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await GetMultipleAsync<MessageThread>(helper.GetAsync("account/messages"));
        }

        public async Task<MessageThread> GetMessageThread(int idOtherUser)
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await GetSingleAsync<MessageThread>(helper.GetAsync($"account/messages/{idOtherUser}"));
        }

        //WriteMessage(idOtherUser, messageContent)
        //DeleteMessage(idOtherUser)
        //DeleteMessage(idOtherUser, idMessage)
    }
}
