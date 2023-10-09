--flight booking details stored procedure

CREATE PROCEDURE GetBookingData
    @UserId INT,
    @FlightId INT
AS
BEGIN
    SELECT
        FB.BookingId,
        U.UserId,
        U.firstName AS UserFirstName,
        U.lastName AS UserLastName,
        FB.FlightId,
        F.flightName,
        F.seatingCapacity,
        F.flightPrice,
        FB.BookingDate,
        FB.PaymentDetails,
        FB.PassengerCount
        -- Add other fields as needed
    FROM
        TblFlightBooking FB
    INNER JOIN
        TblUserLogin U ON FB.UserId = U.UserId
    INNER JOIN
        TblFlight F ON FB.FlightId = F.flightId
    WHERE
        FB.UserId = @UserId AND FB.FlightId = @FlightId;
END;
