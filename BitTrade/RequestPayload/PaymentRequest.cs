using System.ComponentModel.DataAnnotations;

namespace BitTrade.RequestPayload
{
    public class PaymentRequest
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public double Amount { get; set; }
        public string BitcoinAddress { get; set; }
        public double BitcoinAmount { get; set; }
    }
}
