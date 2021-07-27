namespace OrderManagementSystem.ORM.Entities
{
    public enum StateType
    {
        NewOrder = 1,
        OrderCreated =2,
        ContactDetailsCreated=3,
        PaymentDetailsCreated=4,
        DeliveryDetailsUpdated = 5,
        Failed = 6,
        Completed = 7
    }
}
