using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using EmployeeManagement.Repositories;
using EmployeeManagement.Models;

namespace EmployeeManagement
{
    public class EmployeeManagementApp
    {
        private readonly string ConnectionString;

        public EmployeeManagementApp(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task Run()
        {
            var employeeRepository = new EmployeeRepository(ConnectionString);
            var rootCommand = new RootCommand();

            var setEmployeeCommand = new Command("set-employee", "Sets an employee's data")
            {
                new Option<int>("--employeeId", "Employee ID") { IsRequired = true },
                new Option<string>("--employeeName", "Employee name") { IsRequired = true },
                new Option<int>("--employeeSalary", "Employee salary") { IsRequired = true }
            };

            setEmployeeCommand.Handler = CommandHandler.Create<int, string, int>(async (employeeId, employeeName, employeeSalary) =>
            {
                var employee = new Employee { EmployeeId = employeeId, EmployeeName = employeeName, EmployeeSalary = employeeSalary };
                await employeeRepository.SetEmployeeAsync(employee);
            });

            var getEmployeeCommand = new Command("get-employee", "Gets an employee's data")
            {
                new Option<int>("--employeeId", "Employee ID") { IsRequired = true }
            };

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

            await rootCommand.InvokeAsync(Environment.GetCommandLineArgs());
        }
    }
}
