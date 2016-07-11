using System.Collections.Generic;
using System.Linq;
using MagicCardMarket.Models;
using MagicCardMarket.MVVM;

namespace MagicCardMarket.App
{
    public class SellerItem : ObservableObject
    {
        public ArticleSeller Seller { get; protected set; }

        public List<ArticleItem> Articles { get; protected set; }

        public List<WantItemBase> Wants { get; protected set; }

        public decimal BestTotalPrices { get; protected set; }

        public int WantsCount { get; protected set; }

        public SellerItem(ArticleSeller seller)
        {
            Seller = seller;
            Articles = new List<ArticleItem>();
            Wants = new List<WantItemBase>();
        }

        public void AddArticle(WantItemBase wantItem, ArticleItem article)
        {
            Articles.Add(article);
            if (Wants.All(x => x.Want.Id != wantItem.Want.Id))
                Wants.Add(wantItem);
        }

        public void AddArticles(WantItemBase wantItem, IEnumerable<ArticleItem> article)
        {
            Articles.AddRange(article);
            if (Wants.All(x => x.Want.Id != wantItem.Want.Id))
                Wants.Add(wantItem);
        }

        public void ComputeBestTotalPrice()
        {
            BestTotalPrices = 0;
            foreach (WantItemBase want in Wants)
            {
                decimal sumByWant = 0;
                int count = want.Count;
                foreach (ArticleItem article in Articles.Where(x => want.Articles.Any(a => a.Article.Id == x.Article.Id)).OrderBy(x => x.Article.Price))
                {
                    if (article.Article.IsPlayset && count >= 4)
                    {
                        sumByWant += article.Article.Price;
                        WantsCount += 4;
                        count -= 4;
                    }
                    else if (article.Article.Count >= count)
                    {
                        sumByWant += count*article.Article.Price;
                        WantsCount += count;
                        count = 0;
                    }
                    else
                    {
                        sumByWant += article.Article.Count*article.Article.Price;
                        WantsCount += article.Article.Count;
                        count -= article.Article.Count;
                    }
                    if (count == 0)
                        break;
                }
                BestTotalPrices += sumByWant;
            }
        }
    }

    public class SellerItemDesignData : SellerItem
    {
        public SellerItemDesignData()
            : base(new ArticleSeller {UserName = "tsekwa"})
        {
        }
    }
}
