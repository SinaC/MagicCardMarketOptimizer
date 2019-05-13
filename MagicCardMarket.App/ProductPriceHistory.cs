using System;
using System.Linq;
using System.Collections.Generic;
using MagicCardMarket.Models;
using System.Xml.Serialization;

namespace MagicCardMarket.App
{
    public class ProductPriceHistory
    {
        public Product Product { get; set; }

        public List<PriceHistoryEntry> PricesHistory { get; set; }

        [XmlIgnore]
        public PriceHistoryEntry MostRecentPrices
        {
            get
            {
                if (PricesHistory?.Count == 0)
                    return null;
                return PricesHistory.OrderByDescending(x => x.Timestamp).First();
            }
        }

        [XmlIgnore]
        public double? TrendPriceDifference
        {
            get
            {
                if (PricesHistory?.Count <= 1)
                    return null;
                PriceHistoryEntry[] last2 = PricesHistory.OrderByDescending(x => x.Timestamp).Take(2).ToArray();
                return ComputeDifference(last2, x => x.Trend);
            }
        }

        [XmlIgnore]
        public ProductPriceGuide PriceDifference
        {
            get
            {
                if (PricesHistory?.Count <= 1)
                    return null;
                PriceHistoryEntry[] last2 = PricesHistory.OrderByDescending(x => x.Timestamp).Take(2).ToArray();
                return new ProductPriceGuide
                {
                    Sell = ComputeDifference(last2, x => x.Sell),
                    Low = ComputeDifference(last2, x => x.Low),
                    LowEx = ComputeDifference(last2, x => x.LowEx),
                    LowFoil = ComputeDifference(last2, x => x.LowFoil),
                    Average = ComputeDifference(last2, x => x.Average),
                    Trend = ComputeDifference(last2, x => x.Trend)
                };
            }
        }

        private decimal ComputeDifference(PriceHistoryEntry[] items, Func<ProductPriceGuide, decimal> getPriceFunc)
        {
            return getPriceFunc(items[0].Prices) - getPriceFunc(items[1].Prices);
        }

        private double ComputeDifference(PriceHistoryEntry[] items, Func<ProductPriceGuide, double> getPriceFunc)
        {
            return getPriceFunc(items[0].Prices) - getPriceFunc(items[1].Prices);
        }
    }

    public class PriceHistoryEntry
    {
        public DateTime Timestamp { get; set; }

        public ProductPriceGuide Prices { get; set; }
    }
}
