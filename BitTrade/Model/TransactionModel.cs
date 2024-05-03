using System.ComponentModel.DataAnnotations;

namespace BitTrade.Model
{
    public class TransactionModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public double Amount { get; set; }
        public string TrxRef { get; set; }
        public string Email { get; set; }
        public string BitcoinAddress { get; set; }
        public double BitcoinAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Status { get; set; }

        public Guid ApplicationUserId { get; set; }
        
    }
}
