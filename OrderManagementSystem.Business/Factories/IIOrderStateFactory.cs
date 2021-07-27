using OrderManagementSystem.Business.Models.OrderStateMachine;
using OrderManagementSystem.ORM.Models;

namespace OrderManagementSystem.Business.Factories
{
    public interface IIOrderStateFactory
    {
        IOrderStateMachine Create(Order order = null);
    }
}