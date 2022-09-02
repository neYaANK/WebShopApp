using System.ComponentModel.DataAnnotations;

namespace WebShopAPI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Passwords not same")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        [Required]
        public int Year{ get; set; }
    }
}
