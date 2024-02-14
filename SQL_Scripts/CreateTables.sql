
CREATE TABLE Cities (
    City_id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    City_name VARCHAR(50) NOT NULL
);

CREATE TABLE Addresses (
    Address_id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Location_ VARCHAR(100) NOT NULL,
    City_id uniqueidentifier FOREIGN KEY REFERENCES Cities(city_id) ON DELETE CASCADE not null
);

CREATE TABLE Companies (
    Company_id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Company_name VARCHAR(100) NOT NULL,
    Company_info VARCHAR(MAX) null
);

CREATE TABLE Departments (
    Department_id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Department_name VARCHAR(255) NOT NULL,
    Company_id uniqueidentifier FOREIGN KEY REFERENCES Companies(Company_id) ON DELETE SET NULL,
);

CREATE TABLE Positions (
    Position_id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Position_name VARCHAR(100) NOT NULL,
    Department_id uniqueidentifier FOREIGN KEY REFERENCES Departments(Department_id) ON DELETE SET NULL,
);

CREATE TABLE Employees (
    Employee_id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Date_of_hire DATE NOT NULL,   
    Salary DECIMAL(10,2) NOT NULL,
    Position_id uniqueidentifier FOREIGN KEY REFERENCES Positions(Position_id) ON DELETE SET NULL,
);

CREATE TABLE Persons (
    Person_id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Surname VARCHAR(30) NOT NULL,
    Name_ VARCHAR(20) NOT NULL,
    Patronymic VARCHAR(30) NULL,
    Birth_date DATE NULL,
	Address_id uniqueidentifier FOREIGN KEY REFERENCES Addresses(Address_id)  ON DELETE SET NULL,
	Employee_id uniqueidentifier FOREIGN KEY REFERENCES Employees(Employee_id) ON DELETE SET NULL,
);

-- Таблиця "Контакти"
CREATE TABLE Contacts (
    Contacts_id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Phone_number VARCHAR(30) NOT NULL,
    Person_id uniqueidentifier FOREIGN KEY REFERENCES Persons(Person_id) ON DELETE CASCADE not null
);
