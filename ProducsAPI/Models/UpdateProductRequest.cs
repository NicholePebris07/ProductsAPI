using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Models
{
    public class UpdateProductRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "100000")]
        public decimal Price { get; set; }

        public decimal? SalePrice { get; set; }
    }
}
