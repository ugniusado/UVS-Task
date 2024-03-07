Employee Management System
--------------------------

This Employee Management System is a .NET 6 console application that interacts with a PostgreSQL database to manage employee records. It provides a simple and efficient command-line interface for adding and retrieving employee information.

### Features:

*   **Database Setup**: Initializes the PostgreSQL database and prepares it for use with the application.
*   **Set Employee**: Adds or updates an employee's record in the database.
*   **Get Employee**: Retrieves and displays an employee's record based on their ID.

### How to Use:

#### Setting Up the Database:

Before using the application, you must set up the PostgreSQL database. A PowerShell script named `setUpDatabase.ps1` is provided for this purpose. This script:

1.  Pulls a PostgreSQL Docker image and runs a container instance.
2.  Sets up the necessary database schema using the provided SQL script.

To run the setup script, execute the following command:

`.\setUpDatabase.ps1`

This will prepare your PostgreSQL database for use with the Employee Management System.

#### Managing Employee Records:

After setting up the database, you can use the application to manage employee records. A PowerShell script named `verifySubmission.ps1` demonstrates how to build and run the application:

1.  **Build the Application**: Compiles the Employee Management System.
2.  **Set and Get Employee Records**: Demonstrates adding new employee records and retrieving existing ones.

To run the verification script, execute the following command:

`.\verifySubmission.ps1`

This script performs the following actions:

*   Sets the employee data for two employees (John with ID 1 and Steve with ID 2).
*   Retrieves and displays the data for both employees.

#### Manual Usage:

Alternatively, you can manually use the application to set or get employee records using the following commands:

*   **Set Employee**:
    
    `dotnet run -- set-employee --employeeId <ID> --employeeName "<Name>" --employeeSalary <Salary>`
    
    Adds or updates an employee's record. For example, to set an employee with ID 3:
    
    `dotnet run -- set-employee --employeeId 3 --employeeName "Jane Doe" --employeeSalary 45000`
    
*   **Get Employee**:
    
    `dotnet run -- get-employee --employeeId <ID>`
    
    Retrieves and displays an employee's record. For example, to get the employee with ID 3:
    
    `dotnet run -- get-employee --employeeId 3`
    

### Project Structure:

*   **EmployeeManagement**: The main application project that includes the logic for the command-line interface and interactions with the employee repository.
*   **EmployeeManagementApp**: Central application logic handling command-line interactions.
*   **EmployeeRepository**: Data access layer responsible for interacting with the PostgreSQL database.
*   **Models**: Contains data models, such as the `Employee` class.
