SELECT TOP (1000) [BookingId]
      ,[UserId]
      ,[FlightId]
      ,[BookingDate]
      ,[PaymentAmount]
      ,[PassengerCount]
      ,[FromDestination]
      ,[ToDestination]
  FROM [Airticket].[dbo].[TblFlightBooking]


  CREATE PROCEDURE InsertUserFlightBooking
    @email NVARCHAR(255),
    @flightname NVARCHAR(255),
    @PaymentAmount DECIMAL,
    @PassengerCount INT,
    @FromDestination NVARCHAR(255),
    @ToDestination NVARCHAR(255)
AS
BEGIN
    DECLARE @UserId INT
    DECLARE @FlightId INT

    -- Get UserId based on the provided email
    SELECT @UserId = UserId
    FROM TblUserLogin
    WHERE Email = @email

    -- Get FlightId based on the provided flightname
    SELECT @FlightId = FlightId
    FROM TblFlight
    WHERE [flightName] = @flightname

    -- Insert the booking into TblFlightBooking
    INSERT INTO TblFlightBooking (
        UserId,
        FlightId,
        BookingDate,
        PaymentAmount,
        PassengerCount,
        FromDestination,
        ToDestination
    )
    VALUES (
        @UserId,
        @FlightId,
        GETDATE(), -- You can use the current date and time for BookingDate, or specify it as needed
        @PaymentAmount,
        @PassengerCount,
        @FromDestination,
        @ToDestination
    )
END
