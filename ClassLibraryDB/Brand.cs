using System.ComponentModel.DataAnnotations;

namespace ClassLibraryDB
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
