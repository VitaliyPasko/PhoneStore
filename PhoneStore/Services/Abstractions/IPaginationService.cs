using System.Linq;
using System.Threading.Tasks;
using PhoneStore.Models;

namespace PhoneStore.Services.Abstractions
{
    public interface IPaginationService
    {
        Task<(IQueryable<User>, int)> GetABatchOfData(IQueryable<User> users, int page, int pageSize);
    }
}