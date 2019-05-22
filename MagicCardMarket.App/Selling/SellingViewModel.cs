using MagicCardMarket.APIHelpers;
using MagicCardMarket.Models;
using MagicCardMarket.MVVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace MagicCardMarket.App.Selling
{
    public class SellingViewModel : ViewModelBase
    {
        private List<ArticleItem> _articles;
        public List<ArticleItem> Articles
        {
            get { return _articles; }
            protected set { Set(() => Articles, ref _articles, value); }
        }

        private ICommand _updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                _updateCommand = _updateCommand ?? new RelayCommand(async () => await LoadStockAsync(true));
                return _updateCommand;
            }
        }

        private async Task LoadStockAsync(bool updatePrices)
        {
            try
            {
                ShowWaitingScreen();

                StockManagement stockHelper = new StockManagement();
                Article[] articles = await stockHelper.GetStockAsync(true);

                List<ArticleItem> items = new List<ArticleItem>();
                MarketPlaceInformation marketPlaceInformationHelper = new MarketPlaceInformation();
                foreach (Article article in articles)
                {
                    Product product = await marketPlaceInformationHelper.GetProductAsync(article.ProductId, updatePrices);
                    ProductPriceHistory priceHistory = ReadAndUpdateProductPriceHistory(product, updatePrices);

                    items.Add(new ArticleItem(article, product, priceHistory));
                }

                Articles = items;
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        private ProductPriceHistory ReadAndUpdateProductPriceHistory(Product product, bool updatePrices)
        {
            string path = ConfigurationManager.AppSettings["pricehistorypath"];

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
            }
            if (updatePrices)
            {
                // Add current price
                productPriceHistory.PricesHistory.Add(new PriceHistoryEntry
                {
                    Timestamp = DateTime.Now,
                    Prices = product.PriceGuide
                });
                // Save
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
