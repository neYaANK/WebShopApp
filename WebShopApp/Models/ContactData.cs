using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShopApp.Models
{
    public class ContactData
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        
    }
}
