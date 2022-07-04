using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Models;
using PhoneStore.Services.Abstractions;

namespace PhoneStore.Services
{
    public class PaginationService : IPaginationService
    {
        public async Task<(IQueryable<User>, int)> GetABatchOfData(IQueryable<User> users, int page, int pageSize)
        {
            int count = await users.CountAsync();
            return (users.Skip((page - 1) * pageSize).Take(pageSize), count);
        }
    }
}