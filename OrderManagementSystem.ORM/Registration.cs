using OrderManagementSystem.ORM.Containers.Client;
using CoursesDB;
using Microsoft.Extensions.DependencyInjection;
using CoursesDB.Client;
using GenerIcRepository;
using OrderManagementSystem.ORM.Repositories;

namespace OrderManagementSystem.ORM.Containers
{
    public static class Registration
    {
        public static IServiceCollection RegisterORM(this IServiceCollection collection)
        {
            collection.AddSingleton<IDBClient, DbClient>();
            collection.AddScoped<IContainerClient, ContainerClient>();
            collection.AddScoped<IOrderRepository,OrderRepository>();
            collection.AddScoped<IPaymentDetailsRepository,PaymentDetailsRepository>();
            collection.AddScoped<IDeliveryDetailsRepository, DeliveryDetailsRepository>();
            collection.AddScoped<IContactDetailsRepository,ContactDetailsRepository>();
            return collection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
