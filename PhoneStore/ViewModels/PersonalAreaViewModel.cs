using System.Collections.Generic;
using PhoneStore.ViewModels.Account;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.ViewModels
{
    public class PersonalAreaViewModel
    {
        public UserViewModel User { get; set; }
        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
    }
}