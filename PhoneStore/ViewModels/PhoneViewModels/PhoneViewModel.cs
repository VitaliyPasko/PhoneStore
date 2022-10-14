using System.Collections.Generic;
using System.Text.Json.Serialization;
using PhoneStore.Models;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.ViewModels.PhoneViewModels
{
    public class PhoneViewModel : BaseEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("brandId")]
        public int BrandId { get; set; }
        [JsonPropertyName("brand")]
        public Brand Brand { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("feedbacks")]
        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
    }
}