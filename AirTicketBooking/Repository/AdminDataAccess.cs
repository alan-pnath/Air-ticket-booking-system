using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using AirTicketBooking.Models;
using System.Web.UI.WebControls;
using System.Web.Helpers;
using System.Web.UI;
using System.Security.Cryptography.X509Certificates;

namespace AirTicketBooking.Repository
{

    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class AddAdminRepository
    {
        public string connectionString;

        public string InsertAdmin(AddAdmin add)
        {
            SqlConnection con = null;
            string result = "";

            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString; 
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("sp_AddAdminUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", add.Name);
                cmd.Parameters.AddWithValue("@UserEmail", add.AdminEmail);
                cmd.Parameters.AddWithValue("@Password", add.Password);

                con.Open();
                result = cmd.ExecuteScalar()?.ToString() ?? "";

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
            finally
            {
                con?.Close();
            }
        }
    }


    public class UserDataRepository
    {

        public string connectionString;

        public string InsertUser(UserRegModel user)
        {
            SqlConnection con = null;
            string result = "";

            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("InsertUserDetailsAdmin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", user.firstName);
                cmd.Parameters.AddWithValue("@LastName", user.lastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", user.DateofBirth);
                cmd.Parameters.AddWithValue("@Age", user.Age);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.phoneNumber);
                cmd.Parameters.AddWithValue("@Email", user.Email);

                con.Open();
                result = cmd.ExecuteScalar()?.ToString() ?? "";

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
            finally
            {
                con?.Close();
            }
        }
        public List<UserReg> GetUserData()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<UserReg> userList = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("usp_ViewUserDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                userList = new List<UserReg>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    UserReg user = new UserReg
                    {
                        userId = Convert.ToInt32(ds.Tables[0].Rows[i]["userId"]),
                        firstName = ds.Tables[0].Rows[i]["firstName"].ToString(),
                        lastName = ds.Tables[0].Rows[i]["lastName"].ToString(),
                        DateofBirth = Convert.ToDateTime(ds.Tables[0].Rows[i]["DateofBirth"]),
                        Age = Convert.ToInt32(ds.Tables[0].Rows[i]["Age"]),
                        phoneNumber = ds.Tables[0].Rows[i]["phoneNumber"].ToString(),
                        Email = ds.Tables[0].Rows[i]["Email"].ToString(),

                    };

                    userList.Add(user);
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

            return userList;
        }



        public UserReg GetUserDetailsbyId(string userId)
        {
            SqlConnection con = null;
            DataSet ds = null;
            UserReg user = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("GetUserDetailsbyID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.Add(new SqlParameter("@UserId", userId));

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    user = new UserReg()
                    {
                        userId = Convert.ToInt32(row["userId"]),
                        firstName = row["firstName"].ToString(),
                        lastName = row["lastName"].ToString(),
                        DateofBirth = Convert.ToDateTime(row["DateofBirth"].ToString()),
                        Age = Convert.ToInt32(row["Age"]),
                        phoneNumber = row["phoneNumber"].ToString(),
                        Email = row["Email"].ToString(),
                    };
                }

                return user;
            }
            catch 
            {
                
                return user;
            }
            finally
            {
                con.Close();
            }

            
        }

        public string UpdateUserDataById(UserReg user)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateUserDetailsById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", user.userId);
                cmd.Parameters.AddWithValue("@FirstName", user.firstName);
                cmd.Parameters.AddWithValue("@LastName", user.lastName);
                cmd.Parameters.AddWithValue("@DateofBirth", user.DateofBirth);
                cmd.Parameters.AddWithValue("@Age", user.Age);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.phoneNumber);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return " ";
            }
            finally
            {
                con.Close();
            }
        }

        public int DeleteUserById(int UserId)
        {
            SqlConnection con = null;
            int result;

            string connectionString;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("DeleteUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@userId", UserId);



                con.Open();
                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch
            {
                return result = 0;
            }
            finally
            {
                con?.Close();
            }
        }

    }


    public class FlightBookingDataRepository
    {
        public List<FlightBooking> GetFlightBookingData()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<FlightBooking> bookingList = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("usp_GetAllFlightBookings", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                bookingList = new List<FlightBooking>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    FlightBooking booking = new FlightBooking
                    {
                        BookingId = Convert.ToInt32(row["BookingId"]),
                        UserId = Convert.ToInt32(row["UserId"]),
                        FlightId = Convert.ToInt32(row["FlightId"]),
                        BookingDate = Convert.ToDateTime(row["BookingDate"]),
                        PaymentDetails = Convert.ToDecimal(row["PaymentDetails"]),
                        PassengerCount = Convert.ToInt32(row["PassengerCount"])
                    };

                    bookingList.Add(booking);
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

            return bookingList;
        }
    }


    public class FlightJourneyDataRepository
    {
        public List<FlightJourney> GetFlightJourneyData()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<FlightJourney> journeyList = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("GetAllFlightJourneyData", con); 
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                journeyList = new List<FlightJourney>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    FlightJourney journey = new FlightJourney
                    {
                        FlightBookingId = Convert.ToInt32(row["flightBookingId"]),
                        FromDestination = row["fromDestination"].ToString(),
                        ToDestination = row["toDestination"].ToString(),
                        DepartureDate = Convert.ToDateTime(row["departureDate"]),
                        DepartureTime = TimeSpan.Parse(row["departureTime"].ToString()), 
                        FlightId = Convert.ToInt32(row["flightId"]),
                        SeatType = row["seatType"].ToString()
                    };

                    journeyList.Add(journey);
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

            return journeyList;
        }
    }

    public class FlightRepository
    {
        public string connectionString;

        public string InsertFlight(AddFlight flight)
        {
            SqlConnection con = null;
            string result = "";

            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("InsertFlightDetails", con); 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FlightName", flight.FlightName);
                cmd.Parameters.AddWithValue("@SeatingCapacity", flight.SeatingCapacity);
                cmd.Parameters.AddWithValue("@FlightPrice", flight.FlightPrice);

                con.Open();
                result = cmd.ExecuteScalar()?.ToString() ?? ""; 

                return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return result = "";
            }
            finally
            {
                con?.Close();
            }
        }

        public int DeleteFlightById(int flightId)
        {
            SqlConnection con = null;
            int result;


            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("DeleteFlightDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flightId", flightId);



                con.Open();
                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch
            {
                return result = 0;
            }
            finally
            {
                con?.Close();
            }
        }

        public string InsertFlightJourney(AddFlightJourney flightJourney)
        {
            SqlConnection con = null;
            string result = "";

            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("InsertFlightJourney", con); 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FromDestination", flightJourney.FromDestination);
                cmd.Parameters.AddWithValue("@ToDestination", flightJourney.ToDestination);
                cmd.Parameters.AddWithValue("@DepartureDate", flightJourney.DepartureDate);
                cmd.Parameters.AddWithValue("@DepartureTime", flightJourney.DepartureTime);
                cmd.Parameters.AddWithValue("@FlightName", flightJourney.FlightName);
                cmd.Parameters.AddWithValue("@SeatType", flightJourney.SeatType);

                con.Open();
                result = cmd.ExecuteScalar()?.ToString() ?? ""; 

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result = "";
            }
            finally
            {
                con?.Close();
            }
        }

        public int DeleteFlightJourneyById(int flightBookingId)
        {
            SqlConnection con = null;
            int result;

            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("DeleteFlightJourneyDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flightBookingId", flightBookingId); 

                con.Open();
                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch
            {
                return result = 0;
            }
            finally
            {
                con?.Close();
            }
        }

    }

}