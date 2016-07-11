using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicCardMarket.Models;
using MagicCardMarket.MVVM;
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
                if (Set(() => SelectedWantsList, ref _selectedWantsList, value) && !DesignMode.IsInDesignModeStatic)
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

        private List<WantItemBase> _items;
        public List<WantItemBase> Items
        {
            get { return _items; }
            set { Set(() => Items, ref _items, value); }
        }

        private WantItemBase _selectedWantItem;
        public WantItemBase SelectedWantItem
        {
            get { return _selectedWantItem; }
            set
            {
                if (Set(() => SelectedWantItem, ref _selectedWantItem, value) && !DesignMode.IsInDesignModeStatic)
                {
                    //SelectedWantItem.RaisePropertyChangedOnAll();
                    LoadArticlesAsync();
                }
            }
        }

        private async Task LoadWantsAsync()
        {
            try
            {
                ShowWaitingScreen();

                SelectedWantItem = null;
                Sellers = null;
                RequestHelper requestHelper = new RequestHelper();
                Want[] wants = await requestHelper.GetDatasAsync<Want>("wantslist/"+SelectedWantsList.Id);
                //Wants = wants?.ToList();
                if (wants != null)
                {
                    List<WantItemBase> items = new List<WantItemBase>();
                    foreach (Want want in wants)
                    {
                        WantItemBase item;
                        if (want.Type == "product")
                            item = new ProductItem(want);
                        else if (want.Type == "metaproduct")
                            item = new MetaProductItem(want);
                        else
                            throw new InvalidOperationException($"Unknown want type {want.Type}");
                        await item.LoadAdditionalDatasAsync();
                        items.Add(item);
                    }
                    Items = items;
                }
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        #region Optimize

        private List<SellerItem> _sellers;
        public List<SellerItem> Sellers
        {
            get { return _sellers; }
            set { Set(() => Sellers, ref _sellers, value); }
        }

        private SellerItem _selectedSeller;
        public SellerItem SelectedSeller
        {
            get { return _selectedSeller; }
            set
            {
                if (Set(() => SelectedSeller, ref _selectedSeller, value))
                {
                    if (SelectedSeller != null)
                        ArticlesGridHeader = $"Articles for Seller {SelectedSeller.Seller.UserName}";
                    IsArticleProductNameColumnVisible = true;
                    IsArticleSellerNameColumnVisible = false;
                    Articles = SelectedSeller.Articles;
                }
            }
        }

        private ICommand _optimizeCommand;
        public ICommand OptimizeCommand
        {
            get
            {
                _optimizeCommand = _optimizeCommand ?? new RelayCommand(async () => await Optimize());
                return _optimizeCommand;
            }
        }

        private async Task Optimize()
        {
            try
            {
                // TODO
                // for the moment, this only get list a seller with most cards and order them by total price (shipping cost is not used)
                ShowWaitingScreen();

                await Items.ForEachAsync(5, item => item.LoadArticlesAsync());

                List<SellerItem> sellers = new List<SellerItem>();
                foreach (WantItemBase item in Items)
                {
                    //var gharsim = item.Articles.Where(x => x.Seller.UserName == "gharsim");
                    //var daniel20044 = item.Articles.Where(x => x.Seller.UserName == "daniel20044");
                    //var toto = item.Articles.GroupBy(x => x.Seller.Id);
                    //var toto1 = toto.Where(x => x.Sum(a => a.Count) >= item.Count);
                    //var sellersUsername = item.Articles.GroupBy(x => x.Seller.UserName).Where(x => x.Sum(a => a.Count) >= item.Count).Select(x => x.Key);
                    foreach (var sellerWithEnoughItem in item.Articles.GroupBy(x => x.Article.Seller.Id).Where(x => x.Sum(a => a.Article.Count + a.Article.Count*4*(a.Article.IsPlayset?1:0)) >= item.Count)) // seller with enough items
                    {
                        SellerItem seller = sellers.FirstOrDefault(x => x.Seller.Id == sellerWithEnoughItem.Key);
                        if (seller == null)
                        {
                            seller = new SellerItem(sellerWithEnoughItem.First().Article.Seller);
                            sellers.Add(seller);
                        }
                        //seller.AddArticles(item, item.Articles);
                        seller.AddArticles(item, sellerWithEnoughItem);
                    }
                }
                foreach (SellerItem seller in sellers)
                {
                    //if (seller.Seller.UserName == "daniel20044" || seller.Seller.UserName == "Enki13")
                    //if (seller.Seller.UserName == "Griselbrandx")
                    //    Debugger.Break();
                    seller.ComputeBestTotalPrice();
                }
                Sellers = sellers.OrderByDescending(x => x.Wants.Count).ThenBy(x => x.BestTotalPrices).Take(15).ToList();
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        #region Articles

        private bool _isArticleProductNameColumnVisible;
        public bool IsArticleProductNameColumnVisible
        {
            get {  return _isArticleProductNameColumnVisible;}
            set { Set(() => IsArticleProductNameColumnVisible, ref _isArticleProductNameColumnVisible, value); }
        }


        private bool _isArticleSellerNameColumnVisible;
        public bool IsArticleSellerNameColumnVisible
        {
            get { return _isArticleSellerNameColumnVisible; }
            set { Set(() => IsArticleSellerNameColumnVisible, ref _isArticleSellerNameColumnVisible, value); }
        }

        private string _articlesGridHeader;
        public string ArticlesGridHeader
        {
            get { return _articlesGridHeader; }
            set { Set(() => ArticlesGridHeader, ref _articlesGridHeader, value); }
        }

        private List<ArticleItem> _articles;
        public List<ArticleItem> Articles
        {
            get { return _articles; }
            set { Set(() => Articles, ref _articles, value); }
        }

        private async Task LoadArticlesAsync()
        {
            try
            {
                ShowWaitingScreen();

                ArticlesGridHeader = $"Articles for Product {SelectedWantItem.Name}";
                IsArticleProductNameColumnVisible = false;
                IsArticleSellerNameColumnVisible = true;
                await SelectedWantItem.LoadArticlesAsync();
                Articles = SelectedWantItem.Articles;
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        public async Task Initialize()
        {
            Tokens.Init(@"d:\temp\token mcm.txt");

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

            await Task.WhenAll(LoadAccountAsync(), LoadWantsListsAsync());
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
            SelectedWantsList = WantsLists[0];

            Items = new List<WantItemBase>
            {
                new MetaProductItemDesignData("MetaProduct1"),
                new ProductItemDesignData("Product2"),
            };
            SelectedWantItem = Items[1];
        }
    }
}
