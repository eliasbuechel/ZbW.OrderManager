﻿using BusinessLayer.ArticleGroups.Models;
using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.ViewModels;
using BusinessLayer.Dashboard.ViewModels;
using BusinessLayer.Orders.ViewModels;
using Microsoft.Identity.Client;
using System.Windows.Input;

namespace BusinessLayer.Base.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(NavigationStore navigationStore, NavigationService<DashboardViewModel> dashboardViewModelNavigationService, NavigationService<CustomerListingViewModel> customerListingViewModelNavigationService, NavigationService<ArticleGroupListingViewModel> articleGroupListingViewModelNavigationService, NavigationService<ArticleListingViewModel> articleListingViewModelNavigationService, NavigationService<OrderListingViewModel> orderListingViewModelNavigationService)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateToDashboardViewCommand = new NavigateCommand(dashboardViewModelNavigationService);
            NavigateToCustomerListingViewCommand = new NavigateCommand(customerListingViewModelNavigationService);
            NavigateToArticleGroupListingViewCommand = new NavigateCommand(articleGroupListingViewModelNavigationService);
            NavigateToArticleListingViewCommand = new NavigateCommand(articleListingViewModelNavigationService);
            NavigateToOrderListingViewCommand = new NavigateCommand(orderListingViewModelNavigationService);
        }

        public BaseViewModel? CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand NavigateToCustomerListingViewCommand { get; }
        public ICommand NavigateToArticleGroupListingViewCommand { get; }
        public ICommand NavigateToArticleListingViewCommand { get; }
        public ICommand NavigateToOrderListingViewCommand { get; }

        public void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public override void Dispose(bool disposing)
        {
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
        }


        private readonly NavigationStore _navigationStore;
    }
}
