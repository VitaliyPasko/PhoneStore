using System.Collections.Generic;
using System.Linq;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;

namespace PhoneStore.Services
{
    public class UsersSearcher : IUsersSearcher
    {
        private readonly MobileContext _db;

        public UsersSearcher(MobileContext db)
        {
            _db = db;
        }

        public IEnumerable<User> SearchByName(string searchTerm)
            => _db.Users
                .Where(u => u.Name
                .ToLower()
                .Contains(searchTerm.ToLower()));

        public IEnumerable<User> SearchByLogin(string searchTerm)
            => _db.Users
                .Where(u => u.UserName
                    .ToLower()
                    .Contains(searchTerm.ToLower()));

        public IEnumerable<User> SearchByEmail(string searchTerm)
            => _db.Users
                .Where(u => u.Email
                    .ToLower()
                    .Contains(searchTerm.ToLower()));
    }
}