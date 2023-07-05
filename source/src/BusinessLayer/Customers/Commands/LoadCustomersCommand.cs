using BusinessLayer.Base.Command;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.ViewModels;

namespace BusinessLayer.Customers.Commands
{
    public class LoadCustomersCommand : BaseAsyncCommand
    {
        private readonly ManagerStore _managerStore;
        private readonly CustomerListingViewModel _customerListingViewModel;

        public LoadCustomersCommand(ManagerStore managerStore, CustomerListingViewModel customerListingViewModel)
        {
            _managerStore = managerStore;
            _customerListingViewModel = customerListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _customerListingViewModel.ErrorMessage = string.Empty;
            _customerListingViewModel.IsLoading = true;
            try
            {
                await _managerStore.Load();

                _customerListingViewModel.UpdateCustomers(_managerStore.Customers);
            }
            catch (Exception)
            {
                _customerListingViewModel.ErrorMessage = "Failed to load customers.";
            }
            finally
            {
                _customerListingViewModel.IsLoading = false;
            }

        }
    }
}
