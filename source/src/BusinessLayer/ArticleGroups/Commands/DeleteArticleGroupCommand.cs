using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.DTOs;
using System.ComponentModel;

namespace BusinessLayer.ArticleGroups.Commands
{
    public class DeleteArticleGroupCommand : BaseAsyncCommand
    {
        public DeleteArticleGroupCommand(ManagerStore managerStore, ArticleGroupDTO articleGroup, ArticleGroupViewModel articleGroupViewModel)
        {
            _managerStore = managerStore;
            _articleGroup = articleGroup;
            _articleGroupViewModel = articleGroupViewModel;

            _articleGroupViewModel.PropertyChanged += OnArticleGroupViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !_articleGroup.SubordinateArticleGroups.Any() && base.CanExecute(parameter);
        }
        public async override Task ExecuteAsync(object? parameter)
        {
            await _managerStore.DeleteArticleGroup(_articleGroup);
        }

        private void OnArticleGroupViewModelPropertyChanged(object? sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(ArticleGroupViewModel.SubordinateArticleGroups))
                OnCanExecuteChanged();
        }

        private readonly ManagerStore _managerStore;
        private readonly ArticleGroupDTO _articleGroup;
        private readonly ArticleGroupViewModel _articleGroupViewModel;
    }
}
