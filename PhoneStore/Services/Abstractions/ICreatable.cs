using System.Threading.Tasks;

namespace PhoneStore.Services.Abstractions
{
    public interface ICreatable<in T> where T : class
    {
        Task CreateAsync(T entity);
    }
}