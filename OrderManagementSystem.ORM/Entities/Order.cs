
using OrderManagementSystem.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagementSystem.ORM.Models
{
    public class Order : EtagEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string TotalAmount { get; set; }
        public ICollection<Step> Steps { get; set; }
        public StateType State { get; set; }
        public string ErrorMessage => $"Faile at {Steps?.LastOrDefault(s => s.Status == StepStatus.Fail)?.Name}";
        public OrderStatus Status => CalculateStatus();

        private OrderStatus CalculateStatus()
        {
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
                return OrderStatus.Fail;

            if (CompletedAt != null)
                return OrderStatus.Success;

            if(Steps?.Any(s => s.Status == StepStatus.Pending) ?? false)
                return OrderStatus.Pending;

            return OrderStatus.Started;
        }
    }
}
