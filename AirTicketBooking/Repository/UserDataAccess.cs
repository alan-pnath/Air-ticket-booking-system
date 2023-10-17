using AirTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace AirTicketBooking.Repository
{
    public class UserDataAccess
    {
        public bool UserExists(string email)
        {
            // Your connection string
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SqlCommand to check if the user exists based on the email
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM TblUserLogin WHERE [Email] = @Email", connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    // Execute the query
                    int count = (int)cmd.ExecuteScalar();

                    // If count > 0, a user with the same email exists
                    return count > 0;
                }
            }
        }

        public bool InsertUser(UserReg users)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            try
            {
                // Check if the user already exists based on email
                if (UserExists(users.Email))
                {
                    Console.WriteLine("User with the same email already exists.");
                    return false; // Return false to indicate that the user was not inserted
                }
                else 
                { 

               

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("InsertUserDetails", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", users.firstName);
                        cmd.Parameters.AddWithValue("@LastName", users.lastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", users.DateofBirth);
                        cmd.Parameters.AddWithValue("@Age", users.Age);
                        cmd.Parameters.AddWithValue("@PhoneNumber", users.phoneNumber);
                        cmd.Parameters.AddWithValue("@Email", users.Email);
                        cmd.Parameters.AddWithValue("@Password", users.Password);

                        cmd.ExecuteNonQuery();
                    }
                }

                return true; // Return true to indicate that the user was successfully inserted
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred while inserting the user: {exception.Message}");
                return false; // Return false in case of an error
            }
        }



        public string ValidateLogin(string username, string password, out string Usertype, out string FirstName)
        {
            Usertype = "";
            FirstName = "";
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spLogin2", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Useremail", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    SqlParameter outputParamUserType = new SqlParameter("Usertype", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParamUserType);

                    SqlParameter outputParamFirstName = new SqlParameter("FirstName", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParamFirstName);

                    connection.Open();

                    cmd.ExecuteNonQuery();

                    Usertype = outputParamUserType.Value.ToString();
                    FirstName = outputParamFirstName.Value.ToString();
                }
                return Usertype;
            }
        }


        public class FlightSearchRepository
        {
            public List<FlightJourney> GetSearchData(string fromDestination, string toDestination, DateTime? departureDate)
            {
                SqlConnection con = null;
                DataSet ds = null;
                List<FlightJourney> flightJourneyList = null;

                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                    SqlCommand cmd = new SqlCommand("SearchFlights", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@FromDestination", fromDestination);
                    cmd.Parameters.AddWithValue("@ToDestination", toDestination);
                    cmd.Parameters.AddWithValue("@DepartureDate", departureDate);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    ds = new DataSet();
                    da.Fill(ds);
                    flightJourneyList = new List<FlightJourney>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FlightJourney flightJourney = new FlightJourney
                        {
                            FlightBookingId = Convert.ToInt32(ds.Tables[0].Rows[i]["flightBookingId"]),
                            FromDestination = ds.Tables[0].Rows[i]["fromDestination"].ToString(),
                            ToDestination = ds.Tables[0].Rows[i]["toDestination"].ToString(),
                            DepartureDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["departureDate"]),
                            DepartureTime = TimeSpan.Parse(ds.Tables[0].Rows[i]["departureTime"].ToString()),
                            FlightName = ds.Tables[0].Rows[i]["flightName"].ToString(),
                            SeatType = ds.Tables[0].Rows[i]["seatType"].ToString(),
                        };

                        flightJourneyList.Add(flightJourney);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions here
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }

                return flightJourneyList;
            }
        }

    }


}