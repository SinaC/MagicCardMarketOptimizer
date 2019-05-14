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
                return ComputeDifference(x => x.Trend, last2);
            }
        }

        [XmlIgnore]
        public double? TrendPricePercentage
        {
            get
            {
                if (PricesHistory?.Count <= 1)
                    return null;
                PriceHistoryEntry[] last2 = PricesHistory.OrderByDescending(x => x.Timestamp).Take(2).ToArray();
                double diff = ComputeDifference(x => x.Trend, last2);
                return diff / last2[1].Prices.Trend;
            }
        }

        [XmlIgnore]
        public double? TrendPriceDifference7Days
        {
            get
            {
                if (PricesHistory?.Count <= 1)
                    return null;
                // search entry min 7 days ago
                PriceHistoryEntry current = PricesHistory.OrderByDescending(x => x.Timestamp).First();
                DateTime currentDate = current.Timestamp.Date;
                PriceHistoryEntry atLeast7Days = PricesHistory.OrderByDescending(x => x.Timestamp).FirstOrDefault(x => (currentDate - x.Timestamp.Date).Days >= 7);
                if (atLeast7Days == null)
                    return null;
                return ComputeDifference(x => x.Trend, current, atLeast7Days);
            }
        }

        [XmlIgnore]
        public double? TrendPricePercentage7Days
        {
            get
            {
                if (PricesHistory?.Count <= 1)
                    return null;
                // search entry min 7 days ago
                PriceHistoryEntry current = PricesHistory.OrderByDescending(x => x.Timestamp).First();
                DateTime currentDate = current.Timestamp.Date;
                PriceHistoryEntry atLeast7Days = PricesHistory.OrderByDescending(x => x.Timestamp).FirstOrDefault(x => (currentDate - x.Timestamp.Date).Days >= 7);
                if (atLeast7Days == null)
                    return null;
                double diff = ComputeDifference(x => x.Trend, current, atLeast7Days);
                return diff / atLeast7Days.Prices.Trend;
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
                    Sell = ComputeDifference(x => x.Sell, last2),
                    Low = ComputeDifference(x => x.Low, last2),
                    LowEx = ComputeDifference(x => x.LowEx, last2),
                    LowFoil = ComputeDifference(x => x.LowFoil, last2),
                    Average = ComputeDifference(x => x.Average, last2),
                    Trend = ComputeDifference(x => x.Trend, last2)
                };
            }
        }

        private decimal ComputeDifference(Func<ProductPriceGuide, decimal> getPriceFunc, params PriceHistoryEntry[] items)
        {
            return getPriceFunc(items.First().Prices) - getPriceFunc(items.Last().Prices);
        }

        private double ComputeDifference(Func<ProductPriceGuide, double> getPriceFunc, params PriceHistoryEntry[] items)
        {
            return getPriceFunc(items.First().Prices) - getPriceFunc(items.Last().Prices);
        }
    }

    public class PriceHistoryEntry
    {
        public DateTime Timestamp { get; set; }

        public ProductPriceGuide Prices { get; set; }
    }
}
