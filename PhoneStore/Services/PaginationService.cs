using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Services.Abstractions;

namespace PhoneStore.Services
{
    public class PaginationService<T> : IPaginationService<T>
    {
        public async Task<(IQueryable<T>, int)> GetABatchOfData(IQueryable<T> elements, int page, int pageSize)
        {
            int count = await elements.CountAsync();
            return (elements.Skip((page - 1) * pageSize).Take(pageSize), count);
        }

        
    }
}