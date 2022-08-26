using System.Linq;
using PhoneStore.Models;
using PhoneStore.ViewModels.PhoneViewModels;

namespace PhoneStore.Mappers
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
        public static PhoneViewModel MapToPhoneViewModel(this Phone self)
        {
            PhoneViewModel phone = new PhoneViewModel
            {
                Name = self.Name,
                Price = self.Price,
                Brand = self.Brand,
                Image = self.Image,
                Id = self.Id,
                Feedbacks = self.Feedbacks?.Select(f => f.MapToFeedbackViewModel()),
                BrandId = self.BrandId
            };

            return phone;
        }
        
        public static Phone MapToPhone(this PhoneViewModel self, string imagePath = null)
        {
            Phone phone = new Phone
            {
                Name = self.Name,
                Price = self.Price,
                BrandId = self.BrandId,
                Image = imagePath,
                Id = self.Id,
                Brand = self.Brand
            };

            return phone;
        }
    }
}