using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Orders.ViewModels;
using DataLayer.Orders.DTOs;
using System.ComponentModel;

namespace BusinessLayer.Orders.Commands
{
    public class UpdateCreatingPositionCommand : BaseCommand, IDisposable
    {
        public UpdateCreatingPositionCommand(SubNavigationService createPositionViewModelSubNavigationService, EditCreatingPositionViewModel editCreatingPositionViewModel, CreatingPositionDTO position)
        {
            _createPositionViewModelSubNavigationService = createPositionViewModelSubNavigationService;
            _editCreatingPositionViewModel = editCreatingPositionViewModel;
            _position = position;

            editCreatingPositionViewModel.ErrorsChanged += OnEditCreatingPositionViewModelErrorsChanged;
            editCreatingPositionViewModel.PropertyChanged += OnEditCreatingPositionViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && !_editCreatingPositionViewModel.HasErrors
                && DataHasChanged();
        }

        public override void Execute(object? parameter)
        {
            if (_editCreatingPositionViewModel.Article == null)
                return;

            CreatingPositionDTO position = new CreatingPositionDTO(
                _editCreatingPositionViewModel.Number,
                _editCreatingPositionViewModel.Ammount,
                _editCreatingPositionViewModel.Article
                );

            _position.Update(position);
            _createPositionViewModelSubNavigationService.Navigate();
        }
        public void Dispose()
        {
            _editCreatingPositionViewModel.ErrorsChanged -= OnEditCreatingPositionViewModelErrorsChanged;
            _editCreatingPositionViewModel.PropertyChanged -= OnEditCreatingPositionViewModelPropertyChanged;
        }

        private void OnEditCreatingPositionViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
        private void OnEditCreatingPositionViewModelErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
        private bool DataHasChanged()
        {
            return _editCreatingPositionViewModel.Ammount != _position.Ammount
                || _editCreatingPositionViewModel.Article != _position.Article;
        }


        private readonly SubNavigationService _createPositionViewModelSubNavigationService;
        private readonly EditCreatingPositionViewModel _editCreatingPositionViewModel;
        private readonly CreatingPositionDTO _position;
    }
}
