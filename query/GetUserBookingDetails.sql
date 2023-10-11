SELECT TOP (1000) [userId]
      ,[firstName]
      ,[lastName]
      ,[DateofBirth]
      ,[Age]
      ,[PhoneNumber]
      ,[Email]
  FROM [Airticket].[dbo].[TblUserLogin]


  -- Create the stored procedure
CREATE PROCEDURE GetUserBookingDetails
    @FirstName NVARCHAR(50)
AS
BEGIN
    -- Declare a variable to store the userId
    DECLARE @UserId INT

    -- Get the userId based on the provided first name
    SELECT @UserId = [userId]
    FROM [Airticket].[dbo].[TblUserLogin]
    WHERE [firstName] = @FirstName

    -- Check if the userId was found
    IF @UserId IS NOT NULL
    BEGIN
        -- Retrieve booking details for the user using the userId
        SELECT [BookingId]
            ,[UserId]
            ,[FlightId]
            ,[BookingDate]
            ,[PaymentAmount]
            ,[PassengerCount]
            ,[FromDestination]
            ,[ToDestination]
        FROM [Airticket].[dbo].[TblFlightBooking]
        WHERE [UserId] = @UserId
    END
    ELSE
    BEGIN
        -- If the user with the provided first name was not found, return a message
        PRINT 'User not found'
    END
END
