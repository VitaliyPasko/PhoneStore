using System;
using PhoneStore.ViewModels.Account;
using PhoneStore.ViewModels.PhoneViewModels;

namespace PhoneStore.ViewModels.Feedback
{
    public class FeedbackViewModel : BaseEntity
    {
        public int UserId { get; set; }
        public int PhoneId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDateTime { get; set; }
        public UserViewModel User { get; set; }
        public PhoneViewModel Phone { get; set; }
    }
}