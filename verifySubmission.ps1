cd EmployeeManagement

# Ensure the connection string environment variable is set
$Env:EmployeeManagementConnectionString = "Server=localhost; User ID=postgres; Password=guest; Port=7777; Database=uvsproject;"

# Build the project
dotnet build

# Execute set and get commands
dotnet run -- set-employee --employeeId 1 --employeeName "John" --employeeSalary 123
dotnet run -- set-employee --employeeId 2 --employeeName "Steve" --employeeSalary 456
dotnet run -- get-employee --employeeId 1
dotnet run -- get-employee --employeeId 2

# Navigate back to the original directory
cd ..
