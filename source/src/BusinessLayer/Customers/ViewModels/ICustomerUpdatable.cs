using BusinessLayer.Base.ViewModels;
using DataLayer.Customers.DTOs;

namespace BusinessLayer.Customers.ViewModels
{
    public interface ICustomerUpdatable : IUpdatable
    {
        void UpdateCustomers(IEnumerable<CustomerDTO> customers);
    }
}
