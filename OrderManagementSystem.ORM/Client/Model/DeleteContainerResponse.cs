using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementSystem.ORM.Containers.Client.Model
{
    public class DeleteContainerResponse : Response
    {
        public string ConatinerId { get; set; }
    }
}
