use Airticket

CREATE PROCEDURE InsertFlightJourney
    @fromDestination NVARCHAR(255),
    @toDestination NVARCHAR(255),
    @departureDate DATE,
    @departureTime TIME,
    @flightName NVARCHAR(255),
    @seatType NVARCHAR(255)  -- Add seatType parameter
AS
BEGIN
    DECLARE @flightId INT;

    -- Get the flightId based on the provided flight name
    SELECT @flightId = [flightId]
    FROM [Airticket].[dbo].[TblFlight]
    WHERE [flightName] = @flightName;

    -- Insert the new record into TblFlightBooking
    INSERT INTO [Airticket].[dbo].[TblFlightJourney] ([fromDestination], [toDestination], [departureDate], [departureTime], [flightId], [seatType])  -- Include seatType
    VALUES (@fromDestination, @toDestination, @departureDate, @departureTime, @flightId, @seatType);

    -- Optionally, you can return the newly generated flightBookingId
    -- SELECT SCOPE_IDENTITY() AS flightBookingId;
END