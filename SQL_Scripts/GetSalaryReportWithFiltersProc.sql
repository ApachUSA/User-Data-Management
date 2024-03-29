CREATE PROCEDURE GetSalaryReportWithFilters
    @positionID uniqueidentifier = NULL,
    @departID uniqueidentifier = NULL,
    @companyID uniqueidentifier = NULL
AS
BEGIN
	SELECT Surname, Name_, Position_name, Department_name, Company_name, Salary
    FROM Companies

    JOIN Departments ON Companies.Company_id = Departments.Company_id
    JOIN Positions ON Departments.Department_id = Positions.Department_id
	JOIN Employees ON Positions.Position_id = Employees.Position_id
    JOIN Persons ON Employees.Employee_id = Persons.Employee_id

	  WHERE 
	  (@positionID IS NULL OR Positions.Position_id = @positionID)
      AND (@departID IS NULL OR Departments.Department_id = @departID)
      AND (@companyID IS NULL OR Companies.Company_id = @companyID)

END