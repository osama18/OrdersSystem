using OrderManagementSystem.ORM.Entities;

namespace OrderManagementSystem.ORM.Models
{
    public class PaymentDetails : EtagEntity
    {
        public decimal Amount { get; set; }
    }
}