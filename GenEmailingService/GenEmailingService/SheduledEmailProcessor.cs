using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenEmailingService
{
    public class SheduledEmailProcessor
    {
        public Result ProcessAllScheduledEmails()
        {
            try
            {
                DataTable allEventToProcess = null;
                string connectionString = ConfigurationManager.ConnectionStrings["TotalHRConn"].ToString();
                //
                // create connection and prepare reminders.
                //
                using (var con = new SqlConnection(connectionString))
                {

                    using (var command = new SqlCommand())
                    {
                        command.Connection = con;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetNextScheduledEmail";
                        command.Parameters.AddWithValue("minuteoffset", 10);

                        con.Open();
                        var reader = command.ExecuteReader();

                        //use data adapter to collect this values
                        //collect next schedule with recipient list
                        // send it
                        //update tables in db based on send success.



                        //allEventToProcess = new DataTable();
                        //allEventToProcess.Load(reader);
                    }

                }
                return new Result { ReturnedObject = allEventToProcess, ReturnId = 1 };
            }
            catch (Exception ex)
            {
                return new Result { ReturnId = -1, ReturnedError = ex.ToString() };
            }
        }

        public class Result
        {
            public int ReturnId { get; set; }
            public object ReturnedObject { get; set; }
            public string ReturnedError { get; set; }
        }

    }
}
