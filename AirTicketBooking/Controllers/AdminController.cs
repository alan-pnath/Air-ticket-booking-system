using AirTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirTicketBooking.Repository;
using static AirTicketBooking.Repository.FlightDataAccess;

namespace AirTicketBooking.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult GetUserDetails()
        {
            UserDataRepository dataRepository = new UserDataRepository();
            List<UserReg> userList = dataRepository.GetUserData();

            return View(userList);
        }

        public ActionResult GetFlightDetails()
        {
            FlightDataRepository dataRepository = new FlightDataRepository();
            List<flight> flightList = dataRepository.GetAllFlightDetails();

            return View(flightList);
        }
        [HttpGet]
        public ActionResult AddFlightDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFlightDetails(AddFlight flight)
        {
            if (ModelState.IsValid)
            {
                FlightRepository flightRepository = new FlightRepository(); // Assuming you have a FlightRepository class

                // Insert the flight data into the database
                string result = flightRepository.InsertFlight(flight);

                if (!string.IsNullOrEmpty(result))
                {
                    TempData["result1"] = result;
                    ModelState.Clear();
                    return RedirectToAction("GetFlightDetails");
                }
                else
                {
                   
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Error ");
                return View();
            }
        }

        [HttpGet]

        public ActionResult DeleteFlight(int id)
        {
            FlightRepository flightRepository = new FlightRepository();

            // Call the method to delete the flight record by flightId
            int result = flightRepository.DeleteFlightById(id);

            TempData["result3"] = result;
            ModelState.Clear();

            
            return RedirectToAction("GetFlightDetails");
        }


        public ActionResult GetFlightBookings()
        {
            FlightBookingDataRepository dataRepository = new FlightBookingDataRepository();
            List<FlightBooking> flightList = dataRepository.GetFlightBookingData();

            return View(flightList);
        } 
        public ActionResult GetFlightJourneydata()
        {
            FlightJourneyDataRepository dataRepository = new FlightJourneyDataRepository();
            List<FlightJourney> flightList = dataRepository.GetFlightJourneyData();

            return View(flightList);
        }


    }
}
