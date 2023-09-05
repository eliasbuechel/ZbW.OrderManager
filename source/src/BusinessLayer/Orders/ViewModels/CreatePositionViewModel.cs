using BusinessLayer.Articles.Commands;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Orders.Commands;
using DataLayer.Articles.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class CreatePositionViewModel : BaseCreateEditPositionViewModel
    {
        public static CreatePositionViewModel LoadViewModel(ManagerStore managerStore, SubNavigationService createPositionViewModelSubNavigationService, CreateOrderViewModel createOrderViewModel, int nextFreeNumber)
        {
            CreatePositionViewModel viewModel = new CreatePositionViewModel(managerStore, createPositionViewModelSubNavigationService, createOrderViewModel, nextFreeNumber);
            viewModel.LoadArticlesCommand?.Execute(null);
            return viewModel;
        }

        private CreatePositionViewModel(ManagerStore managerStore, SubNavigationService createPositionViewModelSubNavigationService, CreateOrderViewModel createOrderViewModel, int nextFreeNumber)
            : base(managerStore)
        {
            CreatePositionCommand = new CreatePositionCommand(createOrderViewModel, this, nextFreeNumber, createPositionViewModelSubNavigationService);
            CancelCreatePositionCommand = new NavigateCommand(createPositionViewModelSubNavigationService);

            Ammount = 1;
            Article = null;
        }

        public ICommand CreatePositionCommand { get; }
        public ICommand CancelCreatePositionCommand { get; }
    }
}
