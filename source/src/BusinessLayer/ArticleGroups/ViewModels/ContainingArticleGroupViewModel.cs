using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public abstract class ContainingArticleGroupViewModel : BaseLoadableViewModel, IArticleGroupUpdatable
    {
        protected ContainingArticleGroupViewModel(ManagerStore managerStore)
        {
            LoadArticleGroupsCommand = new LoadArticleGroupsCommand(managerStore, this);
        }

        protected ICommand LoadArticleGroupsCommand { get; }
        public IEnumerable<ArticleGroupDTO> ArticleGroups => _articleGroups;

        public void UpdateArticleGroups(IEnumerable<ArticleGroupDTO> articleGroups)
        {
            _articleGroups.Clear();

            foreach (var articleGroup in articleGroups)
            {
                _articleGroups.Add(articleGroup);
            }
        }

        private readonly ObservableCollection<ArticleGroupDTO> _articleGroups = new ObservableCollection<ArticleGroupDTO>();
    }
}
