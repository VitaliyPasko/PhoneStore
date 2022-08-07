using System.Collections.Generic;
using PhoneStore.Models;

namespace PhoneStore.ViewModels
{
    public class PhoneViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Brand Brand { get; set; }
        public string Image { get; set; }
        public List<FeedbackViewModel> Feedbacks { get; set; }
    }
}