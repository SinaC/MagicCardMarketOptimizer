using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

namespace MagicCardMarket.App
{
    public class MainViewModel : ViewModelBase
    {
        #region Account

        private int _id;
        public int Id
        {
            get { return _id; }
            set { Set(() => Id, ref _id, value); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { Set(() => UserName, ref _userName, value); }
        }

        private decimal _accountBalance;
        public decimal AccountBalance
        {
            get { return _accountBalance; }
            set { Set(() => AccountBalance, ref _accountBalance, value); }
        }

        private decimal _bankRecharge;
        public decimal BankRecharge
        {
            get { return _bankRecharge; }
            set { Set(() => BankRecharge, ref _bankRecharge, value); }
        }

        private decimal _payPalRecharge;
        public decimal PayPalRecharge
        {
            get { return _payPalRecharge; }
            set { Set(() => PayPalRecharge, ref _payPalRecharge, value); }
        }

        private int _unreadMessages;
        public int UnreadMessages
        {
            get { return _unreadMessages; }
            set { Set(() => UnreadMessages, ref _unreadMessages, value); }
        }

        private async Task LoadAccountAsync()
        {
            try
            {
                ShowWaitingScreen();

                RequestHelper requestHelper = new RequestHelper();
                Account account = await requestHelper.GetDataAsync<Account>("account");
                Id = account.Id;
                UserName = account.UserName;
                UnreadMessages = account.UnreadMessages;
                AccountBalance = account.AccountBalance;
                BankRecharge = account.BankRecharge;
                PayPalRecharge = account.PayPalRecharge;
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        #region WantsList

        private List<WantsList> _wantsLists;
        public List<WantsList> WantsLists
        {
            get { return _wantsLists; }
            set { Set(() => WantsLists, ref _wantsLists, value); }
        }

        private WantsList _selectedWantsList;
        public WantsList SelectedWantsList
        {
            get { return _selectedWantsList; }
            set
            {
                if (Set(() => SelectedWantsList, ref _selectedWantsList, value))
                    LoadWantsAsync();
            }
        }

        private async Task LoadWantsListsAsync()
        {
            try
            {
                ShowWaitingScreen();

                RequestHelper requestHelper = new RequestHelper();
                WantsList[] wantsLists = await requestHelper.GetDatasAsync<WantsList>("wantslist");
                WantsLists = wantsLists?.OrderBy(x => x.Name).ToList();
                SelectedWantsList = WantsLists?.FirstOrDefault();
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        #region Want

        private List<Want> _wants;
        public List<Want> Wants
        {
            get { return _wants; }
            set { Set(() => Wants, ref _wants, value); }
        }
        
        private async Task LoadWantsAsync()
        {
            try
            {
                ShowWaitingScreen();

                RequestHelper requestHelper = new RequestHelper();
                Want[] wants = await requestHelper.GetDatasAsync<Want>("wantslist/"+SelectedWantsList.Id);
                Wants = wants?.ToList();
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        public async Task Initialize()
        {
            Tokens.Init(@"d:\utils\token mcm.txt");

            //Account account = GetData<Account>("account");
            //Game game = GetData<Game>("games");
            //Game[] games = GetDatas<Game>("games");
            //Product island = GetData<Product>("products/island/1/1/false");
            //Product[] islands = GetDatas<Product>("products/island/1/1/false");
            //MetaProduct metaIsland = GetData<MetaProduct>("metaproduct/2923");
            //Expansion expansion = GetData<Expansion>("expansion/1");
            //Expansion[] expansions = GetDatas<Expansion>("expansion/1");
            //WantsList[] wantsLists = GetDatas<WantsList>("wantslist");

            //Product product266361 = GetData<Product>("product/266361");
            //Article[] articles266361 = GetDatas<Article>("articles/266361");
            //Want[] wants968105 = GetDatas<Want>("wantslist/968105");

            await LoadAccountAsync();
            await LoadWantsListsAsync();
        }
    }

    public class MainViewModelDesignData : MainViewModel
    {
        public MainViewModelDesignData()
        {
            Id = 12345;
            UserName = "SinaC";
            AccountBalance = 2.48m;
            BankRecharge = 3.57m;
            PayPalRecharge = 0m;
            UnreadMessages = 2;
            
            WantsLists = new List<WantsList>
            {
                new WantsList
                {
                    Name = "Deck1",
                    ItemCount = 10,
                },
                new WantsList
                {
                    Name = "Deck2",
                    ItemCount = 22,
                }
            };
        }
    }
}
