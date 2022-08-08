using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        private readonly MobileContext _db;

        public FeedbackController(IFeedbackService feedbackService, MobileContext db)
        {
            _feedbackService = feedbackService;
            _db = db;
        }

        [HttpPost]
        public IActionResult Create(FeedbackCreateViewModel model)
        {
            //TODO: валидация.
            var feedbackViewModel = _feedbackService.Create(model, User);
            return PartialView("PartialViews/FeedbackPartialView", feedbackViewModel);
        }
        
        [HttpGet]
        public ActionResult<FeedbackViewModel> GetCount()
        {
            var usersCount = _db.Users.Count();
            var feedbacksCount = _db.Feedbacks.Count();
            var model = new AccountingViewModel
            {
                User = new UserAccountingViewModel(nameof(User), usersCount),
                Feedback = new FeedbackAccountingViewModel(nameof(Feedback), feedbacksCount)
            };
            return Json(model);
        }
    }
}