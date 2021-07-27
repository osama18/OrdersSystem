using OrderManagementSystem.ORM.Entities;

namespace OrderManagementSystem.ORM.Models
{
    public class Step : EtagEntity
    {
        public StepName Name { get; set; }
        public StepStatus Status { get; set; }
    }
}