CREATE PROCEDURE InsertEmployees
	@Date_of_Hire DATE,
	@Salary DECIMAL(10,2),
	@PositionID uniqueidentifier,
	@EmployeesID uniqueidentifier output
AS
BEGIN

		SET @EmployeesID = NEWID();
        INSERT INTO Employees(Employee_id,Date_of_hire, Salary, Position_id) VALUES (@EmployeesID, @Date_of_Hire, @Salary, @PositionID);

END;