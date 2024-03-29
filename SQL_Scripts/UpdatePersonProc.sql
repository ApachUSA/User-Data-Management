CREATE PROCEDURE UpdatePerson
	@AddressID uniqueidentifier = NULL,
	@PersonID uniqueidentifier,
    @Surname NVARCHAR(30),
    @Name NVARCHAR(20),
    @Patronymic NVARCHAR(30) = NULL,
	@BDate DATE = NULL,
	@EmployeeID uniqueidentifier = NULL

AS
BEGIN
		update Persons
		SET Surname = @Surname,
		Name_ = @Name,
		Patronymic = @Patronymic,
		Birth_date = @BDate,
		Address_id = @AddressID,
		Employee_id = @EmployeeID
		Where Person_id = @PersonID

END;