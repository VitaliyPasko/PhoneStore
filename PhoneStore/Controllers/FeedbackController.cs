using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;
using PhoneStore.Validations;
using PhoneStore.ViewModels;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        private readonly MobileContext _db;
        private readonly FeedbackValidation _feedbackValidation;

        public FeedbackController(
            IFeedbackService feedbackService, 
            MobileContext db, 
            FeedbackValidation feedbackValidation)
        {
            _feedbackService = feedbackService;
            _db = db;
            _feedbackValidation = feedbackValidation;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedbackCreateViewModel model)
        {
            var validationResult = await _feedbackValidation.ValidateAsync(model);
            // if (!validationResult.IsValid)
            // {
            //     //TODO: сформировать сообщение 
            //     return NotFound();
            // }
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

        [HttpPost]
        public IActionResult Update([FromBody]FeedbackEditViewModel model)
        {
            var feedbackViewModel = _feedbackService.Update(model);
            if (feedbackViewModel is null)
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            return PartialView("PartialViews/FeedbackPartialView", feedbackViewModel);
        }
    }
}