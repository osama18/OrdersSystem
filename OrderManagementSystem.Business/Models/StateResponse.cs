

namespace OrderManagementSystem.Business.Models
{
    public class StateResponse
    {
        public StateResponse(bool success)
        {
            Success = success;
        }
        public StateResponse(bool success, string message, string createdResourceId = null)
        {
            Success = success;
            Message = message;
            CreatedResourceId = createdResourceId;
        }

        public string CreatedResourceId { get; private set; }
        public string Message { get; private set; }
        public bool Success { get; private set; }
    }
}