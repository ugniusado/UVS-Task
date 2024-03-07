using System;
using System.IO;
using System.Threading.Tasks;
using Npgsql;

    namespace DatabaseSchema
    {
        public class DatabaseSetup
        {
            private readonly string Password;
            private readonly string Database;
            private readonly string Port;
            private readonly string SchemaLocation;

            public DatabaseSetup(string password, string database, string port, string schemaLocation)
            {
                Password = password;
                Database = database;
                Port = port;
                SchemaLocation = schemaLocation;
            }

            public async Task InitializeDatabase()
            {
                Console.WriteLine("Waiting for database to start");
                await TestConnection();

                Console.WriteLine("Adding new database");
                await CreateDatabase();

                Console.WriteLine("Adding database schema");
                await ImportSchema();
            }

            private async Task TestConnection()
            {
                Exception? latestException = null;
                var then = DateTime.UtcNow;
                while (DateTime.UtcNow - then < TimeSpan.FromMinutes(0.2))
                {
                    try
                    {
                        using var cnxn = new NpgsqlConnection($"Server=localhost; User ID=postgres; Password={Password}; Port={Port};");
                        await cnxn.OpenAsync();
                        Console.WriteLine("Connection attempt succeeded");

                        return;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Connection attempt failed");
                        latestException = e;
                        await Task.Delay(1000);
                    }
                }

                throw new InvalidOperationException("Could not connect to database", latestException);
            }

            private async Task CreateDatabase()
            {
                using var cnxn = new NpgsqlConnection($"Server=localhost; User ID=postgres; Password={Password}; Port={Port};");
                await cnxn.OpenAsync();

                var command = cnxn.CreateCommand();
                command.CommandText = $"CREATE DATABASE {Database}";

                await command.ExecuteNonQueryAsync();
            }

            private async Task ImportSchema()
            {
                using var cnxn = new NpgsqlConnection($"Server=localhost; User ID=postgres; Password={Password}; Port={Port}; Database={Database};");
                await cnxn.OpenAsync();

                var command = cnxn.CreateCommand();
                command.CommandText = await File.ReadAllTextAsync(SchemaLocation);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
