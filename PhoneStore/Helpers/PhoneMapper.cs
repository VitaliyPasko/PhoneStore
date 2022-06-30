using PhoneStore.Models;
using PhoneStore.ViewModels;

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
    }
}