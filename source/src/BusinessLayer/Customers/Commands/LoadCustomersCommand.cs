using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.ViewModels;

namespace BusinessLayer.Customers.Commands
{
    public class LoadCustomersCommand : BaseAsyncCommand
    {
        public LoadCustomersCommand(ManagerStore managerStore, ICustomerUpdatable customerListingViewModel)
        {
            _managerStore = managerStore;
            _customerUpdatable = customerListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _customerUpdatable.ErrorMessage = string.Empty;
            _customerUpdatable.IsLoading = true;

            try
            {
                await _managerStore.Load();

                _customerUpdatable.UpdateCustomers(_managerStore.Customers);
            }
            catch (Exception)
            {
                _customerUpdatable.ErrorMessage = "Failed to load customers.";
            }
            finally
            {
                _customerUpdatable.IsLoading = false;
            }
        }

        private readonly ManagerStore _managerStore;
        private readonly ICustomerUpdatable _customerUpdatable;
    }
}
