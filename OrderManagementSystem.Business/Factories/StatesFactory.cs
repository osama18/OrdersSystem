using OrderManagementSystem.Business.Models.OrderStateMachine;
using OrderManagementSystem.Business.Models.OrderStateMachine.States;
using OrderManagementSystem.Business.Services;
using OrderManagementSystem.ORM.Entities;
using OrderManagementSystem.ORM.Models;
using OrderManagementSystem.ORM.Repositories;

namespace OrderManagementSystem.Business.Factories
{
    public class StatesFactory : IStatesFactory
    {
        private readonly IOrderRepository orderRepo;
        private readonly IPaymentDetailsRepository paymentRepo;
        private readonly IContactDetailsRepository contactRepo;
        private readonly IDeliveryDetailsRepository deliveryRepo;
        private readonly IOrderCache orderCahce;

        public StatesFactory(IOrderRepository orderRepo,
            IPaymentDetailsRepository paymentRepo,
            IContactDetailsRepository contactRepo,
            IDeliveryDetailsRepository deliveryRepo,
            IOrderCache orderCahce)
        {
            this.orderRepo = orderRepo;
            this.paymentRepo = paymentRepo;
            this.deliveryRepo = deliveryRepo;
            this.contactRepo = contactRepo;
            this.orderCahce = orderCahce;
        }

       public IState Create(Order order = null)
       {
            if (order == null)
                return new NewOrder(orderRepo, orderCahce);
            switch (order.State)
            {
                case StateType.NewOrder:
                    return new NewOrder(orderRepo, orderCahce);
                case StateType.Completed:
                case StateType.Failed:
                    return new Compeleted();
                case StateType.OrderCreated:
                    return new OrderCreated(orderRepo,contactRepo, orderCahce);
                case StateType.ContactDetailsCreated:
                case StateType.DeliveryDetailsUpdated:
                case StateType.PaymentDetailsCreated:
                    return new ContactDetailsCreated(orderRepo,paymentRepo,deliveryRepo, orderCahce);
            }
            return null;
       }
    }
}