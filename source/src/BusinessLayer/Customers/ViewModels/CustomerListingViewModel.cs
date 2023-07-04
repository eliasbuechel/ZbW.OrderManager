using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Customers.ViewModels
{
    public class CustomerListingViewModel : BaseViewModel
    {
        public static CustomerListingViewModel LoadViewModel()
        {
            return new CustomerListingViewModel();
        }
    }
}