using OrderManagementSystem.Business.Extensions;
using OrderManagementSystem.ORM.Entities;
using OrderManagementSystem.ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementSystem.Business.Factories
{
    public static class OrderFactory
    {
        public static Order Create()
        {

            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                State = StateType.NewOrder
            };

            order.AppendStep(StepName.ContactDetails);

            return order;
        }
    }
}
