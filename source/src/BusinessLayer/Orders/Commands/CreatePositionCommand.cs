using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Orders.ViewModels;
using DataLayer.Orders.DTOs;
using System.ComponentModel;

namespace BusinessLayer.Orders.Commands
{
    public class CreatePositionCommand : BaseCommand, IDisposable
    {
        public CreatePositionCommand(CreateOrderViewModel createOrderViewModel, CreatePositionViewModel createPositionViewModel, int nextFreeNumber, FromSubNavigationService<CreateOrderViewModel> createPositionViewModelNavigateBackService)
        {
            _createOrderViewModel = createOrderViewModel;
            _createPositionViewModel = createPositionViewModel;
            _nextFreeNumber = nextFreeNumber;
            _createPositionViewModelNavigateBackService = createPositionViewModelNavigateBackService;

            createPositionViewModel.ErrorsChanged += OnCreatePositionViewModelErrorsChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && !_createPositionViewModel.HasErrors;
        }
        public override void Execute(object? parameter)
        {
            if (_createPositionViewModel.Article == null)
                return;

            CreatingPositionDTO position = new CreatingPositionDTO(
                _nextFreeNumber,
                _createPositionViewModel.Ammount,
                _createPositionViewModel.Article
                );

            _createOrderViewModel.AddPosition(position);
            _createPositionViewModelNavigateBackService.Navigate();
        }
        public void Dispose()
        {
            _createPositionViewModel.ErrorsChanged += OnCreatePositionViewModelErrorsChanged;
        }

        private void OnCreatePositionViewModelErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }


        private CreateOrderViewModel _createOrderViewModel;
        private CreatePositionViewModel _createPositionViewModel;
        private readonly int _nextFreeNumber;
        private readonly FromSubNavigationService<CreateOrderViewModel> _createPositionViewModelNavigateBackService;
    }
}
