using System.ComponentModel.DataAnnotations;

namespace Buoi5.Models
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductDescriptiion { get; set; }
        public string? CategoryName { get; set; }
    }
}
