﻿namespace DataGenerator
{
    using King.Service;
    using Microsoft.Azure.WebJobs;

    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var manager = new RoleTaskManager<object>(new Factory());
            manager.OnStart(config);

            var host = new JobHost();
            manager.Run();

            host.RunAndBlock();

            manager.OnStop();
        }
    }
}
