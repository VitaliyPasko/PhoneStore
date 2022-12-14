using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PhoneStore.Mappers;
using PhoneStore.Models;
using PhoneStore.Repositories.Interfaces;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly MobileContext _db;

        public FeedbackService(
            UserManager<User> userManager, 
            IFeedbackRepository feedbackRepository, MobileContext db)
        {
            _userManager = userManager;
            _feedbackRepository = feedbackRepository;
            _db = db;
        }

        public FeedbackViewModel Create(FeedbackCreateViewModel model, ClaimsPrincipal user)
        {
            var userId = int.Parse(_userManager.GetUserId(user));
            var exist = _feedbackRepository.CheckFeedbackExists(userId, model.PhoneId);
            if (exist)
                return null;
            Feedback feedback = new Feedback
            {
                CreationDateTime = DateTime.Now,
                Text = model.Text,
                PhoneId = model.PhoneId,
                UserId = int.Parse(_userManager.GetUserId(user))
            };
            _feedbackRepository.Create(feedback);
            FeedbackViewModel feedbackViewModel = _feedbackRepository
                .GetById(feedback.Id)
                .MapToFeedbackViewModel();
            
            return feedbackViewModel;
        }

        public FeedbackViewModel Update(FeedbackEditViewModel model)
        {
            var feedback = _feedbackRepository.GetById(model.Id);
            if (feedback is null)
                return null;
            feedback.Text = model.Text;
            _feedbackRepository.Update(feedback);
            return feedback.MapToFeedbackViewModel();
        }

        public FeedbackViewModel GetById(int id)
        {
            return _feedbackRepository
                .GetById(id)
                .MapToFeedbackViewModel();
        }

        public PersonalAreaViewModel GetPersonalViewModel(int userId)
        {
            PersonalAreaViewModel model = new PersonalAreaViewModel
            {
                Feedbacks = _feedbackRepository
                    .GetByUserId(userId)
                    .Select(f => f.MapToFeedbackViewModel()),
                User = _db.Users.FirstOrDefault(u => u.Id == userId).MapToUserViewModel()
            };

            return model;
        }
    }
}