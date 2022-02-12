using System.ComponentModel.DataAnnotations;

namespace ProductCatalogManagerAPI.Models
{
    public class ProductDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [RegularExpression(@"^[0-9]+$",ErrorMessage = "Please make sure Code to be a numeric value")]
        [Required]
        [Key]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [RegularExpression(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)+$",ErrorMessage = "Picture URL is not valid")]
        public string? Picture { get; set; }

        [Range(0, 999, ErrorMessage = "Price should between 0 to 999")]
        [Required(ErrorMessage = "You have to specify a price")]
        public decimal Price { get; set; }


        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
