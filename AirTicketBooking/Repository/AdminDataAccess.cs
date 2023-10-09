using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using AirTicketBooking.Models;

namespace AirTicketBooking.Repository
{
    public class UserDataRepository
    {
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
                SqlCommand cmd = new SqlCommand("GetAllFlightJourneyData", con); // Assuming you have a stored procedure with this name
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
                        DepartureTime = TimeSpan.Parse(row["departureTime"].ToString()), // Assuming the time is stored as a string
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

}