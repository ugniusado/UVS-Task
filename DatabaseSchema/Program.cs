using System;
using System.Threading.Tasks;

namespace DatabaseSchema
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length > 0 && args[0].Equals("setup-db", StringComparison.OrdinalIgnoreCase))
            {
                var password = Environment.GetEnvironmentVariable("UvsTaskPassword")
                    ?? throw new InvalidOperationException("You must set the UvsTaskPassword environment variable");
                var database = Environment.GetEnvironmentVariable("UvsTaskDatabase")
                    ?? throw new InvalidOperationException("You must set the UvsTaskDatabase environment variable");
                var port = Environment.GetEnvironmentVariable("UvsTaskPort")
                    ?? throw new InvalidOperationException("You must set the UvsTaskPort environment variable");
                var schemaLocation = Environment.GetEnvironmentVariable("UvsTaskSchemaLocation")
                    ?? throw new InvalidOperationException("You must set the UvsTaskSchemaLocation environment variable");

                var databaseSetup = new DatabaseSetup(password, database, port, schemaLocation);
                await databaseSetup.InitializeDatabase();
            }
        }
    }
}