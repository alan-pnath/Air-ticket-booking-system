using AirTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirTicketBooking.Repository;
using static AirTicketBooking.Repository.FlightDataAccess;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace AirTicketBooking.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNewAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewAdmin(AddAdmin add)
        {
            if (ModelState.IsValid)
            {
                    AddAdminRepository admin = new AddAdminRepository();
                    string result = admin.InsertAdmin(add);
                    TempData["result1"] = result;
                    ModelState.Clear();
                    return RedirectToAction("AddNewAdmin");
            }
            else
            {
                ModelState.AddModelError("", "Error ");
                return View();
            }
           
        }

        [HttpGet]
        public ActionResult AddUserData()
        {
            return View();
        }

        //Add user data by admin
        [HttpPost]
        public ActionResult AddUserData(UserRegModel User)
        {
            if (ModelState.IsValid)
            {
                UserDataRepository user = new UserDataRepository();

                string result = user.InsertUser(User);

                TempData["result1"] = result;
                ModelState.Clear();
                return RedirectToAction("GetFlightDetails");

            }
            else
            {
                ModelState.AddModelError("", "Error ");
                return View();
            }
        }

        //get user data to view deatails
        public ActionResult GetUserDetails()
        {
            UserDataRepository dataRepository = new UserDataRepository();
            List<UserReg> userList = dataRepository.GetUserData();

            return View(userList);
        }
        public ActionResult GetUserDetailsById(string userId)
        {
            UserDataRepository dataRepository = new UserDataRepository();
            UserReg user = dataRepository.GetUserDetailsbyId(userId);

            return View(user);
           
        }
        [HttpGet]
        public ActionResult EditUserDetailsById(string ID)
        {
            UserDataRepository dataRepository = new UserDataRepository();
            UserReg user = dataRepository.GetUserDetailsbyId(ID);

            return View(user);

        }

        [HttpPost]
        public ActionResult EditUserDetailsById(UserReg user)
        {
            user.DateofBirth = Convert.ToDateTime(user.DateofBirth);
            if (ModelState.IsValid)
            {
                UserDataRepository dataRepository = new UserDataRepository();
                string result = dataRepository.UpdateUserDataById(user);
                TempData["result2"] = result;
                ModelState.Clear();
                return RedirectToAction("GetUserDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }

        }
        [HttpGet]
        public ActionResult DeleteUserDetailById(int id)
        {
            UserDataRepository UserDelete = new UserDataRepository();

            
            int result = UserDelete.DeleteUserById(id);

            TempData["result3"] = result;
            ModelState.Clear();


            return RedirectToAction("GetUserDetails");
        }


        //get flight details
        public ActionResult GetFlightDetails()
        {
            FlightDataRepository dataRepository = new FlightDataRepository();
            List<flight> flightList = dataRepository.GetAllFlightDetails();

            return View(flightList);
        }

        public ActionResult GetFlightDetailsById(string flightId)
        {
            FlightDataAccess flightDataAccess = new FlightDataAccess();
            flight Flight = flightDataAccess.GetFlightDetailsById(flightId);
            return View(Flight);
        }

        [HttpGet]
        public ActionResult EditFlightDetailsById(string flightId)
        {
            FlightDataAccess flightDataAccess = new FlightDataAccess();
            flight Flight = flightDataAccess.GetFlightDetailsById(flightId);
            return View(Flight);
        }


        [HttpPost]
        public ActionResult EditFlightDetailsById(flight Flight)
        {
            
            if (ModelState.IsValid)
            {
                FlightDataAccess dataRepository = new FlightDataAccess();
                string result = dataRepository.UpdateFlightDetailsById(Flight);
                TempData["result2"] = result;
                ModelState.Clear();
                return RedirectToAction("GetFlightDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }

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
                FlightRepository flightRepository = new FlightRepository(); 
                
                string result = flightRepository.InsertFlight(flight);

                TempData["result1"] = result;
                ModelState.Clear();
                return RedirectToAction("GetFlightDetails");

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

           
            int result = flightRepository.DeleteFlightById(id);

            TempData["result3"] = result;
            ModelState.Clear();

            
            return RedirectToAction("GetFlightDetails");
        }


        //get flight booking details

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

        [HttpGet]
        public ActionResult AddFlightJourneyData()
        {
            List<string> flightNames = new List<string>();

            
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
                            flightNames.Add(flightName);
                        }
                    }
                }
            }

            
            ViewBag.FlightNames = flightNames;

            return View();
        }

        [HttpPost]
        public ActionResult AddFlightJourneyData(AddFlightJourney flightJourney)
        {
            if (ModelState.IsValid)
            {
                FlightRepository flightRepository = new FlightRepository(); 

                
                string result = flightRepository.InsertFlightJourney(flightJourney);

                TempData["result2"] = result;
                Console.WriteLine("Result: " + result);
                ModelState.Clear();
                return RedirectToAction("AddFlightJourneyData");
                
            }
            else
            {
                
                ModelState.AddModelError("", "Validation failed.");
                return View();
            }
        }

        public ActionResult GetFlightJourneyDataById(string FlightId)
        {
            FlightDataAccess flightDataAccess = new FlightDataAccess();
            string flightDetails = flightDataAccess.GetFlightJourneyDetailsById(FlightId);
            return View(flightDetails);
        }



        public ActionResult DeleteFlightJourney(int id)
        {
            FlightRepository flightRepository = new FlightRepository();

            
            int result = flightRepository.DeleteFlightJourneyById(id);

            TempData["result3"] = result;
            ModelState.Clear();


            return RedirectToAction("GetFlightJourneydata");
        }


        public ActionResult Logout()
        {
            TempData.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Signin","Home");
        }

    }
}
