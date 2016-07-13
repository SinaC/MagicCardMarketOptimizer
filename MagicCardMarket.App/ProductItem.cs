using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicCardMarket.APIHelpers;
using MagicCardMarket.Models;

namespace MagicCardMarket.App
{
    public class ProductItem : WantItemBase
    {
        public override string Name { get; protected set; }

        private Product _product;
        public Product Product
        {
            get { return _product; }
            private set { Set(() => Product, ref _product, value); }
        }

        public ProductItem(Want want)
            : base(want)
        {
            if (want.Type != "product")
                throw new ArgumentException($"This cannot be initialized with {want.Type}", nameof(want));
        }

        public override async Task LoadAdditionalDatasAsync()
        {
            MarketPlaceInformation helper = new MarketPlaceInformation();
            Product = await helper.GetProductAsync(Want.ProductId);
            Name = Product?.Names?.FirstOrDefault(x => x.LanguageId == 1)?.Name;
        }

        public override async Task LoadArticlesAsync()
        {
            if (Articles != null)
                return;
            Articles = new List<ArticleItem>();
            if (Product != null)
            {
                MarketPlaceInformation helper = new MarketPlaceInformation();
                Article[] articles = await helper.GetArticlesAsync(Product.Id);
                List<ArticleItem> articleItems = articles.Select(x => new ArticleItem(x, Product)).ToList();
                Articles = articleItems.OrderBy(x => x.Article.Price).ToList();
            }
        }
    }

    public class ProductItemDesignData : ProductItem
    {
        public ProductItemDesignData(string name)
            :base(new Want { Type = "product", Count = 3})
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
