using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.DTOs;

namespace BusinessLayer.Articles.ViewModels
{
    public class BaseCreateEditArticleViewModel : ContainingArticleGroupViewModel
    {
        protected BaseCreateEditArticleViewModel(ManagerStore managerStore, string name, ArticleGroupDTO? articleGroup = null)
            : base(managerStore)
        {
            Name = name;
            ArticleGroup = articleGroup;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();

                const int maxCharacterSize = 100;

                ClearErrors();
                if (string.IsNullOrEmpty(Name))
                    AddError(EMPTY_MESSAGE);
                if (Name.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
            }
        }
        public ArticleGroupDTO? ArticleGroup
        {
            get
            {
                return _articleGroup;
            }
            set
            {
                _articleGroup = value;
                OnPropertyChanged();

                ClearErrors();
                if (_articleGroup == null)
                    AddError(EMPTY_MESSAGE);
            }
        }

        private string _name = string.Empty;
        private ArticleGroupDTO? _articleGroup;
    }
}