
using OrderManagementSystem.ORM.Containers.Client;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Threading.Tasks;
using OrderManagementSystem.Common.Loggers;
using OrderManagementSystem.ORM.Models;

namespace OrderManagementSystem.ORM.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T: EtagEntity
    {
        private readonly ILogger logger;
        private readonly IContainerClient containerClient;
        private readonly string dbName;
        private readonly string containerId;

        public GenericRepository(IContainerClient containerClient, ILogger logger, string dbName, string containerId)
        {
            this.containerClient = containerClient;
            this.logger = logger;
            this.dbName = dbName;
            this.containerId = containerId;
        }


        public async Task<string> CreateDocument(T document)
        {
            document.Id = Guid.NewGuid().ToString();

            await (await GetContainerAsync()).CreateItemAsync<T>(document);

            return document.Id;
        }

        public async Task<T> Retrieve(string id)
        {
            return await Retrieve(id,id);
        }

        public async Task<T> Retrieve(string id, string partitionKey)
        {
            
            var container = await GetContainerAsync();

            var result = await container.ReadItemAsync<T>(
             partitionKey: new PartitionKey(partitionKey),
             id: id);

            return result;
        }

        
        public async Task<T> UpdateDocument(T document, string id)
        {
            await (await GetContainerAsync()).ReplaceItemAsync<T>(document,id);

            return document;
        }

        public async Task<T> UpdateDocumentOptimisticConcurrency(T document,string partitionKey)
        {
            var container = await GetContainerAsync();

            try
            {
                await container.ReplaceItemAsync<T>(document, document.Id, new PartitionKey(partitionKey), new ItemRequestOptions { IfMatchEtag = document.ETag});
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                    logger.LogException(LogEvent.ConcurrencyIssue, ex );
                }
                throw ex;
            }
            
            return document;
        }

        public async Task DeleteDocument(string id, string partiotionKey)
        {
            await (await GetContainerAsync()).DeleteItemAsync<T>(id, new PartitionKey(partiotionKey));
        }

        protected async Task<Container> GetContainerAsync()
        {
            var response = await containerClient.GetConatiner(new GetContainerRequest
            {
                ContainerId = containerId,
                DbName = dbName
            });

            if (!response.Success)
                throw new ArgumentException($"No container found for {containerId} in Db : {dbName}");

            return response.Container;
        }
    }
}
