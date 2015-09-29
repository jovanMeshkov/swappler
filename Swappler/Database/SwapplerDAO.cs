using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Swappler.Models;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Swappler.Database
{
    public class SwapplerDAO
    {
        public static List<User> getAllUsers()
        {
            String TEST_QUERY = "select * from Users";
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand command = new SqlCommand(TEST_QUERY, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine(reader.ToString());
            return null;
        }
    }
}