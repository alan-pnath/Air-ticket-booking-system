using AirTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static AirTicketBooking.Repository.UserDataAccess;

namespace AirTicketBooking.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.FirstName = TempData["FirstName"] as string;
            return View();
        }
        [HttpGet]
        public ActionResult SearchFlights()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchFlights(string fromDestination, string toDestination, DateTime? departureDate)
        {
            FlightSearchRepository data = new FlightSearchRepository();
            List<FlightJourney> searchList = data.GetSearchData(fromDestination, toDestination, departureDate);

            // Store the searchList in TempData
            TempData["SearchList"] = searchList;

            // Redirect to the SearchFlightResult action
            return RedirectToAction("SearchFlightResult");
        }

        public ActionResult SearchFlightResult()
        {
            // Retrieve the searchList from TempData
            List<FlightJourney> searchList = TempData["SearchList"] as List<FlightJourney>;

            if (searchList != null)
            {
                // Use searchList in your view
                return View(searchList);
            }
            else
            {
                // Handle the case where searchList is not found in TempData
                return RedirectToAction("SearchFlights"); // Redirect to the search form, for example
            }
        }

        public ActionResult BookFlights()
        {
            // Create an instance of the view model
            var viewModel = new BookingFlightViewModel
            {
                BookingFlight = new BookingFlight(), 
                FlightNames = new List<string>()
            };

            // Replace with your connection string
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [flightName] FROM [Airticket].[dbo].[TblFlight]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string flightName = reader["flightName"].ToString();
                            viewModel.FlightNames.Add(flightName);
                        }
                    }
                }
            }

            // Pass the view model to the view
            return View(viewModel);
        }



        public ActionResult UserBookings()
        {
            return View();
        }

        public ActionResult Logout()
        {
            TempData.Clear();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Signin", "Home");
        }
    }
}
