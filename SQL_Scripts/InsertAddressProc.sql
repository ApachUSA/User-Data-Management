CREATE PROCEDURE InsertAddress
    @Location VARCHAR(100),
	@CityID uniqueidentifier,
	@AddressID uniqueidentifier output
AS
BEGIN

	SET @AddressID = NEWID();
    INSERT INTO Addresses (Address_id,Location_, City_id) VALUES (@AddressID, @Location, @CityID);

END;