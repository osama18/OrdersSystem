using OrderManagementSystem.Business.Models.OrderStateMachine;
using OrderManagementSystem.ORM.Models;

namespace OrderManagementSystem.Business.Services
{
    public interface IOrderCache
    {
        Order TryGet(string orderId);
        void Set(Order Order);
    }
}