using AirTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult SearchFlights()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchFlights(SearchCriteria criteria)
        {
            SearchFlightRepository repository = new SearchFlightRepository();
            List<BookingFlight> matchingFlights = repository.SearchFlights(criteria);

            return View(matchingFlights);
        }


        public ActionResult BookFlights()
        {
            // Create an instance of the view model
            var viewModel = new BookingFlightViewModel
            {
                BookingFlight = new BookingFlight(), // Initialize your BookingFlight model as needed
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

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
