using System.Security.Claims;
using PhoneStore.Models;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Services.Interfaces
{
    public interface IFeedbackService
    {
        FeedbackViewModel Create(FeedbackCreateViewModel model, ClaimsPrincipal user);
        FeedbackViewModel Update(FeedbackEditViewModel model);
        FeedbackViewModel GetById(int id);
    }
}