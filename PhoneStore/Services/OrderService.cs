using System.Collections.Generic;
using System.Linq;
using PhoneStore.Mappers;
using PhoneStore.Models;
using PhoneStore.Repositories.Interfaces;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<OrderViewModel> GetAll()
            => _orderRepository.GetAll().Select(o => o.MapToOrderViewModel());
        
        public void Create(Order order)
        {
            _orderRepository.Create(order);
        }
        
        public OrderViewModel GetById(int id)
            => _orderRepository.GetById(id).MapToOrderViewModel();

        public IEnumerable<OrderViewModel> GetByUserId(int id)
            => _orderRepository
                .GetByUserId(id)
                .Select(o => o.MapToOrderViewModel());
    }
}