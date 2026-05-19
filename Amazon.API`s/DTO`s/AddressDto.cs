using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amazon.API_s.DTO_s
{
    public class AddressDto
    {
        
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]
        public string street { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        
    }
}
