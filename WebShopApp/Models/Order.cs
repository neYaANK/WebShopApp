using System.Collections.Generic;

namespace WebShopApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string RecieverName { get; set; }
        public string RecieverSurname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public List<OrderItem> OrderItems{ get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
