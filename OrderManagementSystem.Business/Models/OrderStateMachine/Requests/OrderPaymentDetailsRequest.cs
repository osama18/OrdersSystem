namespace OrderManagementSystem.Business.Models.OrderStateMachine.Requests
{
    public class OrderPaymentDetailsRequest 
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EUR";
    }
}