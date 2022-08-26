using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Mappers;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    public class PersonalAreaController : Controller
    {
        private readonly MobileContext _db;

        public PersonalAreaController(MobileContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(int userId)
        {
            PersonalAreaViewModel model = new PersonalAreaViewModel
            {
                User = _db.Users
                    .FirstOrDefault(u => u.Id == userId)
                    .MapToUserViewModel(),
                Feedbacks = _db.Feedbacks
                    .Include(f => f.Phone)
                    .Include(f => f.User)
                    .Where(f => f.UserId == userId).Select(f => f.MapToFeedbackViewModel())
            };
            
            return View(model);
        }
    }
}