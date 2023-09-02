using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Validation;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class EditArticleGroupViewModel : BaseCreateAndDeleteArticleGroupViewModel
    {
        public static EditArticleGroupViewModel LoadViewModel(ManagerStore managerStore, ArticleGroupDTO initialArticleGroup, NavigationService articleGroupListingViewModelNavigationService, IArticleGroupValidator articleGroupValidator)
        {
            EditArticleGroupViewModel viewModel = new EditArticleGroupViewModel(managerStore, initialArticleGroup, articleGroupListingViewModelNavigationService, articleGroupValidator);
            viewModel.LoadArticleGroupsCommand?.Execute(null);
            return viewModel;
        }

        private EditArticleGroupViewModel(ManagerStore managerStore, ArticleGroupDTO initialArticleGroup, NavigationService articleGroupListingViewModelNavigationService, IArticleGroupValidator articleGroupValidator)
            : base(managerStore, articleGroupValidator)
        {
            _initialArticleGroup = initialArticleGroup;

            Name = initialArticleGroup.Name;
            SuperiorArticleGroup = initialArticleGroup.SuperiorArticleGroup;
            IsRootElement = SuperiorArticleGroup == null;

            SaveChangesToArticleGroupCommand = new UpdateArticleGroupCommand(managerStore, this, initialArticleGroup, articleGroupListingViewModelNavigationService);
            CancelEdtiArticleGroupCommand = new NavigateCommand(articleGroupListingViewModelNavigationService);
        }

        public ICommand SaveChangesToArticleGroupCommand { get; }
		public ICommand CancelEdtiArticleGroupCommand{ get; }
        public int Id => _initialArticleGroup.Id;

        protected override void FurtherValidateSuperiorArticleGroup(string propertyName)
        {
            base.FurtherValidateSuperiorArticleGroup(propertyName);

            if (SuperiorArticleGroup == null)
                return;

            if (SuperiorArticleGroup.Id == _initialArticleGroup.Id)
                AddError("Article group cannot be its own parent!", propertyName);

            if (RefferesAChildRec(_initialArticleGroup.SubordinateArticleGroups, SuperiorArticleGroup.Id))
                AddError("Superior article group reffers one of its own childs!", propertyName);
        }

        private bool RefferesAChildRec(IEnumerable<ArticleGroupDTO> children, int id)
        {
            foreach (var child in children)
            {
                if (child.Id == id)
                    return true;

                if (RefferesAChildRec(child.SubordinateArticleGroups, id))
                    return true;
            }

            return false;
        }

        private readonly ArticleGroupDTO _initialArticleGroup;
    }
}
