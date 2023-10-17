using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
namespace AirTicketBooking.Models
{
    public class UserReg
    {
        [Key]
        public int userId { get; set; } 

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class login
    {

        [Required(ErrorMessage = "Please enter your Email ID.")]
        [Display(Name = "Email : ")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your Password.")]
        [Display(Name = "Password : ")]
        public string Password { get; set; }

        public string firstName { get; set; }


    }

   

    public class BookingFlight
    {
        [Display(Name = "From Destination")]
        public string FromDestination { get; set; }

        [Display(Name = "To Destination")]
        public string ToDestination { get; set; }

        [Display(Name = "Flight Name")]
        public string FlightName { get; set; }

        [Display(Name = "Seat type")]
        public string SeatType { get; set; }

        [Display(Name = "Departure Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int FlightId { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }

        [Display(Name = "Passenger count")]
        public int PassengerCount { get; set; }

        [Display(Name = "Payment amount")]
        public decimal PaymentAmount { get; set; }
    }

    public class BookingFlightViewModel
    {
        public BookingFlight BookingFlight { get; set; }
        public List<string> FlightNames { get; set; }
    }

    public class Search
    {
        [Display(Name = "From Destination")]
        public string FromDestination { get; set; }

        [Display(Name = "To Destination")]
        public string ToDestination { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }


    }

    public class SearcResult
    {
        public string FromDestination { get; set; }
        public string ToDestination { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int FlightId { get; set; }

        public string FlightName { get; set; }
        public string SeatType { get; set; }
    }
    

    public class UserRegModel
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }

        [Required]
        [Display(Name = "Age ")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

}