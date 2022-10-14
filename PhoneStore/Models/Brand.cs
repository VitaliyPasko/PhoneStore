using System.Text.Json.Serialization;

namespace PhoneStore.Models
{
    public class Brand
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}