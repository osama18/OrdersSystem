using CoursesDB.Client;
using CoursesDB.Client.Model;
using DBTester.IO;
using OrderManagementSystem.Common.Settings;
using OrderManagementSystem.ORM.Containers.Client;
using OrderManagementSystem.ORM.Containers.Client.Model;
using System;
using System.Collections.Generic;


namespace DBTester
{
    public class SeedDataBasesCommand : ISeedDataBasesCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IDBClient dbClient;
        private readonly IContainerClient containerClient;
        //private readonly IPaymentDetailsServices PaymentDetailsServices;
        //private readonly IOrdersServices OrdersServices;
        
        public SeedDataBasesCommand(ISettingProvider settings,
            IWriter writer,
            IReader reader,
            IDBClient dbClient,
            IContainerClient containerClient
            )
            //IPaymentDetailsServices PaymentDetailsServices,
            //IOrdersServices OrdersServices)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
            this.containerClient = containerClient;
            //this.PaymentDetailsServices = PaymentDetailsServices;
            //this.OrdersServices = OrdersServices;
        }
        public string Name { get => "Seed Databases"; }
        public string Key { get => "seed"; }

        public void Execute()
        {
            var dbName = settings.GetSetting<string>("OrdersDbName");
            var studentContainer = settings.GetSetting<string>("PaymentDetailsConatiner");
            var courseContainer = settings.GetSetting<string>("OrdersConatiner");

            if (CreateDataBase(dbName))
            {
                if (CreateContainer(studentContainer, dbName))
                {
                    if (CreateContainer(courseContainer,dbName))
                    {
                        //if (SeedData())
                        //{
                        //    writer.Write("Seed completed");
                        //}
                    }
                }
            }
        }

        //private bool SeedData()
        //{
        //    var studentId = PaymentDetailsServices.Create(new StudentDto { 
        //        Name = "Osama",
        //        Age = 32
        //    }).GetAwaiter().GetResult();


        //    var courseId = OrdersServices.Create(new CourseDto { 
        //        Name = "Time managment",
        //        Capacity = 10,
        //        TeacheId = Guid.NewGuid(),
        //        PaymentDetailsIds =new List<Guid> { studentId},
        //    }).GetAwaiter().GetResult();

        //    writer.Write($"{studentId.ToString()} subscribe to course {courseId}");
        //    return true;
        //}

        private bool CreateContainer(string container, string dbName, string partiotionKey = "/id")
        {
            var result = containerClient.CreateContainer(new CreateConatinerRequest
            {
                DbName = dbName,
                ConatinerId = container,
                PartiotionKey = partiotionKey,
                Throughput = 400
            }).GetAwaiter().GetResult();

            if (result.Success)
            {
                writer.Write($"Conatiner with ID : {result.ContainerId} Added");
                return true;
            }
            else
            {
                writer.Write("Failed");
                return false;
            }
        }

        private bool CreateDataBase(string dbName)
        {
            var result = dbClient.CreateDb(new CreateDbRequest
            {
                Name = dbName
            }).GetAwaiter().GetResult();

            if (result.Success)
            {
                writer.Write($"Data Base Created. ID : {result.DataBase.Id} , Last Modified {result.DataBase.LastModified}");
                return true;
            }
            else
            {
                writer.Write("Failed to Create Db");
                return false;
            }
        }
    }
}
