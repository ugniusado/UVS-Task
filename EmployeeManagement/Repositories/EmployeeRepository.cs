using Dapper;
using EmployeeManagement.Models;
using Npgsql;
using System.Data;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SetEmployeeAsync(Employee employee)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"
            INSERT INTO employees (employeeid, employeename, employeesalary) 
            VALUES (@EmployeeId, @EmployeeName, @EmployeeSalary)
            ON CONFLICT (employeeid) 
            DO UPDATE SET employeename = EXCLUDED.employeename, employeesalary = EXCLUDED.employeesalary";

            await connection.ExecuteAsync(sql, employee);
        }
        public async Task<Employee?> GetEmployeeAsync(int employeeId)
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);
            var sqlQuery = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";

            return await db.QueryFirstOrDefaultAsync<Employee>(sqlQuery, new { EmployeeId = employeeId });
        }
    }
}
