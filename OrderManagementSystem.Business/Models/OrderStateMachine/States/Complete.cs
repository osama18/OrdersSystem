using OrderManagementSystem.Business.Models.OrderStateMachine.Requests;
using OrderManagementSystem.ORM.Models;
using OrderManagementSystem.ORM.Repositories;
using System;
using System.Threading.Tasks;

namespace OrderManagementSystem.Business.Models.OrderStateMachine.States
{
    public class Compeleted : IState
    {
        public StateResponse CreateOrder(Order order)
        {
            return new StateResponse(false, $"Order is finalized");
        }

        public StateResponse UpdateContactDetails(Order order, OrderContactDetailsRequest request)
        {
            return new StateResponse(false, $"Order is finalized");
        }

        public StateResponse MarkContactDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Order is finalized");
        }

        public StateResponse UpdatePaymentDetails(Order order, OrderPaymentDetailsRequest request)
        {
            return new StateResponse(false, $"Order is finalized");
        }

        public StateResponse MarkPaymentDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Order is finalized");
        }

        public StateResponse UpdateDeliveryDetails(Order order, OrderDeliveryDetailsRequest request)
        {
            return new StateResponse(false, $"Order is finalized");
        }

        public StateResponse MarkDeliveryDetailsFailed(Order order)
        {
            return new StateResponse(false, $"Order is finalized");
        }


    }
}
