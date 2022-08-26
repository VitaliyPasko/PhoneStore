using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Models;
using PhoneStore.Repositories.Interfaces;

namespace PhoneStore.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly MobileContext _db;

        public PhoneRepository(MobileContext db)
        {
            _db = db;
        }

        public void Create(Phone phone)
        {
            _db.Phones.Add(phone);
            _db.SaveChanges();
        }

        public void Update(Phone phone)
        {
            _db.Phones.Update(phone);
            _db.SaveChanges();
        }

        public Phone GetById(int id)
        {
            var a =_db.Phones
            .Include(p => p.Brand)
            .Include(p => p.Feedbacks)
            .ThenInclude(f => f.User)
            .FirstOrDefault(p => p.Id == id);
            return a;
        }
    }
}