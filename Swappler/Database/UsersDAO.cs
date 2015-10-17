using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Swappler.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;


    public class UsersDAO
    {
        private String SwapplerConnectionString;
        private MySqlConnection connection;

        public UsersDAO()
        {
            SwapplerConnectionString = ConfigurationManager.ConnectionStrings["SwapplerMySqlConnection"].ConnectionString;
            Console.WriteLine(SwapplerConnectionString);
            connection = new MySqlConnection();
            connection.ConnectionString = "server=box1021.bluehost.com;user id=kvantumo_adijo;password=adijoIT-Proekt2015;database=kvantumo_swappler;";
            // TODO: Change hardcoded connection string :D :P
        }

        public List<User> getAllUsers()
        {
            String SELECT_ALL_USERS_QUERY = "select * from User";
            return queryUsers(SELECT_ALL_USERS_QUERY);
        }

        public List<User> queryUsers(String sqlQuery) 
        {
            List<User> resultList = new List<User>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = sqlQuery;

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User(
                        reader["Name"].ToString(),
                        reader["LastName"].ToString(),
                        reader["Email"].ToString(),
                        reader["Password"].ToString(),
                        reader["Username"].ToString(),
                        reader["Phone"].ToString(),
                        reader["AddressID"].ToString()
                        );
                    resultList.Add(user);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while reading users from database.");
                return null;
            }
            finally
            {
                connection.Close();
            }
            return resultList;
        }

       
        public Boolean removeUser(String username)
        {
            //TODO: Change to parametrized SQL Queries (Sql injection security) :P 

            String DELETE_USER_QUERY = "DELETE FROM User WHERE Username = '" + username + "'";
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = DELETE_USER_QUERY;
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

        public Boolean addUser(User user)
        {
            //TODO: Change AddressID long to Address String in Database.Reflect changes in sql queries.

            int result = 0;
            String GET_USER_QUERY = "select * from User u where u.Username='" + user.Username + "'";

            String INSERT_USER_QUERY = "INSERT INTO User (Name, LastName, Email, Password, Username, Phone) " +
                   "VALUES ('" + user.Name + "','" + user.LastName + "','" + user.Email + "','" + user.Password + "','" + user.Username + "','" + user.Phone + "')";

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = GET_USER_QUERY;

            MySqlCommand insertCommand = new MySqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = INSERT_USER_QUERY;

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Debug.WriteLine("User already exists. Returning false. Cannot add !");
                    return false;
                }

                connection.Close();
                //TODO: Check multiple queries within one connection.
                connection.Open();

                Debug.WriteLine(INSERT_USER_QUERY);         
                result = insertCommand.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                connection.Close();
            }

            return result == 1 ? true : false;
        }

        public Boolean updateUser(User user)
        {
            //TODO: Method stub - updateUser
            return false;
        }

    }
