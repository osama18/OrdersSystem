using OrderManagementSystem.Business.Extensions;
using OrderManagementSystem.Business.Models.OrderStateMachine.Requests;
using OrderManagementSystem.Business.Services;
using OrderManagementSystem.ORM.Models;
using OrderManagementSystem.ORM.Repositories;
using System.Linq;

namespace OrderManagementSystem.Business.Models.OrderStateMachine.States
{
    class OrderCreated : IState
    {
        private readonly IOrderRepository orderRepository;
        private readonly IContactDetailsRepository contactDetailsRepository;
        private readonly IOrderCache orderCahce;

        public OrderCreated(IOrderRepository orderRepository,
            IContactDetailsRepository contactDetailsRepository,
            IOrderCache orderCahce)
        {
            this.orderRepository = orderRepository;
            this.contactDetailsRepository = contactDetailsRepository;
            this.orderCahce = orderCahce;
        }

        public StateResponse CreateOrder(Order order)
        {
            return new StateResponse(false, $"Order with ID {order.Id} already created");
        }

        public StateResponse UpdateContactDetails(Order order, OrderContactDetailsRequest request)
        {
            var result = contactDetailsRepository.CreateDocument(new ContactDetails { Email = request.Email }).GetAwaiter().GetResult();
         
            order.State = ORM.Entities.StateType.ContactDetailsCreated;
            
            order.AppendStep(StepName.ProcessPayment);

            order.AppendStep(StepName.ProcessDeliveryAppointment);
            
            order.SuccessStep(StepName.ContactDetails);

            orderRepository.UpdateDocument(order, order.Id).GetAwaiter().GetResult();

            orderCahce.Set(order);

            return new StateResponse(true);
        }

        public StateResponse MarkContactDetailsFailed(Order order)
        {
            order.FailForStep(StepName.ContactDetails);

            orderRepository.UpdateDocument(order, order.Id).GetAwaiter().GetResult();

            orderCahce.Set(order);

            return new StateResponse(true);
        }

        public StateResponse UpdatePaymentDetails(Order order, OrderPaymentDetailsRequest request)
        {
            return new StateResponse(false, $"Can't {nameof(UpdatePaymentDetails)} before {nameof(UpdateContactDetails)}");
        }

        public StateResponse MarkPaymentDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Can't {nameof(MarkPaymentDetailsFailed)} before {nameof(UpdateContactDetails)}");
        }

        public StateResponse UpdateDeliveryDetails(Order order, OrderDeliveryDetailsRequest request)
        {
            return new StateResponse(false, $"Can't {nameof(UpdateDeliveryDetails)} before {nameof(UpdateContactDetails)}");
        }

        public StateResponse MarkDeliveryDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Can't {nameof(MarkDeliveryDetailsFailed)} before {nameof(UpdateContactDetails)}");
        }

    }
}
