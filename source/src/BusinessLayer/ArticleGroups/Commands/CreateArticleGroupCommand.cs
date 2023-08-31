﻿using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.DTOs;
using System.ComponentModel;

namespace BusinessLayer.ArticleGroups.Commands
{
    public class CreateArticleGroupCommand : BaseAsyncCommand, IDisposable
    {
        public CreateArticleGroupCommand(ManagerStore managerStore, CreateArticleGroupViewModel createArticleGroupViewModel, NavigationService articleGroupListingViewModelNavigationService)
        {
            _managerStore = managerStore;
            _createArticleGroupViewModel = createArticleGroupViewModel;
            _articleGroupListingViewModelNavigationService = articleGroupListingViewModelNavigationService;

            _createArticleGroupViewModel.ErrorsChanged += OnCreateArticleGroupViewModelHasErrorsChanged;
        }


        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter) && !_createArticleGroupViewModel.HasErrors;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            if (_createArticleGroupViewModel.IsRootElement)
                await CreateRootArticleGroupAsync();
            else
            {
                await CreateSubordinateArticleGroupAsync();
            }

            _articleGroupListingViewModelNavigationService.Navigate();
        }

        private async Task CreateRootArticleGroupAsync()
        {
            CreatedOrUpdatedArticleGroupDTO articleGroup = new CreatedOrUpdatedArticleGroupDTO(
                await _managerStore.GetNextFreeArticleGroupIdAsync(),
                _createArticleGroupViewModel.Name);

            await _managerStore.CreateRootArticleGroupAsync(articleGroup);
        }
        private async Task CreateSubordinateArticleGroupAsync()
        {
            if (_createArticleGroupViewModel.SuperiorArticleGroup == null)
                return;

            CreatedOrUpdatedArticleGroupDTO articleGroup = new CreatedOrUpdatedArticleGroupDTO(
                await _managerStore.GetNextFreeArticleGroupIdAsync(),
                _createArticleGroupViewModel.Name,
                _createArticleGroupViewModel.SuperiorArticleGroup);

            await _managerStore.CreateSubordinateArticleGroupAsync(articleGroup);
        }
        private void OnCreateArticleGroupViewModelHasErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        void IDisposable.Dispose()
        {
            _createArticleGroupViewModel.ErrorsChanged -= OnCreateArticleGroupViewModelHasErrorsChanged;
        }

        private readonly ManagerStore _managerStore;
        private readonly CreateArticleGroupViewModel _createArticleGroupViewModel;
        private readonly NavigationService _articleGroupListingViewModelNavigationService;
    }
}