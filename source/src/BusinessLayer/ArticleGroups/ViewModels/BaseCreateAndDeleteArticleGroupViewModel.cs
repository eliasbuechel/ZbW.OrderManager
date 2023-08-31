using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public abstract class BaseCreateAndDeleteArticleGroupViewModel : ContainingArticleGroupViewModel, IArticleGroupUpdatable
    {
        protected BaseCreateAndDeleteArticleGroupViewModel(ManagerStore managerStore, string name, ArticleGroupDTO? superiorArticleGroup)
            : base(managerStore)
        {
            Name = name;
            SuperiorArticleGroup = superiorArticleGroup;
            IsRootElement = superiorArticleGroup == null;
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

                int maxPropertyLength = 40;

                ClearErrors();
                if (string.IsNullOrEmpty(Name))
                    AddError(EMPTY_MESSAGE);
                if (Name.Length > maxPropertyLength)
                    AddError(ToLongErrorMessage(maxPropertyLength));
            }
        }
        public ArticleGroupDTO? SuperiorArticleGroup
        {
            get
            {
                return _superiorArticleGroup;
            }
            set
            {
                _superiorArticleGroup = value;
                OnPropertyChanged(nameof(SuperiorArticleGroup));

                ClearErrors();
                if (!IsRootElement && SuperiorArticleGroup == null)
                    AddError(EMPTY_MESSAGE);
                FurtherValidateSuperiorArticleGroup(nameof(SuperiorArticleGroup));
            }
        }
        public bool IsRootElement
        {
            get
            {
                return _isRootElement;
            }
            set
            {
                _isRootElement = value;
                OnPropertyChanged();

                ClearErrors(nameof(SuperiorArticleGroup));
                if (!IsRootElement && SuperiorArticleGroup == null)
                    AddError(EMPTY_MESSAGE, nameof(SuperiorArticleGroup));
            }
        }

        protected virtual void FurtherValidateSuperiorArticleGroup(string propertyName)
        {
        }

        private bool _isRootElement;
        private string _name = string.Empty;
        private ArticleGroupDTO? _superiorArticleGroup = null;
    }
}
