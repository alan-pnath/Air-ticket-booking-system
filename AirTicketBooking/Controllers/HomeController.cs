using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirTicketBooking.Models;
using System.Configuration;
using AirTicketBooking.Repository;
using System.Web.Security;

namespace AirTicketBooking.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(login login) // Use a model for input parameters
        {
            UserDataAccess dataAccess = new UserDataAccess();

            if (ModelState.IsValid)
            {
                string userType = dataAccess.ValidateLogin(login.Email, login.Password, out string Usertype);

                if (!string.IsNullOrEmpty(Usertype))
                {
                    FormsAuthentication.SetAuthCookie(login.Email, false);

                    if (userType.Equals("User", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Index", "User"); // Redirect to User view
                    }
                    else if (userType.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Index", "Admin"); // Redirect to Admin view
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(login);
        }



        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(UserReg users)
        {
            UserDataAccess dataAccess = new UserDataAccess();

            try
            {
                if (ModelState.IsValid)
                {
                    // Attempt to insert the user
                    bool userInserted = dataAccess.InsertUser(users);

                    if (userInserted)
                    {
                        ViewBag.RegistrationSuccess = true;
                        return RedirectToAction("Signin", "Home");
                    }
                    else
                    {
                        // User already exists, set a message
                        ViewBag.Message = "User with the same email already exists.";
                        return RedirectToAction("Signup", "Home");


                    }
                }

                // If model validation fails or user already exists, return the view with validation errors or a message
                return View(users);
            }
            finally
            {
                Console.WriteLine("User registered successfully ");
            }
        }


        public ActionResult Aboutus()
        {
            return View();
        }

        public ActionResult Contactus()
        {
            return View();
        }

    }
}
