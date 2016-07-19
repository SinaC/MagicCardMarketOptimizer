using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

// TODO: implement every methods
namespace MagicCardMarket.APIHelpers
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Wants_List_Management
    public class WantsListManagement : HelperBase
    {
        public async Task<WantsList[]> GetWantsListAsync()
        {
            GetRequestHelper helper = new GetRequestHelper();
            //return await DeserializeMultipleAsync<WantsList>(helper.GetAsync("wantslist"));
            return await DeserializeMultipleAsync<WantsList>(GetWithCacheAsync("wantslist", 1, () => helper.GetAsync("wantslist")));
        }

        public async Task<Want[]> GetWantsAsync(int idWantsList)
        {
            GetRequestHelper helper = new GetRequestHelper();
            //return await DeserializeMultipleAsync<Want>(helper.GetAsync($"wantslist/{idWantsList}"));
            return await DeserializeMultipleAsync<Want>(GetWithCacheAsync("wants", idWantsList, () => helper.GetAsync($"wantslist/{idWantsList}")));
        }

        public async Task<WantsList[]> CreateWantsList(int idGame, string name)
        {
            CreateWantsListRequest request = new CreateWantsListRequest
            {
                WantsList = new CreateWantsList
                {
                    GameId = idGame,
                    Name = name
                }
            };
            IPostRequestHelper helper = new PostRequestHelper();
            return await DeserializeMultipleAsync<WantsList>(helper.PostAsync("wantslist", Serialize(request)));
        }

        public async Task<Want> AddProductInWantsList(int wantsListId, int idProduct, int count, int idLanguage, string minCondition, decimal wishPrice, bool isFoil = false, bool isPlayset = false, bool isAltered = false, bool isSigned = false, bool isFirstEd = false)
        {
            AddMetaProductOrProductRequest request = new AddMetaProductOrProductRequest
            {
                Action = "add",
                Products = new []
                {
                    new AddProduct
                    {
                        Id = idProduct,
                        Count = count,
                        LanguageId = idLanguage,
                        WishPrice = wishPrice,
                        MinCondition = minCondition,
                        IsFoil = isFoil,
                        IsPlayset = isPlayset,
                        IsAltered = isAltered,
                        IsSigned = isSigned,
                        IsFirstEd = isFirstEd
                    },
                }
            };
            IPutRequestHelper helper = new PutRequestHelper();
            return await DeserializeSingleAsync<Want>(helper.PutAsync($"wantslist/{wantsListId}", Serialize(request)));
        }

        public async Task<Want> AddMetaProductInWantsList(int wantsListId, int idMetaProduct, int count, int idLanguage, decimal wishPrice, string minCondition = "PO", bool isFoil = false, bool isPlayset = false, bool isAltered = false, bool isSigned = false, bool isFirstEd = false)
        {
            AddMetaProductOrProductRequest request = new AddMetaProductOrProductRequest
            {
                Action = "add",
                MetaProducts = new []
                {
                    new AddMetaProduct
                    {
                        Id = idMetaProduct,
                        Count = count,
                        LanguageId = idLanguage,
                        WishPrice = wishPrice,
                        MinCondition = minCondition,
                        IsFoil = isFoil,
                        IsPlayset = isPlayset,
                        IsAltered = isAltered,
                        IsSigned = isSigned,
                        IsFirstEd = isFirstEd
                    },
                }
            };
            IPutRequestHelper helper = new PutRequestHelper();
            return await DeserializeSingleAsync<Want>(helper.PutAsync($"wantslist/{wantsListId}", Serialize(request)));
        }

        public async Task<Want[]> AddMultipleInWantsList(int wantsListId, IEnumerable<AddMetaProduct> metaProducts, IEnumerable<AddProduct> products)
        {
            AddMetaProductOrProductRequest request = new AddMetaProductOrProductRequest
            {
                Action = "add",
                MetaProducts = metaProducts?.ToArray(),
                Products = products?.ToArray(),
            };
            IPutRequestHelper helper = new PutRequestHelper();
            return await DeserializeMultipleAsync<Want>(helper.PutAsync($"wantslist/{wantsListId}", Serialize(request)));
        }

        //ChangeWant(idWant, count, idLanguage, minCondition, wishPrice, isFoil, isAltered, isPlayset, isSigned, isFirstEd)
        //DeleteWantInWantsList(idWant)
        //DeleteWantsList(idWantsList)

        public void Test()
        {
            const int wantsListId = 979827;

            Want[] wants = AddMultipleInWantsList(wantsListId, new AddMetaProduct[]
            {
                new AddMetaProduct
                {
                    Id = 5395,
                    Count = 2,
                    WishPrice = 15,
                    LanguageId = 1
                }
            }, new AddProduct[]
            {
                new AddProduct
                {
                    Id = 258288,
                    Count = 3,
                    WishPrice = 12,
                    LanguageId = 1
                },
            }).Result;
        }
    }
}
