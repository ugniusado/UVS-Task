CREATE TABLE employees (
    employeeid INT NOT NULL,
    employeename VARCHAR(128) NOT NULL,
    employeesalary INT NOT NULL
);
ALTER TABLE employees ADD CONSTRAINT employeeid_unique UNIQUE (employeeid);

