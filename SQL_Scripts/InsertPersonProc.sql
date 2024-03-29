CREATE PROCEDURE InsertPerson
    @Surname NVARCHAR(30),
    @Name NVARCHAR(20),
    @Patronymic NVARCHAR(30) = NULL,
	@BDate DATE = NULL,
	@AddressID uniqueidentifier = NULL,
	@EmployeesID uniqueidentifier = NULL,
	@PersonID uniqueidentifier output
AS
BEGIN

    SET @PersonID = NEWID();
    INSERT INTO Persons(Person_id,Surname, Name_, Patronymic, Birth_date,Address_id,Employee_id) 
    VALUES (@PersonID,@Surname, @Name,@Patronymic,@BDate,@AddressID,@EmployeesID);
   
END;