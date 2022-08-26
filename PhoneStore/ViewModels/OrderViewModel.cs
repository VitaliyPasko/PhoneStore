using PhoneStore.ViewModels.Account;
using PhoneStore.ViewModels.PhoneViewModels;

namespace PhoneStore.ViewModels
{
    public class OrderViewModel : BaseEntity
    {
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public int PhoneId { get; set; }
        public PhoneViewModel Phone { get; set; }
    }
}