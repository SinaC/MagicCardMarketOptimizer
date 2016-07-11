using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

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
            RequestHelper helper = new RequestHelper();
            Product = await helper.GetDataAsync<Product>($"product/{Want.ProductId}");
            Name = Product?.Names?.FirstOrDefault(x => x.LanguageId == 1)?.Name;
        }

        public override async Task LoadArticlesAsync()
        {
            if (Articles != null)
                return;
            Articles = new List<ArticleItem>();
            if (Product != null)
            {
                RequestHelper helper = new RequestHelper();
                //Article[] articles = await helper.GetDatasAsync<Article>($"articles/{Product.Id}/1"); // takes first 100 entries
                Article[] articles = await helper.GetDatasAsync<Article>($"articles/{Product.Id}");
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
