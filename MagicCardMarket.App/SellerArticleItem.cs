using MagicCardMarket.MVVM;

namespace MagicCardMarket.App
{
    public class SellerArticleItem : ObservableObject
    {
        private ArticleItem _articleItem;
        public ArticleItem ArticleItem
        {
            get { return _articleItem; }
            set { Set(() => ArticleItem, ref _articleItem, value); }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { Set(() => Count, ref _count, value); }
        }

        public SellerArticleItem(ArticleItem item, int count)
        {
            ArticleItem = item;
            Count = count;
        }
    }

    public class SellerArticleItemDesignData : SellerArticleItem
    {
        public SellerArticleItemDesignData() : base(new ArticleItemDesignData(), 10)
        {
        }
    }
}
