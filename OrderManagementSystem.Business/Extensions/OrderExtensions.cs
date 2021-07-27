using OrderManagementSystem.ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementSystem.Business.Extensions
{
    public static class OrderExtensions
    {
        public static Order FailForStep(this Order order, StepName stepName)
        {
            order.State = ORM.Entities.StateType.Failed;
            foreach (var step in order.Steps)
            {
                if (step.Name == stepName)
                {
                    step.Status = StepStatus.Fail;
                    break;
                }
            }
            return order;
        }

        public static Order SuccessStep(this Order order, StepName stepName)
        {
            foreach (var step in order.Steps)
            {
                if (step.Name == stepName)
                {
                    step.Status = StepStatus.Success;
                    break;
                }
            }
            return order;
        }

        public static Order AppendStep(this Order order, StepName stepName)
        {
            if (order.Steps == null)
                order.Steps = new List<Step>();
            order.Steps.Add(new Step { Id = $"step{order.Steps.Count + 1}", Name = stepName, Status = StepStatus.Pending });
            return order;
        }
    }
}
