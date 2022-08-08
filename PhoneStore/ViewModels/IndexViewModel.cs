using System.Collections.Generic;
using PhoneStore.Models;

namespace PhoneStore.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
    }
}