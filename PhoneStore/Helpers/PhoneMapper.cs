using PhoneStore.Models;
using PhoneStore.ViewModels;
using PhoneStore.ViewModels.PhoneViewModels;

namespace PhoneStore.Helpers
{
    public static class PhoneMapper
    {
        public static Phone MapToPhone(this PhoneCreateViewModel self, string imagePath = null)
        {
            Phone phone = new Phone
            {
                Name = self.Name,
                Price = (decimal) self.Price!,
                BrandId = self.BrandId,
                Image = imagePath,
                Id = self.Id
            };

            return phone;
        }
        public static PhoneViewModel MapToPhoneViewModel(this Phone self, string imagePath = null)
        {
            PhoneViewModel phone = new PhoneViewModel
            {
                Name = self.Name,
                Price = self.Price,
                Brand = self.Brand,
                Image = imagePath,
                Id = self.Id
            };

            return phone;
        }
        
    }
}