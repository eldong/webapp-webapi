using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace mywebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string connString;
            string sqlServerVersion;
            List<string> Quotes = new List<string>();

            // [SuppressMessage("Microsoft.Security", "CS002:SecretInNextLine", Justification="Demo")]
            connString = "Server=tcp:contosobikedb.database.windows.net,1433;" + "Initial Catalog=contosodb;Persist Security Info=False;" + "User ID=dbadmin;Password=DBpass!2;MultipleActiveResultSets=False;" +
                "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            string queryString = "SELECT * from Quotes";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Quotes.Add(reader["Quote"]  + " - " + reader["Who"]);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }


                Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
                Console.WriteLine("State: {0}", connection.State);
                sqlServerVersion = connection.ServerVersion;
            }
            return Quotes;

            //return new string[] { "The two most important days in your life are the day you are born and the day you find out why. ~ Mark Twain",
            //                      "Eighty percent of success is showing up. ~ Woody Allen" ,
            //                      "Believe you can and you’re halfway there. ~ Theodore Roosevelt",
            //                      "Containers Rock! ~ Eldon"};
            
        }
    }
}