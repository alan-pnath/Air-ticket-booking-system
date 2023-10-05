using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirTicketBooking.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace AirTicketBooking.Controllers
{
    public class HomeController : Controller
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(User users)
        {
            if (ModelState.IsValid)
            {
                //message will collect the String value from the model method.
                String message = users.LoginProcess(users.Email, users.Password);
                //RedirectToAction("actionName/ViewName_ActionResultMethodName", "ControllerName");
                if (message.Equals("1"))
                {
                    //this will add cookies for the username.
                    Response.Cookies.Add(new HttpCookie("Users1", users.firstName));
                    //This is a different Controller for the User Homepage. Redirecting after successful process.
                    return RedirectToAction("UserLogged", "User");
                }
                else
                    ViewBag.ErrorMessage = message;
            }
            return View(users);
        }

    public ActionResult Signup()
        {
            return View();
        }



    public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}