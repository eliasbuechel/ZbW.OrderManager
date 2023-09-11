using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace BusinessLayer.Base.ViewModels
{
    public class CollectionView<T> : ObservableCollection<T>
    {
        public CollectionView(IEnumerable<T> source)
            : base(new ObservableCollection<T>())
        {
            _source = source;
            Filter = x => true;
            OrderKeySelector = x => x;

            Update();
        }

        public void Update()
        {
            Clear();
            IEnumerable<T> viewable = _source
                .Where(Filter)
                .OrderBy(OrderKeySelector)
                .ToList();

            foreach (T item in viewable)
            {
                Add(item);
            }
        }


        public Func<T, bool> Filter { private get; set; }
        public Func<T, object> OrderKeySelector { private get; set; }

        private readonly IEnumerable<T> _source;
    }
}
