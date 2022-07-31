using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopApp.ViewModel
{
    public class ContactDataViewModel
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "City required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Address required")]
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
