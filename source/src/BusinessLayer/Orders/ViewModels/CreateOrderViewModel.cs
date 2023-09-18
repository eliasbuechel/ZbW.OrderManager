using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Customers.Commands;
using BusinessLayer.Customers.ViewModels;
using BusinessLayer.Orders.Commands;
using DataLayer.Customers.DTOs;
using DataLayer.Orders.DTOs;
using System.Collections.ObjectModel;
using System.Formats.Tar;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class CreateOrderViewModel : BaseLoadableViewModel, ICustomerUpdatable
    {
		public static CreateOrderViewModel LoadViewModel(ManagerStore managerStore, NavigationStore navigationStore, FromSubNavigationService<OrderListingViewModel> orderListingViewModelNavigateBackService)
		{
			CreateOrderViewModel viewModel = new CreateOrderViewModel(managerStore, navigationStore, orderListingViewModelNavigateBackService);
			viewModel.LoadCustomersCommand?.Execute(null);
			return viewModel;
		}

        private CreateOrderViewModel(ManagerStore managerStore, NavigationStore navigationStore, FromSubNavigationService<OrderListingViewModel> orderListingViewModelNavigateBackService)
        {
            _managerStore = managerStore;
            _navigationStore = navigationStore;

			_createOrderViewModelNavigateBackService = new FromSubNavigationService<CreateOrderViewModel>(_navigationStore, this);

            LoadCustomersCommand = new LoadCustomersCommand(managerStore, this);
			AddPositionCommand = new NavigateCommand(new ToSubNavigationService<CreatePositionViewModel>(navigationStore, CreateCreatePositionViewModel));
			CreateOrderCommand = _createOrderCommand = new CreateOrderCommand(managerStore, this, _positions, orderListingViewModelNavigateBackService);
			CancelCreateOrderCommand = new NavigateCommand(orderListingViewModelNavigateBackService);

			Customer = null;
        }

		public ICommand LoadCustomersCommand { get; }
		public ICommand AddPositionCommand { get; }
		public ICommand CreateOrderCommand { get; }
		public ICommand CancelCreateOrderCommand { get; }
        public CustomerDTO? Customer
		{
			get => _customer;
			set
			{
                _customer = value;
				OnPropertyChanged();

				ClearErrors();
				if (Customer == null)
					AddError("No customer selected!");
			}
		}
		public IEnumerable<CustomerDTO> Customers => _customers;
		public IEnumerable<CreatingPositionViewModel> Positions
		{
			get
			{
				ClearErrors();
				if (!_positions.Any())
					AddError("No position has been added!");

				return _positions
					.Select(p => new CreatingPositionViewModel(
						_managerStore,
						_navigationStore,
						p,
						this,
						_createOrderViewModelNavigateBackService
						)
					);
			}
		}
			
        public void UpdateCustomers(IEnumerable<CustomerDTO> customers)
        {
			_customers.Clear();
			foreach (var c in customers)
			{
				_customers.Add(c);
			}
        }
        public void AddPosition(CreatingPositionDTO position)
        {
			_positions.Add(position);
            OnPropertyChanged(nameof(Positions));
        }
        public void RemovePosition(CreatingPositionDTO position)
        {
            _positions.Remove(position);
			OnPropertyChanged(nameof(Positions));
        }
        public override void Dispose(bool disposing)
        {
            _createOrderCommand.Dispose();
        }

        private CreatePositionViewModel CreateCreatePositionViewModel()
		{
			int nextFreeNumber = 0;

			if (_positions.Any())
				nextFreeNumber = _positions.Max(p => p.Number);

			nextFreeNumber++;

			return CreatePositionViewModel.LoadViewModel(
				_managerStore,
				_createOrderViewModelNavigateBackService,
				this,
				nextFreeNumber
				);
        }

		private CustomerDTO? _customer;
        private readonly ManagerStore _managerStore;
        private readonly NavigationStore _navigationStore;
        private readonly CreateOrderCommand _createOrderCommand;
        private readonly ObservableCollection<CustomerDTO> _customers = new ObservableCollection<CustomerDTO>();
		private readonly ObservableCollection<CreatingPositionDTO> _positions = new ObservableCollection<CreatingPositionDTO>();
		private readonly FromSubNavigationService<CreateOrderViewModel> _createOrderViewModelNavigateBackService;
    }
}
