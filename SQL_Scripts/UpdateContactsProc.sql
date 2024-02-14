CREATE PROCEDURE UpdateContacts
	@ContactID uniqueidentifier = NULL,
	@PhoneNumber NVARCHAR(30),
	@PersonID uniqueidentifier

AS
BEGIN

	IF @ContactID IS NULL

		BEGIN

		EXEC InsertContacts @PhoneNumber = @PhoneNumber, @UserID = @PersonID;

		END;

	ELSE IF @ContactID IS NOT NULL

	BEGIN

	update Contacts
		SET Phone_number = @PhoneNumber
		Where Contacts_id = @ContactID

	END;

END;