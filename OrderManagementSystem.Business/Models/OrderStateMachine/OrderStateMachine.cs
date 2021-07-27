using OrderManagementSystem.Business.Factories;
using OrderManagementSystem.Business.Models.OrderStateMachine.Requests;
using OrderManagementSystem.Business.Models.OrderStateMachine.States;
using OrderManagementSystem.ORM.Models;
using System.Threading.Tasks;

namespace OrderManagementSystem.Business.Models.OrderStateMachine
{
    public class OrderStateMachine : IOrderStateMachine
    {
        private Order order ;
        private readonly IStatesFactory stateFactory;
        public OrderStateMachine(IStatesFactory stateFactory, Order order)
        {
            this.stateFactory = stateFactory;
            this.order = order ?? OrderFactory.Create();
        }
        private IState state => stateFactory.Create(order);
       
        public StateResponse CreateOrder()
        {
            var order = OrderFactory.Create();
            return state.CreateOrder(order);
        }

        public StateResponse UpdateContactDetails(OrderContactDetailsRequest request)
        {
            return state.UpdateContactDetails(order, request);
        }

        public StateResponse MarkContactDetailsFailed()
        {
            return state.MarkContactDetailsFailed(order);
        }

        public StateResponse UpdatePaymentDetails(OrderPaymentDetailsRequest request)
        {
            return state.UpdatePaymentDetails(order, request);
        }

        public StateResponse MarkPaymentDetailsFailed()
        {
            return state.MarkPaymentDetailsFailed(order);
        }

        public StateResponse UpdateDeliveryDetails(OrderDeliveryDetailsRequest request)
        {
            return state.UpdateDeliveryDetails(order, request);
        }

        public StateResponse MarkDeliveryDetailsFailed()
        {
            return state.MarkDeliveryDetailsFailed(order);
        }

    }
}
