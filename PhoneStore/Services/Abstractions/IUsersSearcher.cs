using System.Collections.Generic;
using PhoneStore.Models;

namespace PhoneStore.Services.Abstractions
{
    public interface IUsersSearcher
    {
        IEnumerable<User> SearchByName(string searchTerm);
        IEnumerable<User> SearchByLogin(string searchTerm);
        IEnumerable<User> SearchByEmail(string searchTerm);
    }
}