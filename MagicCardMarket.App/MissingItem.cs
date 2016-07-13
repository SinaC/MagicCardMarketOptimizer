using MagicCardMarket.MVVM;

namespace MagicCardMarket.App
{
    public class MissingItem : ObservableObject
    {
        private WantItemBase _want;

        public WantItemBase Want
        {
            get { return _want; }
            set { Set(() => Want, ref _want, value); }
        }

        private int _count;

        public int Count
        {
            get { return _count; }
            set { Set(() => Count, ref _count, value); }
        }

        public MissingItem(WantItemBase want, int count)
        {
            Want = want;
            Count = count;
        }
    }

    public class MissingItemDesignData : MissingItem
    {
        public MissingItemDesignData()
            : base(new MetaProductItemDesignData("toto"), 5)
        {
        }
    }
}
