using CoursesDB.Client.Model;
using Microsoft.Azure.Cosmos;
using OrderManagementSystem.Common.Loggers;
using OrderManagementSystem.Common.Settings;
using System;
using System.Threading.Tasks;

namespace CoursesDB.Client
{
    internal class DbClient : IDBClient
    {
        private readonly ISettingProvider settingProvider;
        private readonly ILogger logger;
        private readonly CosmosClient cosmosClient;

        public DbClient(ISettingProvider settingProvider,ILogger logger)
        {
            this.settingProvider = settingProvider;
            this.logger = logger;
            var connection = settingProvider.GetSetting<string>(Constants.ConnectionString);
            var primaryKey = settingProvider.GetSetting<string>(Constants.PrimaryKey);
            cosmosClient = new CosmosClient(connection,primaryKey);
        }

        public async Task<CreateDbResponse> CreateDb(CreateDbRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");
               
                var result = await cosmosClient.CreateDatabaseAsync(request.Name);
                var database = result.Resource;

                return new CreateDbResponse { 
                    DataBase = database
                };

            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.DataBaseCreationFailed, ex);
                return new CreateDbResponse { 
                    Error = new Error { 
                        Message = "Data Base Creation Failed"
                    }
                };
            }
        }

        public async Task<DeleteDbResponse> DeleteDb(DeleteDbRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");

                var result = await cosmosClient.GetDatabase(request.Name).DeleteAsync();
                var database = result.Resource;

                return new DeleteDbResponse();

            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.DataBasDeleteFailed, ex);
                return new DeleteDbResponse
                {
                    Error = new Error
                    {
                        Message = "Data Base delete Failed"
                    }
                };
            }
        }

        public async Task<GetDataBaseResponse> GetDataBase(GetDataBaseRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");

                var dataBase = cosmosClient.GetDatabase(request.Name);

                if (dataBase == null)
                    throw new InvalidOperationException($"No Db found with name {request.Name}");

                return new GetDataBaseResponse
                {
                    Database = dataBase
                };
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.RetriveDataBasesFailed, ex);
                return new GetDataBaseResponse
                {
                    Error = new Error
                    {
                        Message = "Data Base retrieve Failed"
                    }
                };
            }
        }

        public async Task<GetDataBasesResponse> GetDataBases()
        {
            try
            {
                var iterators = cosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();
                var dataBases = await iterators.ReadNextAsync();
                return new GetDataBasesResponse
                {
                    DataBases = dataBases
                };
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.RetriveDataBasesFailed, ex);
                return new GetDataBasesResponse
                {
                    Error = new Error
                    {
                        Message = "Data Base retrieve Failed"
                    }
                };
            }
        }

    }
}
