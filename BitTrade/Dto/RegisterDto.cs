using System.ComponentModel.DataAnnotations;

namespace BitTrade.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
