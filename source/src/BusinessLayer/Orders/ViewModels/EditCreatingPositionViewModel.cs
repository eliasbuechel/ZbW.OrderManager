using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Orders.Commands;
using DataLayer.Orders.DTOs;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class EditCreatingPositionViewModel : BaseCreateEditPositionViewModel
    {
        public static EditCreatingPositionViewModel LoadViewModel(ManagerStore managerStore, FromSubNavigationService<CreateOrderViewModel> createPositionViewModelNavigateBackService, CreatingPositionDTO position)
        {
            EditCreatingPositionViewModel viewModel = new EditCreatingPositionViewModel(managerStore, createPositionViewModelNavigateBackService, position);
            viewModel.LoadArticlesCommand?.Execute(null);
            return viewModel;
        }

        private EditCreatingPositionViewModel(ManagerStore managerStore, FromSubNavigationService<CreateOrderViewModel> createPositionViewModelNavigateBackService, CreatingPositionDTO initialPosition)
            : base(managerStore)
        {
            _initialPosition = initialPosition;

            UpdatePositionCommand = _updatePositionCommand = new UpdateCreatingPositionCommand(createPositionViewModelNavigateBackService, this, initialPosition);
            CancelUpdatePositionCommand = new NavigateCommand(createPositionViewModelNavigateBackService);

            Article = initialPosition.Article;
            Ammount = initialPosition.Ammount;
        }

        public ICommand UpdatePositionCommand { get; }
        public ICommand CancelUpdatePositionCommand { get; }
        public int Number => _initialPosition.Number;

        public override void Dispose(bool disposing)
        {
            _updatePositionCommand.Dispose();
        }

        private readonly CreatingPositionDTO _initialPosition;
        private readonly UpdateCreatingPositionCommand _updatePositionCommand;
    }
}