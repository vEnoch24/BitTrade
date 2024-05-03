namespace BitTrade.Methods
{
    public class BitCoinConverter
    {
        public BitCoinConverter() 
        {

        }

        public double Convert(string currencyCode, double amount)
        {
            if(currencyCode == "NGN")
            {
                var result = amount * 0.000000026;
                return result;
            }
            else {
                return 0.0;
            }
        }
    }
}
