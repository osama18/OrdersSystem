

using OrderManagementSystem.Common.Loggers;
using OrderManagementSystem.Common.Settings;
using OrderManagementSystem.ORM.Containers.Client;
using OrderManagementSystem.ORM.Entities;
using OrderManagementSystem.ORM.Models;

using System;
using System.Threading.Tasks;

namespace OrderManagementSystem.ORM.Repositories
{
    internal class ContactDetailsRepository : GenericRepository<ContactDetails>, IContactDetailsRepository
    {
        public ContactDetailsRepository(ILogger logger, IContainerClient containerClient) :base(containerClient,logger, Constants.OrdersDbName, Constants.OrdersConatiner)
        {
           
        }

    }
}
