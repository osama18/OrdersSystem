using OrderManagementSystem.Business.Models.OrderStateMachine;
using OrderManagementSystem.ORM.Models;

namespace OrderManagementSystem.Business.Factories
{
    public interface IStatesFactory
    {
        IState Create(Order order = null);
    }
}