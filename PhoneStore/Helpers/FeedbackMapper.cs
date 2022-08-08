using PhoneStore.Models;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Helpers
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
                CreationDateTime = self.CreationDateTime
            };
            return feedbackViewModel;
        }
    }
}