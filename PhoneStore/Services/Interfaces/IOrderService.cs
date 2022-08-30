using System.Collections.Generic;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderViewModel> GetAll();
        void Create(Order order);
        IEnumerable<OrderViewModel> GetByUserId(int id);
    }
}