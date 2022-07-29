using System.Collections.Generic;

namespace WebShopApp.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
