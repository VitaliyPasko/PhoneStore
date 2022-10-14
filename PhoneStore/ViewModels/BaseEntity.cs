using System.Text.Json.Serialization;

namespace PhoneStore.ViewModels
{
    public class BaseEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}