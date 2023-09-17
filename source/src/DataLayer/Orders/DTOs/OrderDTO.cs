using DataLayer.Customers.DTOs;
using System.Diagnostics;

namespace DataLayer.Orders.DTOs
{
    public class OrderDTO
    {
        public OrderDTO(int id, DateTime timeStamp, CustomerDTO customer, IEnumerable<PositionDTO> positions)
        {
            Id = id;
            Customer = customer;
            Positions = positions;
            TimeStamp = timeStamp;
        }

        public int Id { get; }
        public DateTime TimeStamp { get; }
        public CustomerDTO Customer { get; private set; }
        public IEnumerable<PositionDTO> Positions { get; }

        public void UpdateCustomer(CustomerDTO customer)
        {
            Debug.Assert(customer.Id == Customer.Id);
            Customer = customer;
        }
    }
}
