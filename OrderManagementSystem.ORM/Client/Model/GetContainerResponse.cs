namespace OrderManagementSystem.ORM.Containers.Client.Model
{
    public class GetContainerResponse : Response
    {
        public Microsoft.Azure.Cosmos.Container Container { get; set; }
    }
}