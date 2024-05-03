using System.ComponentModel.DataAnnotations;

namespace BitTrade.Dto
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
