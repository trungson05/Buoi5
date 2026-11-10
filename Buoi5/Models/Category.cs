using System.ComponentModel.DataAnnotations;

namespace Buoi5.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        public List<Category> Categories = new List<Category>();
    }
}
