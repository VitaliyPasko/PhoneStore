using PhoneStore.Models;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Mappers
{
    public static class FeedbackMapper
    {
        public static FeedbackViewModel MapToFeedbackViewModel(this Feedback self)
        {
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel
            {
                Id = self.Id,
                Text = self.Text,
                PhoneId = self.PhoneId,
                UserId = self.UserId,
                User = self.User?.MapToUserViewModel(),
                Phone = self.Phone?.MapToPhoneViewModel(),
                CreationDateTime = self.CreationDateTime
            };
            return feedbackViewModel;
        }
        
        public static Feedback MapToFeedback(this FeedbackViewModel self)
        {
            Feedback feedbackViewModel = new Feedback
            {
                Id = self.Id,
                Text = self.Text,
                PhoneId = self.PhoneId,
                UserId = self.UserId,
                User = self.User.MapToUser(),
                Phone = self.Phone.MapToPhone(),
                CreationDateTime = self.CreationDateTime
            };
            return feedbackViewModel;
        }
    }
}