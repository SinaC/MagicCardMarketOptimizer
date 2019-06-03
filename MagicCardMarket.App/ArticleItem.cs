using System;
using System.Collections.Generic;
using System.Linq;
using MagicCardMarket.Models;
using MagicCardMarket.MVVM;

namespace MagicCardMarket.App
{
    public class ArticleItem : ObservableObject
    {
        public Article Article { get; protected set; }

        public Product Product { get; protected set; }

        public MetaProduct MetaProduct { get; protected set; }

        public ProductPriceHistory PriceHistory { get; protected set; }

        public string Name { get; protected set; }

        public int ProductId { get; private set; }

        public decimal RealPrice => Article.IsPlayset ? Article.Price/4m : Article.Price;

        public ArticleItem(Article article, Product product)
        {
            Article = article;
            Product = product;

            Name = Product.Names?.FirstOrDefault(x => x.LanguageId == 1)?.Name;
        }

        public ArticleItem(Article article, MetaProduct metaProduct, int productId)
        {
            Article = article;
            MetaProduct = metaProduct;
            ProductId = productId;

            Name = MetaProduct.Names?.FirstOrDefault(x => x.LanguageId == 1)?.Name + (Article.IsFoil ? "*" : string.Empty);
        }

        public ArticleItem(Article article, Product product, ProductPriceHistory priceHistory)
        {
            Article = article;
            Product = product;
            PriceHistory = priceHistory;

            Name = Product.Names?.FirstOrDefault(x => x.LanguageId == 1)?.Name + (Article.IsFoil ? "*" : string.Empty);
        }
    }

    public class ArticleItemDesignData : ArticleItem
    {
        public ArticleItemDesignData()
            :base(
                 new Article
                 {
                     Count = 2,
                     Price = 9.45m,
                     Seller = new ArticleSeller
                     {
                         UserName = "tsekwa",
                         Country = "BE",
                     }
                 },
                 new MetaProduct
                 {
                     Names = new[] { new MetaProductName { Name = "Article1", LanguageId = 1 } }
                 },
                 1)
        {
            PriceHistory = new ProductPriceHistory
            {
                PricesHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry
                    {
                        Timestamp = DateTime.Now,
                        Prices = new ProductPriceGuide
                        {
                            Trend = 8,
                        }
                    },
                    new PriceHistoryEntry
                    {
                        Timestamp = DateTime.Now.AddDays(-1),
                        Prices = new ProductPriceGuide
                        {
                            Trend = 8.75,
                        }
                    }
                }
            };
        }
    }
}
