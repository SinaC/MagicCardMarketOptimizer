using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicCardMarket.APIHelpers;
using MagicCardMarket.Models;
using MagicCardMarket.MVVM;

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

                AccountManagement helper = new AccountManagement();
                Account account = await helper.GetAccountAsync();
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

                WantsListManagement helper = new WantsListManagement();
                WantsList[] wantsLists = await helper.GetWantsList();
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

        private List<WantItemBase> _wants;
        public List<WantItemBase> Wants
        {
            get { return _wants; }
            set { Set(() => Wants, ref _wants, value); }
        }

        private WantItemBase _selectedWantItem;
        public WantItemBase SelectedWantItem
        {
            get { return _selectedWantItem; }
            set
            {
                if (Set(() => SelectedWantItem, ref _selectedWantItem, value) && !DesignMode.IsInDesignModeStatic)
                    LoadArticlesFromSelectedWantItemAsync();
            }
        }

        private async Task LoadWantsAsync()
        {
            try
            {
                ShowWaitingScreen();

                SelectedWantItem = null;
                Sellers = null;
                Articles = null;
                SellerArticles = null;
                Wants = null;
                WantsListManagement helper = new WantsListManagement();
                Want[] wants = await helper.GetWants(SelectedWantsList.Id);
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
                    Wants = items;
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
                    {
                        SellerArticles = SelectedSeller.BestArticles;
                        MissingItems = SelectedSeller.MissingItems;
                        IsSellerArticlesExpanded = true;
                    }
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

                // Foreach want item, get sellers and articles
                await Wants.ForEachAsync(5, want => want.LoadArticlesAsync());
                //await Items.OfType<MetaProductItem>().First(x => x.MetaProduct.Id == 10738).LoadArticlesAsync();

                List<SellerItem> sellers = new List<SellerItem>();
                foreach (WantItemBase item in Wants)
                {
                    //var gharsim = item.Articles.Where(x => x.Seller.UserName == "gharsim");
                    //var daniel20044 = item.Articles.Where(x => x.Seller.UserName == "daniel20044");
                    //var toto = item.Articles.GroupBy(x => x.Seller.Id);
                    //var toto1 = toto.Where(x => x.Sum(a => a.Count) >= item.Count);
                    //var sellersUsername = item.Articles.GroupBy(x => x.Seller.UserName).Where(x => x.Sum(a => a.Count) >= item.Count).Select(x => x.Key);
                    //foreach (var articlesFromSellerWithEnoughItem in item.Articles.Where(x => x.Article.Seller != null).GroupBy(x => x.Article.Seller.Id).Where(x => x.Sum(a => a.Article.Count + a.Article.Count*4*(a.Article.IsPlayset?1:0)) >= item.Count)) // seller with enough items
                    //{
                    //    SellerItem seller = sellers.FirstOrDefault(x => x.Seller.Id == articlesFromSellerWithEnoughItem.Key);
                    //    if (seller == null)
                    //    {
                    //        seller = new SellerItem(articlesFromSellerWithEnoughItem.First().Article.Seller);
                    //        sellers.Add(seller);
                    //    }
                    //    //seller.AddArticles(item, item.Articles);
                    //    seller.AddArticles(item, articlesFromSellerWithEnoughItem);
                    //}
                    foreach (var articlesFromSeller in item.Articles.Where(x => x.Article.Seller != null).GroupBy(x => x.Article.Seller.Id))
                    {
                        SellerItem seller = sellers.FirstOrDefault(x => x.Seller.Id == articlesFromSeller.Key);
                        if (seller == null)
                        {
                            seller = new SellerItem(articlesFromSeller.First().Article.Seller);
                            sellers.Add(seller);
                        }
                        seller.AddArticles(item, articlesFromSeller);
                    }

                }
                foreach (SellerItem seller in sellers)
                {
                    //if (seller.Seller.UserName == "daniel20044" || seller.Seller.UserName == "Enki13")
                    //if (seller.Seller.UserName == "Griselbrandx")
                    //    Debugger.Break();
                    seller.ComputeBestTotalPrice();
                    seller.ComputeMissingItems(Wants);
                }
                Sellers = sellers.OrderByDescending(x => x.WantsCount).ThenBy(x => x.BestTotalPrices).ToList();
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        #region Articles from wants list

        private bool _isArticlesExpanded;
        public bool IsArticlesExpanded
        {
            get { return _isArticlesExpanded; }
            set { Set(() => IsArticlesExpanded, ref _isArticlesExpanded, value); }
        }

        private List<ArticleItem> _articles;
        public List<ArticleItem> Articles
        {
            get { return _articles; }
            set { Set(() => Articles, ref _articles, value); }
        }

        private async Task LoadArticlesFromSelectedWantItemAsync()
        {
            try
            {
                ShowWaitingScreen();

                await SelectedWantItem.LoadArticlesAsync();
                Articles = SelectedWantItem.Articles;
                IsArticlesExpanded = true;
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        #region Articles (+missing) from optimized seller

        private bool _isSellerArticlesExpanded;
        public bool IsSellerArticlesExpanded
        {
            get { return _isSellerArticlesExpanded; }
            set { Set(() => IsSellerArticlesExpanded, ref _isSellerArticlesExpanded, value); }
        }

        private List<SellerArticleItem> _sellerArticles;
        public List<SellerArticleItem> SellerArticles
        {
            get { return _sellerArticles; }
            set { Set(() => SellerArticles, ref _sellerArticles, value); }
        }

        private List<MissingItem> _missingItems;
        public List<MissingItem> MissingItems
        {
            get { return _missingItems; }
            set { Set(() => MissingItems, ref _missingItems, value); }
        }

        #endregion

        public async Task Initialize()
        {
            //Tokens.Init(@"d:\temp\token mcm.txt");

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

            Wants = new List<WantItemBase>
            {
                new MetaProductItemDesignData("MetaProduct1"),
                new ProductItemDesignData("Product2"),
            };
            SelectedWantItem = Wants[1];

            Articles = Enumerable.Repeat(new ArticleItemDesignData(), 100).Cast<ArticleItem>().ToList();
            IsArticlesExpanded = true;

            SellerArticles = Enumerable.Repeat(new SellerArticleItemDesignData(), 100).Cast<SellerArticleItem>().ToList();
            IsSellerArticlesExpanded = true;

            MissingItems = Enumerable.Repeat(new MissingItemDesignData(), 10).Cast<MissingItem>().ToList();
        }
    }
}
