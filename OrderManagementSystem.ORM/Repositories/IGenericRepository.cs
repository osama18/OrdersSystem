using OrderManagementSystem.ORM.Models;
using System.Threading.Tasks;

namespace OrderManagementSystem.ORM.Repositories
{
    public interface IGenericRepository<T> where T : EtagEntity
    {
        Task<string> CreateDocument(T document);
        Task<T> Retrieve(string id);
        Task<T> Retrieve(string id, string partitionKey);
        Task<T> UpdateDocument(T document, string id);
        Task<T> UpdateDocumentOptimisticConcurrency(T document,string partitionKey);
        Task DeleteDocument(string id, string partiotionKey);


    }
}