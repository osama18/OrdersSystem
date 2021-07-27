
using OrderManagementSystem.Business.Factories;
using OrderManagementSystem.Business.Models.OrderStateMachine;
using OrderManagementSystem.Common.Loggers;
using OrderManagementSystem.ORM.Models;
using OrderManagementSystem.ORM.Repositories;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementSystem.Business.Services
{
    public class OrderServices : IOrderServices
    {

        private readonly ILogger logger;
        private readonly IOrderCache orderCache;
        private readonly IOrderRepository orderRepository;
        private readonly IIOrderStateFactory orderStateMachineFactory;
        public OrderServices(IOrderCache orderCache, ILogger logger, IOrderRepository orderRepository,
            IIOrderStateFactory orderStateMachineFactory)
        {
            this.orderCache = orderCache;
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.orderStateMachineFactory = orderStateMachineFactory;
        }

        public async Task<IOrderStateMachine> GetOrderStateMachine(string orderId = null)
        {
            if (orderId == null)
                return orderStateMachineFactory.Create();

            var cachedOrder = orderCache.TryGet(orderId);
            if (cachedOrder == null)
            {
                cachedOrder = await orderRepository.Retrieve(orderId);
                orderCache.Set(cachedOrder);
            }
            
            return orderStateMachineFactory.Create(cachedOrder);
        }
    }
}