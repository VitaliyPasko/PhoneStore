using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Helpers;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly MobileContext _db;
        private readonly UserManager<User> _userManager;

        public FeedbackService(
            MobileContext db, 
            UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public FeedbackViewModel Create(FeedbackCreateViewModel model, ClaimsPrincipal user)
        {
            Feedback feedback = new Feedback
            {
                CreationDateTime = DateTime.Now,
                Text = model.Text,
                PhoneId = model.PhoneId,
                UserId = int.Parse(_userManager.GetUserId(user))
            };
            _db.Feedbacks.Add(feedback);
            _db.SaveChanges();
            Feedback newFeedback = _db.Feedbacks
                .Include(f => f.User)
                .Include(f => f.Phone)
                .First(f => f.Id == feedback.Id);
            FeedbackViewModel feedbackViewModel = newFeedback.MapToFeedbackViewModel();
            
            return feedbackViewModel;
        }
    }
}