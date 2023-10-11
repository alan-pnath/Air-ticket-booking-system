using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using AirTicketBooking.Models;
using System.Web.UI.WebControls;

namespace AirTicketBooking.Repository
{
    public class FlightRepository
    {
        public List<BookingFlight> SearchFlights(SearchCriteria criteria)
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<BookingFlight> matchingFlights = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("SearchFlights", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FromDestination", criteria.FromDestination);
                cmd.Parameters.AddWithValue("@ToDestination", criteria.ToDestination);

                if (criteria.DepartureDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@DepartureDate", criteria.DepartureDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DepartureDate", DBNull.Value);
                }

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                matchingFlights = new List<BookingFlight>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BookingFlight flight = new BookingFlight
                    {
                        FlightBookingId = Convert.ToInt32(ds.Tables[0].Rows[i]["flightBookingId"]),
                        FromDestination = ds.Tables[0].Rows[i]["fromDestination"].ToString(),
                        ToDestination = ds.Tables[0].Rows[i]["toDestination"].ToString(),
                        DepartureDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["departureDate"]),
                        DepartureTime = ds.Tables[0].Rows[i]["departureTime"].ToString(),
                        FlightId = Convert.ToInt32(ds.Tables[0].Rows[i]["flightId"]),
                        SeatType = ds.Tables[0].Rows[i]["seatType"].ToString()
                        // Add more properties as needed
                    };

                    matchingFlights.Add(flight);
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

            return matchingFlights;
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
                SqlCommand cmd = new SqlCommand("InsertFlightDetails", con); // Replace with your actual stored procedure name
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FlightName", flight.FlightName);
                cmd.Parameters.AddWithValue("@SeatingCapacity", flight.SeatingCapacity);
                cmd.Parameters.AddWithValue("@FlightPrice", flight.FlightPrice);

                con.Open();
                result = cmd.ExecuteScalar()?.ToString() ?? ""; // ExecuteScalar can return null

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
                SqlCommand cmd = new SqlCommand("InsertFlightJourney", con); // Replace with your actual stored procedure name
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FromDestination", flightJourney.FromDestination);
                cmd.Parameters.AddWithValue("@ToDestination", flightJourney.ToDestination);
                cmd.Parameters.AddWithValue("@DepartureDate", flightJourney.DepartureDate);
                cmd.Parameters.AddWithValue("@DepartureTime", flightJourney.DepartureTime);
                cmd.Parameters.AddWithValue("@FlightName", flightJourney.FlightName);
                cmd.Parameters.AddWithValue("@SeatType", flightJourney.SeatType);

                con.Open();
                result = cmd.ExecuteScalar()?.ToString() ?? ""; // ExecuteScalar can return null

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

                cmd.Parameters.AddWithValue("@flightBookingId", flightBookingId); // Use @flightBookingId

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