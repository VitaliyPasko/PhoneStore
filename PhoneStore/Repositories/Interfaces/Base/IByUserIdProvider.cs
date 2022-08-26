using System.Collections.Generic;

namespace PhoneStore.Repositories.Interfaces.Base
{
    public interface IByUserIdProvider<out T>
    {
        IEnumerable<T> GetByUserId(int id);
    }
}