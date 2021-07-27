using OrderManagementSystem.Business.Models.OrderStateMachine;
using System.Threading.Tasks;

namespace OrderManagementSystem.Business.Services
{
    public interface IOrderServices
    {
        Task<IOrderStateMachine> GetOrderStateMachine(string orderId = null);
    }
}