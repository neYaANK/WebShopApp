using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebShopApp.Models
{
    public class User : IdentityUser
    {
        //public string Username { get; set; }
        //public List<Order> Orders { get; set; }
        public int Year{ get; set; }
    }
}
