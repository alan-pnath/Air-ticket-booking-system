using AirTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;


namespace AirTicketBooking.Repository
{
    public class FlightDataAccess
    {
        public class FlightDataRepository
        {
            public List<flight> GetAllFlightDetails()
            {
                SqlConnection con = null;
                DataSet ds = null;
                List<flight> flightList = null;

                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                    SqlCommand cmd = new SqlCommand("GetAllFlightDetails", con); // Assuming you have a stored procedure for this purpose
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    ds = new DataSet();
                    da.Fill(ds);
                    flightList = new List<flight>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        flight flight = new flight
                        {
                            FlightId = Convert.ToInt32(ds.Tables[0].Rows[i]["flightId"]),
                            FlightName = ds.Tables[0].Rows[i]["flightName"].ToString(),
                            SeatingCapacity = Convert.ToInt32(ds.Tables[0].Rows[i]["seatingCapacity"]),
                            FlightPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["flightPrice"])
                        };

                        flightList.Add(flight);
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

                return flightList;
            }

        }
        public flight GetFlightDetailsById(string flightId)
        {
            flight Flight = null;
            SqlConnection con = null;
            
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("GetFlightDetailsByID", con); 
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.Add(new SqlParameter("@flightId", flightId));

                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    Flight = new flight
                    {
                        FlightId = Convert.ToInt32(row["flightId"]),
                        FlightName = row["flightName"].ToString(),
                        SeatingCapacity = Convert.ToInt32(row["seatingCapacity"]),
                        FlightPrice = Convert.ToDecimal(row["flightPrice"])
                    };

                   
                }
                return Flight;
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

            return Flight;
        }

        public string UpdateFlightDetailsById(flight Flight)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateFlightDetailsById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flightId", Flight.FlightId);
                cmd.Parameters.AddWithValue("@flightName", Flight.FlightName);
                cmd.Parameters.AddWithValue("@seatingCapacity", Flight.SeatingCapacity);
                cmd.Parameters.AddWithValue("@flightPrice", Flight.FlightPrice);
               
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



        public string GetFlightJourneyDetailsById(string FlightId)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                SqlCommand cmd = new SqlCommand("GetFlightJourneyById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flightBookingId", FlightId); 

                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch 
            {
                
                return result;
            }
            finally
            {
                
                 con.Close();
                
            }
        }


    }
}