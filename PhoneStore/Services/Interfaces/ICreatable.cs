using System.Threading.Tasks;

namespace PhoneStore.Services.Interfaces
{
    public interface ICreatable<in T> where T : class
    {
        Task CreateAsync(T entity);
    }
}