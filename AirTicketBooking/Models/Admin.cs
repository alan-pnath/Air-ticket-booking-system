using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AirTicketBooking.Models
{
    public class Admin
    {
        [Required(ErrorMessage = "Please enter your User ID.")]
        [Display(Name = "User Id : ")]
        public string adminUserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your Password.")]
        [Display(Name = "Password : ")]
        public string adminPassword { get; set; }

        public string adminName { get; set; }

    }

    public class AddFlight
    {
        [Key]
        public int FlightId { get; set; }

        [Display(Name = "Flight name")]
        public string FlightName { get; set; }

        [Display(Name = "Seating capacity")]
        public int SeatingCapacity { get; set; }

        [Display(Name = "Flight Price")]
        public decimal FlightPrice { get; set; }
    }

}