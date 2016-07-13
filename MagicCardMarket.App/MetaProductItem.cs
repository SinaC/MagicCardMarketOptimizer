using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicCardMarket.APIHelpers;
using MagicCardMarket.Models;

namespace MagicCardMarket.App
{
    public class MetaProductItem : WantItemBase
    {
        public override string Name { get; protected set; }

        private MetaProduct _metaProduct;
        public MetaProduct MetaProduct
        {
            get { return _metaProduct; }
            private set { Set(() => MetaProduct, ref _metaProduct, value); }
        }

        private List<Product> _products;

        public List<Product> Products
        {
            get { return _products; }
            private set { Set(() => Products, ref _products, value); }
        }

        public MetaProductItem(Want want) 
            : base(want)
        {
            if (want.Type != "metaproduct")
                throw new ArgumentException($"This cannot be initialized with {want.Type}", nameof(want));
            Products = new List<Product>();
        }

        public override async Task LoadAdditionalDatasAsync()
        {
            MarketPlaceInformation helper = new MarketPlaceInformation();
            MetaProduct = await helper.GetMetaProductAsync(Want.MetaProductId);
            Name = MetaProduct?.Names?.FirstOrDefault(x => x.LanguageId == 1)?.Name;
        }

        public override async Task LoadArticlesAsync()
        {
            if (Articles != null)
                return;
            Articles = new List<ArticleItem>();
            if (MetaProduct?.Products != null) // TODO: should be able to take only one product but which one ???
            {
                //System.Diagnostics.Debug.WriteLine($"LoadArticlesAsync: Name: {MetaProduct.Names[0].Name} #Products: {MetaProduct.Products.ProductIds.Length}");

                if (Parameters.UseHigherIdHeuristic || Parameters.FilterOutSpecial)
                {
                    // Heuristic, recent items (with higher id) are cheaper
                    IEnumerable<int> productsIds = MetaProduct.Products.ProductIds;
                    if (Parameters.UseHigherIdHeuristic)
                        productsIds = productsIds.OrderByDescending(x => x).Take(Parameters.HigherIdHeuristicCount);
                    foreach (int productId in productsIds)
                    {
                        MarketPlaceInformation helper = new MarketPlaceInformation();
                        Product product = await helper.GetProductAsync(productId);
                        Products.Add(product);
                    }
                    //
                    IEnumerable<Product> validProducts = Products;
                    if (Parameters.FilterOutSpecial)
                        validProducts = validProducts.Where(x => x.Rarity != "Special");
                    if (Parameters.FilterOutSpecial)
                        validProducts = validProducts.Where(x => x.Rarity != "Land");
                    if (Parameters.UseTrendPriceHeuristic)
                    {
                        double averageTrendPrice = validProducts.Average(x => x.PriceGuide.Trend);
                        validProducts = validProducts.Where(x => x.PriceGuide.Trend <= averageTrendPrice * (1 + Parameters.TrendPriceHeuristicPercentage));
                    }
                    foreach (Product product in validProducts)
                    {
                        //Article[] articles = await helper.GetDatasAsync<Article>($"articles/{productId}/1"); // takes first 100 entries
                        MarketPlaceInformation helper = new MarketPlaceInformation();
                        Article[] articles = await helper.GetArticlesAsync(product.Id);
                        List<ArticleItem> articleItems = articles.Select(x => new ArticleItem(x, product)).ToList();
                        Articles.AddRange(articleItems);
                    }
                }
                else
                {
                    foreach (int productId in MetaProduct.Products.ProductIds)
                    {
                        //Article[] articles = await helper.GetDatasAsync<Article>($"articles/{productId}/1"); // takes first 100 entries
                        MarketPlaceInformation helper = new MarketPlaceInformation();
                        Article[] articles = await helper.GetArticlesAsync(productId);
                        List<ArticleItem> articleItems = articles.Select(x => new ArticleItem(x, MetaProduct, productId)).ToList();
                        Articles.AddRange(articleItems);
                    }
                }
                Articles = Articles.OrderBy(x => x.Article.Price).ToList();
            }
        }
    }

    public class MetaProductItemDesignData : MetaProductItem
    {
        public MetaProductItemDesignData(string name)
            : base(new Want {Type = "metaproduct", Count = 2})
        {
            Name = name;
            Articles = new List<ArticleItem>
            {
                new ArticleItemDesignData(),
                new ArticleItemDesignData()
            };
        }
    }
}
