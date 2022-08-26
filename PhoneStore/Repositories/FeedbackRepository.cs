using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Models;
using PhoneStore.Repositories.Interfaces;

namespace PhoneStore.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly MobileContext _db;

        public FeedbackRepository(MobileContext db)
        {
            _db = db;
        }

        public IEnumerable<Feedback> GetAllByUserId(int userId)
            => _db.Feedbacks.Where(f => f.UserId == userId);

        public Feedback GetById(int id)
        {
            return _db.Feedbacks
                .Include(f => f.User)
                .Include(f => f.Phone)
                .First(f => f.Id == id);
        }

        public bool CheckFeedbackExists(int userId, int phoneId)
            => _db.Feedbacks.Any(f => f.UserId == userId && f.PhoneId == phoneId);

        public void Create(Feedback feedback)
        {
            _db.Feedbacks.Add(feedback);
            _db.SaveChanges();
        }

        public void Update(Feedback feedback)
        {
            _db.Update(feedback);
            _db.SaveChanges();
        }

        public IEnumerable<Order> GetByUserId(int id)
            => _db.Orders.Where(o => o.UserId == id);
    }
}