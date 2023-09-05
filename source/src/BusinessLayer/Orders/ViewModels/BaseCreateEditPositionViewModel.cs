using BusinessLayer.Articles.Commands;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.Articles.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class BaseCreateEditPositionViewModel : BaseLoadableViewModel, IArticleUpdatable
    {
        protected BaseCreateEditPositionViewModel(ManagerStore managerStore)
        {
            LoadArticlesCommand = new LoadArticlesCommand(managerStore, this);
        }

        public ICommand LoadArticlesCommand { get; }
        public int Ammount
        {
            get => _ammount;
            set
            {
                _ammount = value;
                OnPropertyChanged();

                ClearErrors();
                if (Ammount < 1)
                    AddError("Add at least 1!");
            }
        }
        public ArticleDTO? Article
        {
            get => _article;
            set
            {
                _article = value;
                OnPropertyChanged();

                ClearErrors();
                if (Article == null)
                    AddError("No article selected!");
            }
        }
        public IEnumerable<ArticleDTO> Articles => _articles;

        public void UpdateArticles(IEnumerable<ArticleDTO> articles)
        {
            _articles.Clear();
            foreach (var a in articles)
            {
                _articles.Add(a);
            }
        }

        private int _ammount;
        private ArticleDTO? _article;
        private readonly ObservableCollection<ArticleDTO> _articles = new ObservableCollection<ArticleDTO>();
    }
}
