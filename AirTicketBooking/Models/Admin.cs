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


        //This method validates the Login credentials
        public String LoginProcess(String strUsername, String strPassword)
        {
            String message = "";
            //my connection string
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Select * from TblAdminLogin where adminUserName=@Username", con);
            cmd.Parameters.AddWithValue("@Username", strUsername);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Boolean login = (strPassword.Equals(reader["adminPassword"].ToString(), StringComparison.InvariantCulture)) ? true : false;
                    if (login)
                    {
                        message = "1";
                        adminName = reader["adminName"].ToString();

                    }
                    else
                        message = "Invalid Credentials";
                }
                else
                    message = "Invalid Credentials";

                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString() + "Error.";

            }
            return message;
        }
    }
}