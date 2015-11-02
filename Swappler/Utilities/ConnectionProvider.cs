using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Swappler.Utilities
{
    public class ConnectionProvider
    {
        public static MySqlConnection MySqlConnection()
        {
            string mySqlConnectionString = ConfigurationManager.ConnectionStrings["SwapplerMySqlConnection"].ConnectionString;
            MySqlConnection connection = new MySqlConnection(mySqlConnectionString);
            return connection;
        }
    }
}