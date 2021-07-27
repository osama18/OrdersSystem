using OrderManagementSystem.Business.Models.OrderStateMachine;
using OrderManagementSystem.Business.Models.OrderStateMachine.States;
using OrderManagementSystem.ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementSystem.Business.Factories
{
    internal class OrderStateFactory : IIOrderStateFactory
    {
        private readonly IStatesFactory stateFactory;
        public OrderStateFactory(IStatesFactory stateFactory)
        {
            this.stateFactory = stateFactory;
        }
        public IOrderStateMachine Create(Order order = null)
        {
            return new OrderStateMachine(stateFactory, order);
        }
    }
}
