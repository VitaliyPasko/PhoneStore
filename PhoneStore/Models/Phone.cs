using System.Collections.Generic;

namespace PhoneStore.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public string Image { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
    }
}