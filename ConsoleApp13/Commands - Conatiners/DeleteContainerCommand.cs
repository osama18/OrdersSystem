
using DBTester.IO;
using OrderManagementSystem.Common.Settings;
using OrderManagementSystem.ORM.Containers.Client;
using OrderManagementSystem.ORM.Containers.Client.Model;

namespace DBTester
{
    public class DeleteContainerCommand : IDeleteContainerCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IContainerClient dbClient;

        public DeleteContainerCommand(ISettingProvider settings, IWriter writer, IReader reader, IContainerClient dbClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
        }
        public string Name { get => "Delete Conatiner"; }
        public string Key { get => "dc"; }

        public void Execute()
        {
            writer.Write("Enter Db Name > Conatiner Id");
            var result = dbClient.DeleteConatiner(new DeleteContainerRequest{
                DbName = reader.ReadMessage(),
                ConatinerId = reader.ReadMessage(),
            }).GetAwaiter().GetResult();
            if (result.Success)
            {
                writer.Write("Deleted");
            }
            else
            {
                writer.Write("Failed");
            }
        }
    }
}
