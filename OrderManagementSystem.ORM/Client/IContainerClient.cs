using OrderManagementSystem.ORM.Containers.Client.Model;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace OrderManagementSystem.ORM.Containers.Client
{
    public interface IContainerClient
    {
        Task<CreateContainerResponse> CreateContainer(CreateConatinerRequest request);
        Task<GetContainersResponse> GetConatiners(GetContainersRequest request);

        Task<GetContainerResponse> GetConatiner(GetContainerRequest request);
        Task<DeleteContainerResponse> DeleteConatiner(DeleteContainerRequest request);

    }
}