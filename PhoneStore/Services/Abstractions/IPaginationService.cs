using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Services.Abstractions
{
    public interface IPaginationService<T>
    {
        Task<(IQueryable<T>, int)> GetABatchOfData(IQueryable<T> elements, int page, int pageSize);
    }
}