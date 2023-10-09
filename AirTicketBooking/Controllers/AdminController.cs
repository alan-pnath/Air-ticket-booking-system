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

        public ActionResult GetFlightBookings()
        {
            FlightBookingDataRepository dataRepository = new FlightBookingDataRepository();
            List<FlightBooking> flightList = dataRepository.GetFlightBookingData();

            return View(flightList);
        } public ActionResult GetFlightJourneydata()
        {
            FlightJourneyDataRepository dataRepository = new FlightJourneyDataRepository();
            List<FlightJourney> flightList = dataRepository.GetFlightJourneyData();

            return View(flightList);
        }


    }
}
