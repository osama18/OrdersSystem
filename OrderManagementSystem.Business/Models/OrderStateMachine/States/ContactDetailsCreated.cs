using OrderManagementSystem.Business.Extensions;
using OrderManagementSystem.Business.Models.OrderStateMachine.Requests;
using OrderManagementSystem.Business.Services;
using OrderManagementSystem.ORM.Models;
using OrderManagementSystem.ORM.Repositories;
using System;
using System.Linq;

namespace OrderManagementSystem.Business.Models.OrderStateMachine.States
{
    class ContactDetailsCreated : IState
    {
        private readonly IOrderRepository orderRepository;
        private readonly IPaymentDetailsRepository paymentDetailsRepository;
        private readonly IDeliveryDetailsRepository deliveryDetailsRepository;
        private readonly IOrderCache orderCahce;
        public ContactDetailsCreated(IOrderRepository orderRepository,
            IPaymentDetailsRepository paymentDetailsRepository,
            IDeliveryDetailsRepository deliveryDetailsRepository,
            IOrderCache orderCahce)
        {
            this.paymentDetailsRepository = paymentDetailsRepository;
            this.orderRepository = orderRepository;
            this.deliveryDetailsRepository = deliveryDetailsRepository;
            this.orderCahce = orderCahce;
        }

        public StateResponse CreateOrder(Order order)
        {
            return new StateResponse(false, $"Order with ID {order.Id} already created");
        }

        public StateResponse UpdateContactDetails(Order order, OrderContactDetailsRequest request)
        {
            return new StateResponse(false, $"Contact Details for order {order.Id} already processed");
        }

        public StateResponse MarkContactDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Contact Details for order {order.Id} already processed");
        }

        public StateResponse UpdatePaymentDetails(Order order, OrderPaymentDetailsRequest request)
        {
            var result = paymentDetailsRepository.CreateDocument(new PaymentDetails { Amount = request.Amount }).GetAwaiter().GetResult();
            //This part could be extracted to a service
            order.TotalAmount = request.Amount.ToString();

            order.SuccessStep(StepName.ProcessPayment);

            if (order.State == ORM.Entities.StateType.DeliveryDetailsUpdated)
            {
                order.State = ORM.Entities.StateType.Completed;
                order.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                order.State = ORM.Entities.StateType.PaymentDetailsCreated;
            }

            orderRepository.UpdateDocument(order, order.Id).GetAwaiter().GetResult();

            orderCahce.Set(order);

            return new StateResponse(true);
        }

        public StateResponse MarkPaymentDetailsFailed(Order order)
        {
            order.FailForStep(StepName.ProcessPayment);
            orderRepository.UpdateDocument(order, order.Id).GetAwaiter().GetResult();
            orderCahce.Set(order);
            return new StateResponse(true);
        }

        public StateResponse UpdateDeliveryDetails(Order order, OrderDeliveryDetailsRequest request)
        {
            var result = deliveryDetailsRepository.CreateDocument(new DeliveryAppointment { AppointmentDateTime = request.AppointmentDateTime }).GetAwaiter().GetResult();

            order.SuccessStep(StepName.ProcessDeliveryAppointment);

            if (order.State == ORM.Entities.StateType.PaymentDetailsCreated)
            {
                order.State = ORM.Entities.StateType.Completed;
                order.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                order.State = ORM.Entities.StateType.DeliveryDetailsUpdated;
            }

            orderRepository.UpdateDocument(order, order.Id).GetAwaiter().GetResult();

            orderCahce.Set(order);

            return new StateResponse(true);
        }

        public StateResponse MarkDeliveryDetailsFailed(Order order)
        {
            order.FailForStep(StepName.ProcessDeliveryAppointment);
            orderRepository.UpdateDocument(order, order.Id).GetAwaiter().GetResult();
            orderCahce.Set(order);
            return new StateResponse(true);
        }

      
    }
}
