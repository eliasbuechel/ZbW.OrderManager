using DataLayer.Customers.Models;

namespace DataLayer.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public Customer Customer { get; set; } = new Customer();
        public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
    }
}
