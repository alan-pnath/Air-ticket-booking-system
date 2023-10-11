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

   

    }
}