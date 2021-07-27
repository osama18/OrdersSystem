using OrderManagementSystem.Business.Models.OrderStateMachine.Requests;
using OrderManagementSystem.ORM.Models;
using System.Threading.Tasks;

namespace OrderManagementSystem.Business.Models.OrderStateMachine
{
    public interface IState
    {
        public StateResponse CreateOrder(Order order);
        public StateResponse UpdateContactDetails(Order order, OrderContactDetailsRequest request);
        public StateResponse MarkContactDetailsFailed(Order order);

        public StateResponse UpdatePaymentDetails(Order order, OrderPaymentDetailsRequest request);
        public StateResponse MarkPaymentDetailsFailed(Order order);

        public StateResponse UpdateDeliveryDetails(Order order, OrderDeliveryDetailsRequest request);
        public StateResponse MarkDeliveryDetailsFailed(Order order);
    }
}