using System.ComponentModel.DataAnnotations;

namespace Amazon.API_s.DTO_s
{
    public class BasketItemDto
    {
        [Required]
        public int ProductId { get; set; }   // ✅ REQUIRED
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="quantity must be 1 at least")]
        public int Quantinty { get; set; }

        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}