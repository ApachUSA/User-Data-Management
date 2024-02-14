CREATE PROCEDURE GetPersonsWithFilters
    @cityID uniqueidentifier = NULL,
    @positionID uniqueidentifier = NULL,
    @departID uniqueidentifier = NULL,
    @companyID uniqueidentifier = NULL,
    @pib NVARCHAR(100) = NULL
AS
BEGIN
    SELECT Person_id, Surname, Name_, Patronymic, Birth_date, City_name, Position_name, Department_name, Company_name
    FROM Persons

    LEFT JOIN Addresses ON Persons.Address_id = Addresses.Address_id
    LEFT JOIN Cities ON Addresses.City_id = Cities.City_id
    LEFT JOIN Employees ON Persons.Employee_id = Employees.Employee_id
    LEFT JOIN Positions ON Employees.Position_id = Positions.Position_id
    LEFT JOIN Departments ON Positions.Department_id = Departments.Department_id
    LEFT JOIN Companies ON Departments.Company_id = Companies.Company_id

    WHERE (@cityID IS NULL OR Cities.City_id = @cityID)
      AND (@positionID IS NULL OR Positions.Position_id = @positionID)
      AND (@departID IS NULL OR Departments.Department_id = @departID)
      AND (@companyID IS NULL OR Companies.Company_id = @companyID)
      AND (@pib IS NULL
           OR Surname IN (SELECT value FROM STRING_SPLIT(@pib, ' '))
           OR Name_ IN (SELECT value FROM STRING_SPLIT(@pib, ' '))
           OR Patronymic IN (SELECT value FROM STRING_SPLIT(@pib, ' ')));
END