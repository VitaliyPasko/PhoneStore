using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(
            UserManager<User> userManager, 
            IFeedbackService feedbackService)
        {
            _userManager = userManager;
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public ActionResult<FeedbackViewModel> Create(FeedbackCreateViewModel model)
        {
            //TODO: валидация.
            var userId = int.Parse(_userManager.GetUserId(User));
            var feedbackViewModel = _feedbackService.Create(model, userId);
            
            return Json(feedbackViewModel);
        }
    }
}