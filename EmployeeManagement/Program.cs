using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using System;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace EmployeeManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand();

            var setEmployeeCommand = new Command("set-employee", "Sets an employee's data")
            {
                new Option<int>("--employeeId", "Employee ID") { IsRequired = true },
                new Option<string>("--employeeName", "Employee name") { IsRequired = true },
                new Option<int>("--employeeSalary", "Employee salary") { IsRequired = true }
            };

            var getEmployeeCommand = new Command("get-employee", "Gets an employee's data")
            {
                new Option<int>("--employeeId", "Employee ID") { IsRequired = true }
            };

            string connectionString = Environment.GetEnvironmentVariable("EmployeeManagementConnectionString")
                ?? throw new InvalidOperationException("You must set the EmployeeManagementConnectionString environment variable");
            var employeeRepository = new EmployeeRepository(connectionString);

            setEmployeeCommand.Handler = CommandHandler.Create<int, string, int>(async (employeeId, employeeName, employeeSalary) =>
            {
                var employee = new Employee
                {
                    EmployeeId = employeeId,
                    EmployeeName = employeeName,
                    EmployeeSalary = employeeSalary
                };

                await employeeRepository.SetEmployeeAsync(employee);
                Console.WriteLine($"Employee set: {employeeId}, {employeeName}, {employeeSalary}");
            });

            getEmployeeCommand.Handler = CommandHandler.Create<int>(async (employeeId) =>
            {
                var employee = await employeeRepository.GetEmployeeAsync(employeeId);
                if (employee != null)
                {
                    Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.EmployeeName}, Salary: {employee.EmployeeSalary}");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            });

            rootCommand.AddCommand(setEmployeeCommand);
            rootCommand.AddCommand(getEmployeeCommand);

            await rootCommand.InvokeAsync(args);
        }
    }
}