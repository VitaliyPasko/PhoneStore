
using System.Collections.Generic;
using PhoneStore.Models;
using PhoneStore.Repositories.Interfaces.Base;

namespace PhoneStore.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseOperations<Order>, IByUserIdProvider<Order>
    {
        IEnumerable<Order> GetAll();
    }
}