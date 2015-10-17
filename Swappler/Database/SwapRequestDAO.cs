using MySql.Data.MySqlClient;
using Swappler.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Swappler.Database
{
    /*
     * Swap request data access object.
     * 
     */
    public class SwapRequestDAO
    {
        private String SwapplerConnectionString;
        private MySqlConnection connection;

        public SwapRequestDAO()
        {
            SwapplerConnectionString = ConfigurationManager.ConnectionStrings["SwapplerMySqlConnection"].ConnectionString;
            Console.WriteLine(SwapplerConnectionString);
            connection = new MySqlConnection();
            connection.ConnectionString = "server=box1021.bluehost.com;user id=kvantumo_adijo;password=adijoIT-Proekt2015;database=kvantumo_swappler;";
            // TODO: Change hardcoded connection string :D :P
        }

        public Boolean addSwapRequest(SwapRequest swapRequest)
        {
            int result = 0;
            String INSERT_REQUEST_QUERY = "INSERT INTO SwapRequest (Guid, Date, Money, FlagActive, SwapItemId, OfferItemId) " + // UserId is Username from User;
               "VALUES ('" + swapRequest.SwapRequestGuid + "','" + swapRequest.Date + "','" + swapRequest.Money + "','" + swapRequest.FlagActive + "','" + swapRequest.SwapItem.SwapItemGuid + "','" + swapRequest.OfferItem.SwapItemGuid + "')";

            MySqlCommand insertCommand = new MySqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = INSERT_REQUEST_QUERY;
            Debug.WriteLine(INSERT_REQUEST_QUERY);

            try
            {
                connection.Open();
                result = insertCommand.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine("EXCEPTION WHILE ADDING SWAP REQUEST: \n" + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return result == 1 ? true : false;
        }

        public Boolean removeSwapRequest(String swapRequestGuid)
        {
            //TODO: Change to parametrized SQL Queries (Sql injection security) :P 

            String DELETE_REQUEST_QUERY = "DELETE FROM SwapRequest WHERE Guid = '" + swapRequestGuid + "'";
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = DELETE_REQUEST_QUERY;
            int result = 0;

            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return result == 1 ? true : false;
        }

        public List<SwapRequest> query(String sqlQuery)
        {
            List<SwapRequest> resultList = new List<SwapRequest>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = sqlQuery;
            Debug.WriteLine("Quering: " + " - " + sqlQuery);
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                Debug.WriteLine("Reader: " + reader.HasRows + " num rows " + reader.FieldCount);
                if (!reader.HasRows)
                {
                    return resultList;
                }

                while (reader.Read())
                {
                    String requestGuid = reader["Guid"].ToString();
                    DateTime requestDateTime = reader.GetDateTime(reader.GetOrdinal("Date"));
                    String requestMoney = reader["Money"].ToString();
                    String requestFlagActive = reader["FlagActive"].ToString();
                    String requestSwapItem = reader["SwapItemId"].ToString();
                    String requestOfferItem = reader["OfferItemId"].ToString();

                    // Temporary store user id.

                    SwapItem swapItem = new SwapItem();
                    SwapItem offerItem = new SwapItem();
                    swapItem.SwapItemGuid = requestSwapItem;
                    offerItem.SwapItemGuid = requestOfferItem;

                    SwapRequest swapRequest = new SwapRequest(requestGuid, swapItem, offerItem, requestDateTime, Int32.Parse(requestMoney));
                    resultList.Add(swapRequest);
                }
                connection.Close();

                foreach (SwapRequest swapRequest in resultList)
                {
                    String GET_SWAP_ITEM = "select * from SwapItem i where i.Guid='" + swapRequest.SwapItem.SwapItemGuid + "'";
                    String GET_OFFER_ITEM = "select * from SwapItem i where i.Guid='" + swapRequest.OfferItem.SwapItemGuid + "'";        

                    SwapItemsDAO swapItemsDAO = new SwapItemsDAO();
                    List<SwapItem> swapItems = swapItemsDAO.query(GET_SWAP_ITEM);
                    List<SwapItem> offerItems = swapItemsDAO.query(GET_OFFER_ITEM);

                    if (swapItems.Count > 0 && offerItems.Count > 0)
                    {
                        swapRequest.SwapItem = swapItems.ElementAt(0);
                        swapRequest.OfferItem = offerItems.ElementAt(0);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return resultList;
        }
    }
}