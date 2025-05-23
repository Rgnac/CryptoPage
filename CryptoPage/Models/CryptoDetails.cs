using System.Text.Json.Serialization;

namespace CryptoPage.Models
{
    public class CryptoDetails
    {
        [JsonPropertyName("usd")]
        public double Usd { get; set; }
        
        public string Name { get; set; }
    }
}
