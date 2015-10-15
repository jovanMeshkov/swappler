using MySql.Data.MySqlClient;
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

        public List<SwapItem> query(String sqlQuery)
        {
            List<SwapItem> resultList = new List<SwapItem>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = sqlQuery;

            try 
            {
                
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                connection.Close();

                Debug.WriteLine("QUERY IS : " + sqlQuery);
                Debug.WriteLine("reader : " + reader.HasRows);

                if (!reader.HasRows)
                {
                    return resultList;
                }

                while (reader.Read())
                {
                        String itemName = reader["Name"].ToString();
                        String itemDescription = reader["Description"].ToString();
                        DateTime itemDateTime = reader.GetDateTime(reader.GetOrdinal("Date"));
                        int userId = reader.GetInt16(reader.GetOrdinal("UserID"));
                        
                        command.CommandText = "select * from User u where u.UserID='" + userId + "'";
                        connection.Open();
                        MySqlDataReader readerUser = command.ExecuteReader();
                        connection.Close();
                        User itemUser = null;
                        if (readerUser.Read())
                        {
                                itemUser = new User(
                                readerUser["Name"].ToString(),
                                readerUser["LastName"].ToString(),
                                readerUser["Email"].ToString(),
                                readerUser["Password"].ToString(),
                                readerUser["Username"].ToString(),
                                readerUser["Phone"].ToString(),
                                readerUser["AddressID"].ToString()
                            );
                        }
                        SwapItem swapItem = new SwapItem(itemName, itemDescription, itemDateTime, itemUser);
                        resultList.Add(swapItem);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                connection.Close();
            }
            return resultList;
        }
    }
}