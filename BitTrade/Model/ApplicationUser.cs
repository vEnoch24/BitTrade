using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitTrade.Model
{
    [Table("ApplicationUser")]
    public class ApplicationUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Unicode(false)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public byte[] passwordhash { get; set; } = new byte[32];
        public byte[] passwordSalt { get; set; } = new byte[32];
        //public override string Id { get; set; } = Guid.NewGuid().ToString();

        public List<TransactionModel> Transactions { get; set; }
    }
}
