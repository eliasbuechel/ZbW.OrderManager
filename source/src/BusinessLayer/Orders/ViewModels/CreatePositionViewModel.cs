using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Orders.Commands;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class CreatePositionViewModel : BaseCreateEditPositionViewModel
    {

        public static CreatePositionViewModel LoadViewModel(ManagerStore managerStore, FromSubNavigationService<CreateOrderViewModel> createPositionViewModelNavigateBackService, CreateOrderViewModel createOrderViewModel, int nextFreeNumber)
        {
            CreatePositionViewModel viewModel = new CreatePositionViewModel(managerStore, createPositionViewModelNavigateBackService, createOrderViewModel, nextFreeNumber);
            viewModel.LoadArticlesCommand?.Execute(null);
            return viewModel;
        }

        private CreatePositionViewModel(ManagerStore managerStore, FromSubNavigationService<CreateOrderViewModel> createPositionViewModelNavigateBackService, CreateOrderViewModel createOrderViewModel, int nextFreeNumber)
            : base(managerStore)
        {
            CreatePositionCommand = _createPositionCommand = new CreatePositionCommand(createOrderViewModel, this, nextFreeNumber, createPositionViewModelNavigateBackService);
            CancelCreatePositionCommand = new NavigateCommand(createPositionViewModelNavigateBackService);

            Ammount = 1;
            Article = null;
        }

        public ICommand CreatePositionCommand { get; }
        public ICommand CancelCreatePositionCommand { get; }

        public override void Dispose(bool disposing)
        {
            _createPositionCommand.Dispose();
        }

        private readonly CreatePositionCommand _createPositionCommand;
    }
}
