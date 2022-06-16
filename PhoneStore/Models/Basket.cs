using PhoneStore.ViewModels;

namespace PhoneStore.Models
{
    public class Basket : BaseEntity
    {
        public int PhoneId { get; set; }
        public virtual Phone Phone { get; set; }
    }
}