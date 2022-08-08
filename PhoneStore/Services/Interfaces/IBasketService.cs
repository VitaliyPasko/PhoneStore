using System.Collections.Generic;
using PhoneStore.Enums;
using PhoneStore.Models;

namespace PhoneStore.Services.Interfaces
{
    public interface IBasketService
    {
        StatusCodes AddPhone(int id);
        List<Basket> GetAll();
        void Remove(int id);
    }
}