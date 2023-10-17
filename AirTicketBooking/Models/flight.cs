using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirTicketBooking.Models
{
    public class flight
    {
        [Key]
        public int FlightId { get; set; }
        public string FlightName { get; set; }
        public int SeatingCapacity { get; set; }
        public decimal FlightPrice { get; set; }
    }

    public class FlightBooking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal PaymentDetails { get; set; }
        public int PassengerCount { get; set; }
    }

   

    public class FlightJourney
    {
        public int FlightBookingId { get; set; }
        public string FromDestination { get; set; }
        public string ToDestination { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int FlightId { get; set; }

        public string FlightName { get; set; }
        public string SeatType { get; set; }
    }


    public class AddFlightJourney
    {
        [Key]
        public int FlightBookingId { get; set; }

        [Display(Name =" From ")]
        public string FromDestination { get; set; }

        [Display(Name = " To ")]
        public string ToDestination { get; set; }

        [Display(Name = " Departure date ")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Display(Name = " Time ")]
        [DataType(DataType.Time)]
        public TimeSpan DepartureTime { get; set; }

        [Display(Name = " Flight name ")]
        public string  FlightName { get; set; }

        [Display(Name = " Seat type ")]
        public string SeatType { get; set; }
    }


}