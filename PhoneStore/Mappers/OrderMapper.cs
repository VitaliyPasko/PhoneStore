using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Mappers
{
    public static class OrderMapper
    {
        public static OrderViewModel MapToOrderViewModel(this Order self)
        {
            return new OrderViewModel
            {
                Address = self.Address,
                Phone = self.Phone.MapToPhoneViewModel(),
                ContactPhone = self.ContactPhone,
                PhoneId = self.PhoneId,
                User = self.User.MapToUserViewModel(),
                UserId = self.UserId,
                Id = self.Id
            };
        }
        
        public static Order MapToOrder(this OrderViewModel self)
        {
            return new Order
            {
                Address = self.Address,
                Phone = self.Phone?.MapToPhone(),
                ContactPhone = self.ContactPhone,
                PhoneId = self.PhoneId,
                UserId = self.UserId,
                Id = self.Id
            };
        }
    }
}