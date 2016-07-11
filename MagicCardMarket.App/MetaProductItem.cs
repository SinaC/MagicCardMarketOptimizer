using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

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
            RequestHelper helper = new RequestHelper();
            MetaProduct = await helper.GetDataAsync<MetaProduct>($"metaproduct/{Want.MetaProductId}");
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

                RequestHelper helper = new RequestHelper();

                if (Parameters.UseHigherIdHeuristics || Parameters.FilterOutSpecial)
                {
                    // Heuristic, recent items (with higher id) are cheaper
                    IEnumerable<int> productsIds = MetaProduct.Products.ProductIds;
                    if (Parameters.UseHigherIdHeuristics)
                        productsIds = productsIds.OrderByDescending(x => x).Take(Parameters.HigherIdHeuristicsCount);
                    foreach (int productId in productsIds)
                    {
                        Product product = await helper.GetDataAsync<Product>($"product/{productId}");
                        Products.Add(product);
                    }
                    //
                    IEnumerable<Product> validProducts = Products;
                    if (Parameters.FilterOutSpecial)
                        validProducts = validProducts.Where(x => x.Rarity != "Special");
                    if (Parameters.UseTrendPriceHeuristics)
                    {
                        double averageTrendPrice = validProducts.Average(x => x.PriceGuide.Trend);
                        validProducts = validProducts.Where(x => x.PriceGuide.Trend <= averageTrendPrice * (1 + Parameters.TrendPriceHeuristicsPercentage));
                    }
                    foreach (Product product in validProducts)
                    {
                        //Article[] articles = await helper.GetDatasAsync<Article>($"articles/{productId}/1"); // takes first 100 entries
                        Article[] articles = await helper.GetDatasAsync<Article>($"articles/{product.Id}");
                        List<ArticleItem> articleItems = articles.Select(x => new ArticleItem(x, product)).ToList();
                        Articles.AddRange(articleItems);
                    }
                }
                else
                {
                    foreach (int productId in MetaProduct.Products.ProductIds)
                    {
                        //Article[] articles = await helper.GetDatasAsync<Article>($"articles/{productId}/1"); // takes first 100 entries
                        Article[] articles = await helper.GetDatasAsync<Article>($"articles/{productId}");
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
