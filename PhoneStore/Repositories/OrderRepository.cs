using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Models;
using PhoneStore.Repositories.Interfaces;
using PhoneStore.Repositories.Interfaces.Base;

namespace PhoneStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MobileContext _db;

        public OrderRepository(MobileContext db)
        {
            _db = db;
        }

        public void Create(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public void Update(Order order)
        {
            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public Order GetById(int id) 
            => _db.Orders
                .Include(o => o.Phone)
                .ThenInclude(p => p.Brand)
                .Include(p => p.Phone.Feedbacks)
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == id);

        public IEnumerable<Order> GetAll()
        {
            return _db.Orders
                .Include(p => p.Phone)
                .ThenInclude(p => p.Brand)
                .Include(p => p.Phone.Feedbacks)
                .Include(p => p.User)
                .ToList();
        }


        public IEnumerable<Order> GetByUserId(int id)
            => _db.Orders
                .Include(o => o.User)
                .Include(o => o.Phone)
                .Where(o => o.UserId == id);
    }
}