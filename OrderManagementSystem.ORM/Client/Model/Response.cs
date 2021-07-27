namespace OrderManagementSystem.ORM.Containers.Client.Model
{
    public class Response
    {
        public bool Success => Error == null;
        public Error Error { get; set; }
    }
}