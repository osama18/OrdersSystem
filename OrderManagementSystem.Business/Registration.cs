using OrderManagementSystem.ORM.Containers.Client;
using CoursesDB;
using Microsoft.Extensions.DependencyInjection;
using CoursesDB.Client;
using GenerIcRepository;
using OrderManagementSystem.ORM.Repositories;
using OrderManagementSystem.Business.Factories;
using OrderManagementSystem.Business.Mappers;
using OrderManagementSystem.Business.Services;
using System;

namespace OrderManagementSystem.Business
{
    public static class Registration
    {
        public static IServiceCollection RegisterBusiness(this IServiceCollection collection)
        {
            collection.AddScoped<IOrderMapper, OrderMapper>();
            collection.AddScoped<IIOrderStateFactory, OrderStateFactory>();
            collection.AddScoped<IStatesFactory, StatesFactory>();
            collection.AddScoped<IOrderServices, OrderServices>();
            collection.AddScoped<IOrderCache, OrderCache>();
            return collection;
        }
    }
}
