using Microsoft.AspNetCore.Mvc;
using PhoneStore.Services.Interfaces;

namespace PhoneStore.Controllers
{
    public class PersonalAreaController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public PersonalAreaController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public IActionResult Index(int userId)
        {
            var model = _feedbackService.GetPersonalViewModel(userId);
            return View(model);
        }
    }
}