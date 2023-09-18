using DataLayer.Customers.DTOs;

namespace DataLayer.Orders.DTOs
{
    public class CreatingOrderDTO
    {
        public CreatingOrderDTO(CustomerDTO customer, DateTime timeStamp, IEnumerable<CreatingPositionDTO> positions)
        {
            Customer = customer;
            TimeStamp = timeStamp;
            Positions = positions;
        }

        public DateTime TimeStamp { get; set; }
        public CustomerDTO Customer { get; }
        public IEnumerable<CreatingPositionDTO> Positions { get; }
    }
}
