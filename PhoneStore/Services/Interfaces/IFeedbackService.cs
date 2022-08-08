using System.Security.Claims;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Services.Interfaces
{
    public interface IFeedbackService
    {
        FeedbackViewModel Create(FeedbackCreateViewModel model, ClaimsPrincipal user);
    }
}