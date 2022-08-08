using System;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Models;
using PhoneStore.ViewModels;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly MobileContext _db;
        private readonly UserManager<User> _userManager;

        public FeedbackController(MobileContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // GET
        public ActionResult<FeedbackViewModel> Create(FeedbackCreateViewModel model)
        {
            Feedback feedback = new Feedback
            {
                CreationDateTime = DateTime.Now,
                Text = model.Text,
                PhoneId = model.PhoneId,
                UserId = int.Parse(_userManager.GetUserId(User))
            };
            _db.Feedbacks.Add(feedback);
            _db.SaveChanges();
            Feedback newFeedback = _db.Feedbacks
                .Include(f => f.User)
                .Include(f => f.Phone)
                .First(f => f.Id == feedback.Id);
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel
            {
                Id = newFeedback.Id,
                Text = newFeedback.Text,
                PhoneId = newFeedback.PhoneId,
                UserId = newFeedback.UserId,
                CreationDateTime = newFeedback.CreationDateTime
            };
            return Json(feedbackViewModel);
        }
    }
}