using DataLayer.Customers.DTOs;

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
        public CustomerDTO Customer { get; }
        public IEnumerable<PositionDTO> Positions { get; }
    }
}
