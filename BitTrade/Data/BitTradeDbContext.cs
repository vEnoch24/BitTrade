using BitTrade.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor.Rendering;

namespace BitTrade.Data
{
    public class BitTradeDbContext : DbContext
    {
        public BitTradeDbContext(DbContextOptions<BitTradeDbContext> options) : base(options)
        {

        }

        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //configures the one - to - many relationship
            modelBuilder.Entity<TransactionModel>()
                     .HasOne<ApplicationUser>()
                     .WithMany(u => u.Transactions)
                     .HasForeignKey(a => a.ApplicationUserId);

        }
    }
}
