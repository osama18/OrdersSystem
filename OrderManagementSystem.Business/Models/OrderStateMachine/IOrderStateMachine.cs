using OrderManagementSystem.Business.Models.OrderStateMachine.Requests;
using OrderManagementSystem.ORM.Models;
using System.Threading.Tasks;

namespace OrderManagementSystem.Business.Models.OrderStateMachine
{
    public interface IOrderStateMachine 
    {
        public StateResponse CreateOrder();
        public StateResponse UpdateContactDetails(OrderContactDetailsRequest request);
        public StateResponse MarkContactDetailsFailed();

        public StateResponse UpdatePaymentDetails(OrderPaymentDetailsRequest request);
        public StateResponse MarkPaymentDetailsFailed();

        public StateResponse UpdateDeliveryDetails(OrderDeliveryDetailsRequest request);
        public StateResponse MarkDeliveryDetailsFailed();
    }
}