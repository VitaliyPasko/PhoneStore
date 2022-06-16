using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Enums;
using PhoneStore.Models;
using PhoneStore.Services.Abstractions;

namespace PhoneStore.Services
{
    public class BasketService : IBasketService
    {
        private readonly MobileContext _context;

        public BasketService(MobileContext context)
        {
            _context = context;
        }

        public StatusCodes AddPhone(int id)
        {
            var exists = _context.Phones.Any(p => p.Id == id);
            if (!exists) return StatusCodes.EntityNotFound;
            
            _context.Baskets.Add(new Basket{PhoneId = id});
            _context.SaveChanges();
                
            return StatusCodes.Success;
        }

        public List<Basket> GetAll()
        {
            return _context.Baskets
                .Include(b => b.Phone)
                .ToList();
        }

        public void Remove(int id)
        {
            var basket = _context.Baskets.FirstOrDefault(b => b.PhoneId == id);
            if (basket is null)
            {
                return;
            }

            _context.Baskets.Remove(basket);
            _context.SaveChanges();
        }
    }
}