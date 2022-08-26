using System.Collections.Generic;
using PhoneStore.Models;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.ViewModels.PhoneViewModels
{
    public class PhoneViewModel : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public string Image { get; set; }
        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
    }
}