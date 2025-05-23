using CryptoPage.Models;

namespace CryptoPage.Views.ViewModels
{
    public class CryptoViewModels
    {
        public List<CryptoCurrency> Coin { get; set; }
        public Dictionary<string,CryptoDetails> Price { get; set; }

        public string search_CryptoName { get; set; }
    }
}
