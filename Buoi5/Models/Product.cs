using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buoi5.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Ten khong duoc qua 100 ky tu!")]
        [StringLength(100)]
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        [Required]
        [StringLength(10000)]
        public string? ProductDescription { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
