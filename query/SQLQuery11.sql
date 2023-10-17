SELECT TOP (1000) [flightBookingId]
      ,[fromDestination]
      ,[toDestination]
      ,[departureDate]
      ,[departureTime]
      ,[flightId]
      ,[seatType]
  FROM [Airticket].[dbo].[TblFlightJourney]


  CREATE PROCEDURE SearchFlights
    @FromDestination NVARCHAR(255),
    @ToDestination NVARCHAR(255),
    @DepartureDate DATE
AS
BEGIN
    SELECT
        flightBookingId,
        fromDestination,
        toDestination,
        departureDate,
        departureTime,
        flightId,
        seatType
    FROM
        [Airticket].[dbo].[TblFlightJourney]
    WHERE
        fromDestination = @FromDestination
        AND toDestination = @ToDestination
        AND (
            @DepartureDate IS NULL
            OR departureDate = @DepartureDate
        );
END
