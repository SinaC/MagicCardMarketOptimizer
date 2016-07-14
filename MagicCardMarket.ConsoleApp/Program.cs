using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using MagicCardMarket.APIHelpers;
using MagicCardMarket.Models;
using System.Configuration;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

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
            //helper.GetShoppingCartAsync();
            //XDocument doc = XDocument.Load(@"d:\temp\MCMO\shoppingcart.xml");
            //XmlSerializer serializer = new XmlSerializer(typeof(ShoppingCarts));
            //ShoppingCarts carts = (ShoppingCarts)serializer.Deserialize(doc.CreateReader());

            WantsListObject tree = BuildTree(968414);

            System.Diagnostics.Debugger.Break();
        }

        public static WantsListObject BuildTree(int wantsListId)
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
                wantsListObject.WantObjects.AddIfNotExists(wantObject);

                if (want.Type == "metaproduct")
                {
                    MetaProduct metaproduct = AsyncGetResult(marketHelper.GetMetaProductAsync(want.MetaProductId));
                    MetaProductObject metaProductObject = new MetaProductObject
                    {
                        MetaProduct = metaproduct
                    };
                    wantObject.MetaProductObjects.AddIfNotExists(metaProductObject);

                    foreach (int productId in metaproduct.Products.ProductIds)
                    {
                        Product product = AsyncGetResult(marketHelper.GetProductAsync(productId));

                        if (product.Rarity != "Special" && product.Rarity != "Land")
                        {
                            ProductObject productObject = new ProductObject
                            {
                                Product = product
                            };
                            metaProductObject.ProductObjects.AddIfNotExists(productObject);

                            Article[] articles = AsyncGetResult(marketHelper.GetArticlesAsync(product.Id));
                            foreach (Article article in articles)
                            {
                                ArticleObject articleObject = new ArticleObject
                                {
                                    Article = article
                                };
                                productObject.ArticleObjects.AddIfNotExists(articleObject);
                            }
                        }
                    }
                }
                else if (want.Type == "product")
                {
                    Product product = AsyncGetResult(marketHelper.GetProductAsync(want.ProductId));
                    if (product.Rarity != "Special" && product.Rarity != "Land")
                    {
                        ProductObject productObject = new ProductObject
                        {
                            Product = product
                        };
                        wantObject.ProductObjects.AddIfNotExists(productObject);

                        Article[] articles = AsyncGetResult(marketHelper.GetArticlesAsync(product.Id));
                        foreach (Article article in articles)
                        {
                            ArticleObject articleObject = new ArticleObject
                            {
                                Article = article
                            };
                            productObject.ArticleObjects.AddIfNotExists(articleObject);
                        }
                    }
                }
            }

            return wantsListObject;
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

    public class WantObject
    {
        public Want Want { get; set; }

        public List<MetaProductObject> MetaProductObjects { get; set; }
        public List<ProductObject> ProductObjects { get; set; }

        public WantObject()
        {
            MetaProductObjects = new List<MetaProductObject>();
            ProductObjects = new List<ProductObject>();
        }
    }

    public class MetaProductObject
    {
        public MetaProduct MetaProduct { get; set; }

        public List<ProductObject> ProductObjects { get; set; }

        public MetaProductObject()
        {
            ProductObjects = new List<ProductObject>();
        }
    }

    public class ProductObject
    {
        public Product Product { get; set; }

        public List<ArticleObject> ArticleObjects { get; set; }

        public ProductObject()
        {
            ArticleObjects = new List<ArticleObject>();
        }
    }

    public class ArticleObject
    {
        public Article Article { get; set; }
    }
}
