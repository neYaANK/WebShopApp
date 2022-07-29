using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopApp.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Invalid name")]
        [StringLength(20)]
        public string PhoneModel { get; set; }
        [Required(ErrorMessage = "Invalid description")]
        public string PhoneDescription { get; set; }
        [Required]
        public int Price { get; set; }
        public string PhoneImage { get; set; }
        [NotMapped]
        public IFormFile PhoneImageFile { get; set; }
        public int CountPhones { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }        
        public int BrandId { get; set; }
        public Brand Brand{ get; set; }        
        public int CountryId{ get; set; }
        public Country Country{ get; set; }
    }
}
