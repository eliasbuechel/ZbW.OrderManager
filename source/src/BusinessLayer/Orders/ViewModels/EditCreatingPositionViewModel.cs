using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Articles.Commands;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Orders.Commands;
using DataLayer.Articles.DTOs;
using DataLayer.Orders.DTOs;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class EditCreatingPositionViewModel : BaseCreateEditPositionViewModel
    {
        public static EditCreatingPositionViewModel LoadViewModel(ManagerStore managerStore, SubNavigationService createPositionViewModelSubNavigationService, CreatingPositionDTO position)
        {
            EditCreatingPositionViewModel viewModel = new EditCreatingPositionViewModel(managerStore, createPositionViewModelSubNavigationService, position);
            viewModel.LoadArticlesCommand?.Execute(null);
            return viewModel;
        }

        private EditCreatingPositionViewModel(ManagerStore managerStore, SubNavigationService createPositionViewModelSubNavigationService, CreatingPositionDTO initialPosition)
            : base(managerStore)
        {
            _initialPosition = initialPosition;

            UpdatePositionCommand = new UpdateCreatingPositionCommand(createPositionViewModelSubNavigationService, this, initialPosition);
            CancelUpdatePositionCommand = new NavigateCommand(createPositionViewModelSubNavigationService);

            Article = initialPosition.Article;
            Ammount = initialPosition.Ammount;
        }

        public ICommand UpdatePositionCommand { get; }
        public ICommand CancelUpdatePositionCommand { get; }
        public int Number => _initialPosition.Number;

        private int _number;
        private readonly CreatingPositionDTO _initialPosition;
    }
}
