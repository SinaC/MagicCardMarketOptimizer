using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Wants_List_Management
    public class WantsListManagement : HelperBase
    {
        public async Task<WantsList[]> GetWantsList()
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await GetMultipleAsync<WantsList>(helper.GetAsync("wantslist"));
        }

        public async Task<Want[]> GetWants(int idWantsList)
        {
            GetRequestHelper helper = new GetRequestHelper();
            return await GetMultipleAsync<Want>(helper.GetAsync($"wantslist/{idWantsList}"));
        }

//CreateWantsList(idGame, name)
//AddProductInWantsList(idProduct, count, idLanguage, minCondition, wishPrice, isFoil, isAltered, isPlayset, isSigned, isFirstEd)
//AddMetaProductInWantsList(idMetaProduct, count, idLanguage, minCondition, wishPrice, isFoil, isAltered, isPlayset, isSigned, isFirstEd)
//ChangeWant(idWant, count, idLanguage, minCondition, wishPrice, isFoil, isAltered, isPlayset, isSigned, isFirstEd)
//DeleteWantInWantsList(idWant)
//DeleteWantsList(idWantsList)
    }
}
