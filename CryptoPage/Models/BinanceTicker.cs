using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoPage.Models
{
    public class BinanceTicker
    {
        [JsonPropertyName("symbol")]
        [Key]
        public string Symbol { get; set; }

        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; }

        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; }
    }
}
