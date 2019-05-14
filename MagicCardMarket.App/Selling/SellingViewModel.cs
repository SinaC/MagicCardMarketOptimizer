using MagicCardMarket.APIHelpers;
using MagicCardMarket.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MagicCardMarket.App.Selling
{
    public class SellingViewModel : ViewModelBase
    {
        private TimeSpan MinDelta = TimeSpan.FromHours(6);

        private List<ArticleItem> _articles;
        public List<ArticleItem> Articles
        {
            get { return _articles; }
            protected set { Set(() => Articles, ref _articles, value); }
        }

        private async Task LoadStockAsync(bool forceReload)
        {
            try
            {
                ShowWaitingScreen();

                StockManagement stockHelper = new StockManagement();
                Article[] articles = await stockHelper.GetStockAsync(forceReload);

                List<ArticleItem> items = new List<ArticleItem>();
                MarketPlaceInformation marketPlaceInformationHelper = new MarketPlaceInformation();
                foreach (Article article in articles)
                {
                    Product product = await marketPlaceInformationHelper.GetProductAsync(article.ProductId, forceReload);
                    ProductPriceHistory priceHistory = UpdateProductPriceHistory(product);

                    items.Add(new ArticleItem(article, product, priceHistory));
                }

                Articles = items;
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        private ProductPriceHistory UpdateProductPriceHistory(Product product)
        {
            string path = ConfigurationManager.AppSettings["pricehistorypath"];

            bool saveNeeded = false;
            XmlSerializer serializer = new XmlSerializer(typeof(ProductPriceHistory));

            // Read exising or create new one
            ProductPriceHistory productPriceHistory;
            string filename = Path.Combine(path, product.Id.ToString()) + ".xml";
            if (File.Exists(filename))
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    productPriceHistory = (ProductPriceHistory)serializer.Deserialize(reader);
                }
            }
            else
            {
                productPriceHistory = new ProductPriceHistory
                {
                    Product = product,
                    PricesHistory = new List<PriceHistoryEntry>()
                };
                saveNeeded = true;
            }
            // Add current price if time delta is enough
            if (productPriceHistory.PricesHistory.Count == 0 || (DateTime.Now - productPriceHistory.PricesHistory.Max(x => x.Timestamp)) >= MinDelta)
            {
                productPriceHistory.PricesHistory.Add(new PriceHistoryEntry
                {
                    Timestamp = DateTime.Now,
                    Prices = product.PriceGuide
                });
                saveNeeded = true;
            }
            // Save if needed
            if (saveNeeded)
            {
                using (TextWriter writer = new StreamWriter(filename))
                {
                    serializer.Serialize(writer, productPriceHistory);
                }
            }
            //
            return productPriceHistory;
        }

        public async Task InitializeAsync()
        {
            try
            {
                ShowWaitingScreen();

                await LoadStockAsync(false);
            }
            catch (Exception ex)
            {
                OnError(true, ex);
            }
            finally
            {
                HideWaitingScreen();
            }
        }
    }

    internal class SellingViewModelDesignData : SellingViewModel
    {
        public SellingViewModelDesignData()
        {
            Articles = new List<ArticleItem>
            {
                new ArticleItemDesignData(),
                new ArticleItemDesignData(),
            };
        }
    }
}
