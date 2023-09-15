using BusinessLayer.ArticleGroups.ViewModels;
using System.Collections.ObjectModel;

namespace BusinessLayer.Base.ViewModels
{
    public class ArticleGroupCollectionView : ObservableCollection<ArticleGroupViewModel>
    {
        public ArticleGroupCollectionView(IEnumerable<ArticleGroupViewModel> source)
            : base(new ObservableCollection<ArticleGroupViewModel>())
        {
            _source = source;
            Filter = x => true;
            OrderKeySelector = x => x;

            Update();
        }

        public void Update()
        {
            Clear();
            IEnumerable<ArticleGroupViewModel> viewable = _source
                .Where(Filter)
                .OrderBy(OrderKeySelector)
                .ToList();

            foreach (ArticleGroupViewModel item in viewable)
            {
                Add(item);
            }
        }


        public Func<ArticleGroupViewModel, bool> Filter { private get; set; }
        public Func<ArticleGroupViewModel, object> OrderKeySelector { private get; set; }

        private readonly IEnumerable<ArticleGroupViewModel> _source;
    }
}
