CREATE PROCEDURE UpdateEmployee
	@EmployeeID uniqueidentifier = NULL,
	@Date_of_Hire DATE,
	@Salary DECIMAL(10,2),
	@PositionID uniqueidentifier = NULL,
	@PersonID uniqueidentifier
AS
BEGIN

	DECLARE @TempID uniqueidentifier;

	IF @EmployeeID IS NULL

		BEGIN

		EXEC InsertEmployees  @Date_of_Hire,@Salary, @PositionID, @TempID output

		update Persons
		SET Employee_id = @TempID
		Where Person_id = @PersonID

		END;

	ELSE IF @EmployeeID IS NOT NULL

	BEGIN

	update Employees
		SET Date_of_hire = @Date_of_Hire,
		Salary = @Salary,
		Position_id = @PositionID
		Where Employee_id = @EmployeeID

	END;

END;
