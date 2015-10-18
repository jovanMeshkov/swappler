using MySql.Data.MySqlClient; //download MySqlConnector .net and add this library from references
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Swappler.Models;
using System.Diagnostics;

namespace Swappler.Database
{
    public class SwapItemsDAO
    {
        private String SwapplerConnectionString;
        private MySqlConnection connection;

        public SwapItemsDAO()
        {
            SwapplerConnectionString = ConfigurationManager.ConnectionStrings["SwapplerMySqlConnection"].ConnectionString;
            Console.WriteLine(SwapplerConnectionString);
            connection = new MySqlConnection();
            connection.ConnectionString = "server=box1021.bluehost.com;user id=kvantumo_adijo;password=adijoIT-Proekt2015;database=kvantumo_swappler;";
            // TODO: Change hardcoded connection string :D :P
        }

        public Boolean addSwapItem(SwapItem swapItem)
        {
            int result = 0;
            String GET_ITEM_QUERY = "select * from SwapItem s where s.Guid='" + swapItem.SwapItemGuid + "'"; //TODO: Check this

            String INSERT_ITEM_QUERY = "INSERT INTO SwapItem (Guid, Name, Description, Date, PhotoURL, FlagSwapped, UserId) " + // UserId is Username from User;
                "VALUES ('" + swapItem.SwapItemGuid + "','" + swapItem.Name + "','" + swapItem.Description + "','" + swapItem.Date + "','" + swapItem.PhotoUrl + "','" + (swapItem.Flag_swapped ? 1 : 0) + "','" + swapItem.UserId.Username + "')";

            Debug.WriteLine(GET_ITEM_QUERY);
            Debug.WriteLine(INSERT_ITEM_QUERY);

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = GET_ITEM_QUERY;

            MySqlCommand insertCommand = new MySqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = INSERT_ITEM_QUERY;

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Debug.WriteLine("Item already exists. Returning false. Cannot add item!");
                    return false;
                }

                connection.Close();
                //TODO: Check multiple queries within one connection.
                connection.Open();

                Debug.WriteLine(INSERT_ITEM_QUERY);
  
                result = insertCommand.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            Debug.WriteLine("ITEM successfully added ! result = " + result);
            return result == 1 ? true : false;
        }


        public Boolean removeSwapItem(String swapItemGuid)
        {
            //TODO: Change to parametrized SQL Queries (Sql injection security) :P 

            String DELETE_ITEM_QUERY = "DELETE FROM SwapItem WHERE Guid = '" + swapItemGuid + "'";
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = DELETE_ITEM_QUERY;
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


        public List<SwapItem> query(String sqlQuery)
        {
            List<SwapItem> resultList = new List<SwapItem>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = sqlQuery;
            Debug.WriteLine("Quering: " + " - " + sqlQuery);
            try 
            {      
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                Debug.WriteLine("Reader: " + reader.HasRows);
                if (!reader.HasRows)
                {
                    return resultList;
                }

                while (reader.Read())
                {
                        String swapItemGuid = reader["Guid"].ToString();
                        String itemName = reader["Name"].ToString();
                        String itemDescription = reader["Description"].ToString();
                        DateTime itemDateTime = reader.GetDateTime(reader.GetOrdinal("Date"));
                        String userUsername = reader["UserID"].ToString();

                        User usernameHolder = new User();
                        usernameHolder.Username = userUsername; // Temporary store user id.

                        SwapItem swapItem = new SwapItem(swapItemGuid, itemName, itemDescription, itemDateTime, usernameHolder);
                        resultList.Add(swapItem);
                }
                connection.Close();

                foreach (SwapItem swapItem in resultList)
                {
                    String GET_USER_QUERY = "select * from User u where u.Username='" + swapItem.UserId.Username + "'";
                    UsersDAO usersDAO = new UsersDAO();
                    List<User> users = usersDAO.queryUsers(GET_USER_QUERY);

                    if (users.Count > 0)
                    {
                        swapItem.UserId = users.ElementAt(0);
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