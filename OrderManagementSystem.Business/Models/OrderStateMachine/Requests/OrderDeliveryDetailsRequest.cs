using System;

namespace OrderManagementSystem.Business.Models.OrderStateMachine.Requests
{
    public class OrderDeliveryDetailsRequest 
    {
        public DateTime AppointmentDateTime { get; set; }
    }
}