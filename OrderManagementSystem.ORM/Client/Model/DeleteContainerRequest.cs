using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementSystem.ORM.Containers.Client.Model
{
    public class DeleteContainerRequest
    {
        public string DbName { get; set; }
        public string ConatinerId { get; set; }
    }
}
