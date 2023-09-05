using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Orders.Commands;
using DataLayer.Articles.DTOs;
using DataLayer.Orders.DTOs;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class CreatingPositionViewModel
    {
        public CreatingPositionViewModel(ManagerStore managerStore, NavigationStore navigationStore, CreatingPositionDTO position, CreateOrderViewModel createOrderViewModel)
        {
            _managerStore = managerStore;
            _position = position;

            _editCreatingPositionViewModelSubNavigationService = new SubNavigationService(navigationStore, createOrderViewModel, CreateEditCreatingPositionViewModel);

            EditCreatingPositionCommand = new NavigateCommand(_editCreatingPositionViewModelSubNavigationService);
            DeleteCreatingPositioncommand = new DeleteCreatingPositionCommand(createOrderViewModel, position);
        }

        private EditCreatingPositionViewModel CreateEditCreatingPositionViewModel()
        {
            return EditCreatingPositionViewModel.LoadViewModel(_managerStore, _editCreatingPositionViewModelSubNavigationService, _position);
        }

        public ICommand EditCreatingPositionCommand { get; }
        public ICommand DeleteCreatingPositioncommand { get; }
        public int Number => _position.Number;
        public ArticleDTO Article => _position.Article;
        public int Ammount => _position.Ammount;

        private readonly ManagerStore _managerStore;
        private readonly CreatingPositionDTO _position;
        private readonly SubNavigationService _editCreatingPositionViewModelSubNavigationService;
    }
}
