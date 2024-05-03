using NBitcoin;

namespace BitTrade.Services
{
    public class BitCoinService
    {
        public string GeneratePrivateKey()
        {
            var privateKey = new Key();
            //var bitcoinPrivateKey = privateKey.GetWif(Network.Main).ToString();
            var bitcoinPrivateKey = privateKey.GetWif(Network.TestNet).ToString();

            return bitcoinPrivateKey;
        }

        public string GeneratePublicKey()
        {
            var privateKey = new Key();
            //var bitcoinPrivateKey = privateKey.GetWif(Network.Main);
            var bitcoinPrivateKey = privateKey.GetWif(Network.TestNet);
            var bitcoinPublicKey = bitcoinPrivateKey.PubKey.ToString();

            return bitcoinPublicKey;
        }

        public BitcoinAddress CreateBitCoinsAddress()
        {
            var privateKey = new Key();
            var bitcoinPrivateKey = privateKey.GetWif(Network.Main);
            var bitcoinPublicKey = bitcoinPrivateKey.PubKey;
            //var address = bitcoinPublicKey.GetAddress(ScriptPubKeyType.Segwit, Network.Main);
            var address = bitcoinPublicKey.GetAddress(ScriptPubKeyType.Segwit, Network.TestNet);

            return address;
        }

        public BitcoinAddress GenerateAddressForKnownPrivateKey()
        {
            var key = GeneratePrivateKey();
            var bitcoinSecret = new BitcoinSecret(key, Network.Main);
            //var address = bitcoinSecret.PubKey.GetAddress(ScriptPubKeyType.Segwit, Network.Main);
            var address = bitcoinSecret.PubKey.GetAddress(ScriptPubKeyType.Segwit, Network.TestNet);

            return address;
        }

        public Mnemonic GenerateMnemonic()
        {
            var mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);

            return mnemo;
        }

        public BitcoinAddress GenerateAddressFromMnemonic()
        {
            var mnemo = GenerateMnemonic();
            var hdroot = mnemo.DeriveExtKey();
            var pKey = hdroot.Derive(new KeyPath("m/84'/0'/0'/0/0"));
            //var address = pKey.PrivateKey.PubKey.GetAddress(ScriptPubKeyType.Segwit, Network.Main);
            var address = pKey.PrivateKey.PubKey.GetAddress(ScriptPubKeyType.Segwit, Network.TestNet);


            return address;
        }
    }
}
