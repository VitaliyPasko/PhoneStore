using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PhoneStore.Models;

namespace PhoneStore.ViewModels.PhoneViewModels
{
    public class PhoneEditViewModel : BaseEntity, IPhoneEditable
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int BrandId { get; set; }
        public List<Brand> Brands { get; set; }
        public IFormFile File { get; set; }
        public string Image { get; set; }
    }
}