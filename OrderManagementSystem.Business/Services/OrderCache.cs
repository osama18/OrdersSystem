
using OrderManagementSystem.Business.Models.OrderStateMachine;
using OrderManagementSystem.ORM.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OrderManagementSystem.Business.Services
{
    public class OrderCache : IOrderCache
    {
        //For demo purpose I am using a local dictionary to test the flow, however that definitly couldn't go live, It wouldn't work with distrbuted system 
        //I would use reliable distrbuted cahce like Redis for that purpose
        private IDictionary<string, Order> dictionary = new ConcurrentDictionary<string, Order>();
        public Order TryGet(string orderId)
        {

            Order result = null;

            if (dictionary.TryGetValue(orderId, out result))
                return result;

            return null;
        }

        public void Set(Order order)
        {
            dictionary[order.Id] = order;
        }
    }
}