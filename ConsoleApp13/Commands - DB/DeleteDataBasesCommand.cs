using CoursesDB.Client;
using CoursesDB.Client.Model;
using DBTester.IO;
using OrderManagementSystem.Common.Settings;

namespace DBTester
{
    public class DeleteDataBasesCommand : IDeleteDataBasesCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IDBClient dbClient;

        public DeleteDataBasesCommand(ISettingProvider settings, IWriter writer, IReader reader, IDBClient dbClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
        }
        public string Name { get => "Delete Databases"; }
        public string Key { get => "d"; }

        public void Execute()
        {
            writer.Write("Enter Db Name");
            var result = dbClient.DeleteDb(new DeleteDbRequest {
                Name = reader.ReadMessage()
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
