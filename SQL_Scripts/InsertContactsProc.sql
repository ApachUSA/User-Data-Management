CREATE PROCEDURE InsertContacts
	@PhoneNumber NVARCHAR(30),
	@UserID uniqueidentifier
AS
BEGIN
        INSERT INTO Contacts (Phone_number, Person_id) VALUES (@PhoneNumber, @UserID);
END;