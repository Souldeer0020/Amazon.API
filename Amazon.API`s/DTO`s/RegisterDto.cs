using System.ComponentModel.DataAnnotations;

namespace Amazon.API_s.DTO_s
{
    public class RegisterDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }


        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
