using System;
using System.Collections.Generic;
using MagicCardMarket.APIHelpers;
using MagicCardMarket.Models;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MagicCardMarket.Extensions;
using MagicCardMarket.Log;

namespace MagicCardMarket.ConsoleApp
{
    class Program
    {
        private static T AsyncGetResult<T>(Task<T> task)
        {
            task.Wait();
            return task.Result;
        }

        static void Main(string[] args)
        {
            string logFilename = "ConsoleApp_" + Guid.NewGuid().ToString().Substring(0, 5) + ".log";
            Log.Log.Default.Initialize(ConfigurationManager.AppSettings["logpath"], logFilename);

            //XDocument doc = XDocument.Load(@"d:\temp\MCMO\invalidarticles.xml");

            //articles/10683
            //MarketPlaceInformation helper = new MarketPlaceInformation();
            //helper.GetArticlesAsync(289367);
            //Expansion[] expansions = AsyncGetResult(helper.GetExpansions(1));
            //ExpansionCards cards = AsyncGetResult(helper.GetExpansionCards(1, "Alpha"));

            //AccountManagement helper = new AccountManagement();
            //MessageThread[] messageThreadOverview = AsyncGetResult(helper.GetMessageThreadOverviewAsync());

            //XDocument doc = XDocument.Load(@"d:\temp\MCMO\messagethreadoverview.xml");
            //XmlSerializer serializer = new XmlSerializer(typeof(MessageThread));
            //foreach (XNode node in doc.Root.Nodes()) // loop on <response> subnodes
            //{
            //    MessageThread thread = (MessageThread)serializer.Deserialize(node.CreateReader());
            //}
            //AccountManagement helper = new AccountManagement();
            //MessageThread messageThread = AsyncGetResult(helper.GetMessageThreadAsync(1679796));

            //ShoppingCartManipulation helper = new ShoppingCartManipulation();
            //ShoppingCarts carts1 = AsyncGetResult(helper.GetShoppingCartAsync());
            //XDocument doc = XDocument.Load(@"d:\temp\MCMO\shoppingcart.xml");
            //XmlSerializer serializer = new XmlSerializer(typeof(ShoppingCarts));
            //ShoppingCarts carts = (ShoppingCarts)serializer.Deserialize(doc.CreateReader());
            //ShoppingCarts carts2 = AsyncGetResult(helper.AddArticleInShoppingCart(238672691, 1));
            //ShoppingCarts carts3 = AsyncGetResult(helper.RemoveArticlesFromShoppingCart(new Tuple<int, int>(238672691, 1)));
            //helper.EmptyShoppingCart().Wait();
            //helper.Test();

            //WantsListManagement helper = new WantsListManagement();
            //helper.Test();

            Log.Log.Default.WriteLine(LogLevels.Info, "Building tree");
            WantsListObject tree = BuildWantsListObject(968414);
            Log.Log.Default.WriteLine(LogLevels.Info, "Done");

            Log.Log.Default.WriteLine(LogLevels.Info, "Searching sellers with most cards");
            List<SellerObject> sellersWithMostCards = BuildSellersWithMostCards(tree);
            Log.Log.Default.WriteLine(LogLevels.Info, "Done");

            //var prettyPrint = tree.WantObjects.Select(w => new {w.Want.Id, w.Want.Count, Name = w.AllProductObjects.First().Product.Names.First(p => p.LanguageId == 1).Name});

            // Search cheapest seller for each want
            Log.Log.Default.WriteLine(LogLevels.Info, "Searching cheapest articles by condition");
            List<CheapestWantByConditionObject> cheapestSellers = BuildCheapestArticlesByCondition(tree);
            Log.Log.Default.WriteLine(LogLevels.Info, "Done");

            System.Diagnostics.Debugger.Break();
        }

        public static WantsListObject BuildWantsListObject(int wantsListId)
        {
            WantsListManagement wantHelper = new WantsListManagement();
            MarketPlaceInformation marketHelper = new MarketPlaceInformation();

            // Wants
            List<Want> wants = AsyncGetResult(wantHelper.GetWantsAsync(wantsListId)).ToList();
            WantsListObject wantsListObject = new WantsListObject
            {
                WantsListId = wantsListId,
                Wants = wants
            };

            // MetaProducts/Products/Articles
            foreach (Want want in wants)
            {
                WantObject wantObject = new WantObject
                {
                    Want = want
                };
                wantsListObject.WantObjects.AddIfNotExists(wantObject, (w1, w2) => w1.Want.Id == w2.Want.Id);

                if (want.Type == "metaproduct")
                {
                    MetaProduct metaproduct = AsyncGetResult(marketHelper.GetMetaProductAsync(want.MetaProductId));
                    MetaProductObject metaProductObject = new MetaProductObject
                    {
                        MetaProduct = metaproduct
                    };
                    wantObject.MetaProductObjects.AddIfNotExists(metaProductObject, (m1, m2) => m1.MetaProduct.Id == m2.MetaProduct.Id);

                    foreach (int productId in metaproduct.Products.ProductIds)
                    {
                        Product product = AsyncGetResult(marketHelper.GetProductAsync(productId));

                        if (product.Rarity != "Special" && product.Rarity != "Land" && product.Expansion != "International Edition" && product.Expansion != "Collectors' Edition")
                        {
                            ProductObject productObject = new ProductObject
                            {
                                Product = product
                            };
                            metaProductObject.ProductObjects.AddIfNotExists(productObject, (p1, p2) => p1.Product.Id == p2.Product.Id);

                            Article[] articles = AsyncGetResult(marketHelper.GetArticlesAsync(product.Id));
                            foreach (Article article in articles.Where(a => a.Seller != null))
                            {
                                ArticleObject articleObject = new ArticleObject
                                {
                                    WantObject = wantObject,
                                    ProductObject =  productObject,
                                    Article = article
                                };
                                productObject.ArticleObjects.AddIfNotExists(articleObject, (a1, a2) => a1.Article.Id == a2.Article.Id);
                            }
                        }
                    }
                }
                else if (want.Type == "product")
                {
                    Product product = AsyncGetResult(marketHelper.GetProductAsync(want.ProductId));
                    if (product.Rarity != "Special" && product.Rarity != "Land" && product.Expansion != "International Edition" && product.Expansion != "Collectors' Edition")
                    {
                        ProductObject productObject = new ProductObject
                        {
                            Product = product
                        };
                        wantObject.ProductObjects.AddIfNotExists(productObject, (p1, p2) => p1.Product.Id == p2.Product.Id);

                        Article[] articles = AsyncGetResult(marketHelper.GetArticlesAsync(product.Id));
                        foreach (Article article in articles.Where(a => a.Seller != null))
                        {
                            ArticleObject articleObject = new ArticleObject
                            {
                                WantObject = wantObject,
                                ProductObject = productObject,
                                Article = article
                            };
                            productObject.ArticleObjects.AddIfNotExists(articleObject, (a1, a2) => a1.Article.Id == a2.Article.Id);
                        }
                    }
                }
            }

            return wantsListObject;
        }

        public static List<SellerObject> BuildSellersWithMostCards(WantsListObject wantsListObject)
        {
            List<SellerObject> sellerObjects = new List<SellerObject>();
            var sellers = wantsListObject.WantObjects.SelectMany(w => w.AllProductObjects.SelectMany(p => p.ArticleObjects)).Select(x => x.Article.Seller).DistinctBy(x => x.Id);
            foreach (var sellerObject in wantsListObject.WantObjects.SelectMany(w => w.AllProductObjects.SelectMany(p => p.ArticleObjects).Where(a => a.ConditionOrder >= Helpers.ConditionOrder(w.Want.MinCondition))).GroupBy(x => x.Article.Seller.Id).Select(g => new SellerObject
            {
                Seller = sellers.FirstOrDefault(x => x.Id == g.Key),
                ArticleObjects = g.ToList()
            }))
            {
                int wantsCount = 0;
                sellerObject.BuyList = new List<BuyObject>();
                foreach (var wantObject in wantsListObject.WantObjects)
                {
                    decimal sumByWant = 0;
                    int count = wantObject.Want.Count;
                    foreach (var articleObject in sellerObject.ArticleObjects.Where(a => a.WantObject.Want.Id == wantObject.Want.Id).OrderBy(x => x.Article.Price))
                    {
                        if (articleObject.Article.IsPlayset && count >= 4)
                        {
                            sellerObject.BuyList.Add(new BuyObject
                            {
                                ArticleObject = articleObject,
                                Count = 4
                            });
                            sumByWant += articleObject.Article.Price;
                            wantsCount += 4;
                            count -= 4;
                        }
                        else if (articleObject.Article.Count >= count)
                        {
                            sellerObject.BuyList.Add(new BuyObject
                            {
                                ArticleObject = articleObject,
                                Count = count
                            });
                            sumByWant += count * articleObject.Article.Price;
                            wantsCount += count;
                            count = 0;
                        }
                        else
                        {
                            sellerObject.BuyList.Add(new BuyObject
                            {
                                ArticleObject = articleObject,
                                Count = articleObject.Article.Count
                            });
                            sumByWant += articleObject.Article.Count * articleObject.Article.Price;
                            wantsCount += articleObject.Article.Count;
                            count -= articleObject.Article.Count;
                        }
                        if (count == 0)
                            break;
                    }
                    if (count != 0)
                    {
                        sellerObject.MissingWants.Add(new MissingWantObject
                        {
                            WantObject = wantObject,
                            Count = wantObject.Want.Count - count
                        });
                    }
                    sellerObject.BestTotalPrice += sumByWant;
                }
                sellerObject.WantsCount = wantsCount;
                sellerObjects.Add(sellerObject);
            }
            return sellerObjects.OrderByDescending(x => x.WantsCount).ThenBy(x => x.BestTotalPrice).ToList();
        }

        public static List<CheapestWantByConditionObject> BuildCheapestArticlesByCondition(WantsListObject wantsListObject)
        {
            return wantsListObject.WantObjects.Select(w => new CheapestWantByConditionObject
                {
                    WantObject = w,
                    CheapestByConditionObjects = w.AllProductObjects
                        .SelectMany(x => x.ArticleObjects.Where(a => a.ConditionOrder >= Helpers.ConditionOrder(w.Want.MinCondition)))
                        .GroupBy(x => x.Article.Condition)
                        .Select(g => new CheapestByConditionObject
                        {
                            Condition = g.Key,
                            ArticleObjects = g.OrderBy(a => a.Article.Price).Take(5).ToList()
                        }).OrderByDescending(x => Helpers.ConditionOrder(x.Condition)).ToList()
                }).ToList();
        }
    }

    public class WantsListObject
    {
        public int WantsListId { get; set; }

        public List<Want> Wants { get; set; }

        public List<WantObject> WantObjects { get; set; }

        public WantsListObject()
        {
            WantObjects = new List<WantObject>();
        }
    }

    [DebuggerDisplay("Name:{Name} Count:{Want.Count}")]
    public class WantObject
    {
        public Want Want { get; set; }

        public List<MetaProductObject> MetaProductObjects { get; set; }
        public List<ProductObject> ProductObjects { get; set; }

        public string Name => AllProductObjects.First().Name;

        public IEnumerable<ProductObject> AllProductObjects => MetaProductObjects.SelectMany(x => x.ProductObjects).Union(ProductObjects, (p1, p2) => p1.Product.Id == p2.Product.Id);

        public WantObject()
        {
            MetaProductObjects = new List<MetaProductObject>();
            ProductObjects = new List<ProductObject>();
        }
    }

    [DebuggerDisplay("Name:{Name}")]
    public class MetaProductObject
    {
        public MetaProduct MetaProduct { get; set; }

        public List<ProductObject> ProductObjects { get; set; }

        public string Name => MetaProduct.Names.First(n => n.LanguageId == 1).Name;

        public MetaProductObject()
        {
            ProductObjects = new List<ProductObject>();
        }
    }

    [DebuggerDisplay("Name:{Name} Expansion:{Product.Expansion}")]
    public class ProductObject
    {
        public Product Product { get; set; }

        public List<ArticleObject> ArticleObjects { get; set; }

        public string Name => Product.Names.First(n => n.LanguageId == 1).Name;

        public ProductObject()
        {
            ArticleObjects = new List<ArticleObject>();
        }
    }

    [DebuggerDisplay("Price:{Article.Price} Condition:{Article.Condition} Seller:{Article.Seller.UserName} Count:{Article.Count} Expansion:{ProductObject.Product.Expansion}")]
    public class ArticleObject
    {
        public int ConditionOrder => Helpers.ConditionOrder(Article.Condition);

        public WantObject WantObject { get; set; } // stored to speed up search and optimization
        public ProductObject ProductObject { get; set; } // stored to speed up search and optimization
        public Article Article { get; set; }
    }

    public class SellerObject
    {
        public ArticleSeller Seller { get; set; }
        public List<ArticleObject> ArticleObjects { get; set; }

        // Results of optimizer
        public decimal BestTotalPrice { get; set; }
        public int WantsCount { get; set; }
        public List<BuyObject> BuyList { get; set; }
        public List<MissingWantObject> MissingWants { get; set; }

        public SellerObject()
        {
            ArticleObjects = new List<ArticleObject>();
            BuyList = new List<BuyObject>();
            MissingWants = new List<MissingWantObject>();
        }
    }

    public class BuyObject
    {
        public ArticleObject ArticleObject { get; set; }
        public int Count { get; set; }
    }

    public class MissingWantObject
    {
        public WantObject WantObject { get; set; }
        public int Count { get; set; }
    }

    public class CheapestByConditionObject
    {
        public List<ArticleObject> ArticleObjects { get; set; } // n-cheapest articles
        public string Condition { get; set; }
    }

    public class CheapestWantByConditionObject
    {
        public WantObject WantObject { get; set; }
        public List<CheapestByConditionObject> CheapestByConditionObjects { get; set; }
    }

    public static class Helpers
    {
        public static int ConditionOrder(string condition)
        {
            //https://www.magiccardmarket.eu/Help/Card_condition
            switch (condition)
            {
                case "MT":
                    return 6;
                case "NM":
                    return 5;
                case "EX":
                    return 4;
                case "GD":
                    return 3;
                case "LP":
                    return 2;
                case "PL":
                    return 1;
                case "PO":
                    return 0;
                default:
                    return -1;
            }
        }
    }
}
