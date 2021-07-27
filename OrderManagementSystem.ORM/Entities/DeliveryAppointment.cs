using OrderManagementSystem.ORM.Entities;

namespace OrderManagementSystem.ORM.Models
{
    public class DeliveryAppointment : EtagEntity
    {
        public System.DateTime AppointmentDateTime { get; set; }
    }
}