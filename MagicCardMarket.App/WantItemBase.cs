using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MagicCardMarket.Models;
using MagicCardMarket.MVVM;

namespace MagicCardMarket.App
{
    public abstract class WantItemBase : ObservableObject
    {
        public Want Want { get; }

        private List<ArticleItem> _articles;
        public List<ArticleItem> Articles
        {
            get { return _articles; }
            protected set { Set(() => Articles, ref _articles, value); }
        }

        public abstract string Name { get; protected set; }

        public int Count => Want.Count;

        public string MinCondition => Want.MinCondition;

        protected WantItemBase(Want want)
        {
            if (want == null)
                throw new ArgumentNullException(nameof(want));

            Want = want;
        }

        public abstract Task LoadAdditionalDatasAsync();

        public abstract Task LoadArticlesAsync();
    }
}
