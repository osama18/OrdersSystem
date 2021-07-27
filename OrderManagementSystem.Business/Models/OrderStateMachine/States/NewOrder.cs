using OrderManagementSystem.Business.Models.OrderStateMachine.Requests;
using OrderManagementSystem.Business.Services;
using OrderManagementSystem.ORM.Models;
using OrderManagementSystem.ORM.Repositories;
using System;
using System.Threading.Tasks;

namespace OrderManagementSystem.Business.Models.OrderStateMachine.States
{
    class NewOrder : IState
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderCache orderCahce;
        public NewOrder(IOrderRepository orderRepository, IOrderCache orderCahce)
        {
            this.orderRepository = orderRepository;
            this.orderCahce = orderCahce;
        }


        public StateResponse CreateOrder(Order order)
        {
            order.State = ORM.Entities.StateType.OrderCreated;
            var id = orderRepository.CreateDocument(order)
                .GetAwaiter()
                .GetResult();

            orderCahce.Set(order);
            return new StateResponse(true,"", id);
        }

        public StateResponse UpdateContactDetails(Order order, OrderContactDetailsRequest request)
        {
            return new StateResponse(false, $"Can't {nameof(UpdateContactDetails)} before {nameof(CreateOrder)}");
        }

        public StateResponse MarkContactDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Can't {nameof(MarkContactDetailsFailed)} before {nameof(CreateOrder)}");
        }

        public StateResponse UpdatePaymentDetails(Order order, OrderPaymentDetailsRequest request)
        {
            return new StateResponse(false, $"Can't {nameof(UpdatePaymentDetails)} before {nameof(CreateOrder)}");
        }

        public StateResponse MarkPaymentDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Can't {nameof(MarkPaymentDetailsFailed)} before {nameof(CreateOrder)}");
        }

        public StateResponse UpdateDeliveryDetails(Order order, OrderDeliveryDetailsRequest request)
        {
            return new StateResponse(false, $"Can't {nameof(UpdateDeliveryDetails)} before {nameof(CreateOrder)}");
        }

        public StateResponse MarkDeliveryDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Can't {nameof(MarkDeliveryDetailsFailed)} before {nameof(CreateOrder)}");
        }


    }
}
