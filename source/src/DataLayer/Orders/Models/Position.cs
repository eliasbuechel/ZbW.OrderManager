using DataLayer.Articles.Models;
using Microsoft.Identity.Client;

namespace DataLayer.Orders.Models
{
    public class Position
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Article Article { get; set; }
        public int Ammount { get; set; }
        public Order Order { get; set; }
    }
}
