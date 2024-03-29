CREATE PROCEDURE UpdateAddress
	@AddressID uniqueidentifier = NULL,
    @Location VARCHAR(100),
	@CityID uniqueidentifier,
	@PersonID uniqueidentifier

AS
BEGIN

	DECLARE @TempID uniqueidentifier;

	IF @AddressID IS NULL

		BEGIN

		EXEC InsertAddress @Location, @CityID, @TempID output

		update Persons
		SET Address_id = @TempID
		Where Person_id = @PersonID

		END;

	ELSE IF @AddressID IS NOT NULL

	BEGIN

	update Addresses
		SET Location_ = @Location, City_id = @CityID	
		Where Address_id = @AddressID

	END;

END;