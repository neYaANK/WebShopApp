using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebShopApp.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public List<Order> Orders { get; set; }
    }
}
