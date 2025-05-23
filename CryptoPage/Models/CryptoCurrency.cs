using System.Text.Json.Serialization;

namespace CryptoPage.Models
{
    public class CryptoCurrency
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public string? BinanceId {get; set; }//idk

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}