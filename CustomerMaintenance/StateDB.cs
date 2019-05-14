using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CustomerMaintenance
{
    public class StateDB
    {
        public static List<State> GetStates()
        {
            // declare and instantiate List of States
            List<State> states = new List<State>();
            // create connection object to connect to database
            SqlConnection connection = MMABooksDB.GetConnection();
            // string variable of sql select statement
            string selectStatement = "SELECT StateCode, StateName "
                                   + "FROM States "
                                   + "ORDER BY StateName";
            // create a sql command object to execute commands
            SqlCommand selectCommand = 
                new SqlCommand(selectStatement, connection);
            try
            {
                // establishes connection
                connection.Open();
                // returns a reader object
                SqlDataReader reader = selectCommand.ExecuteReader();
                // while the reader is read
                while (reader.Read())
                {
                    // State object
                    State s = new State();
                    // read the code and name fields (represented as array of chars) 
                    //  and convert into a string
                    s.StateCode = reader["StateCode"].ToString();
                    s.StateName = reader["StateName"].ToString();
                    // add State to the states List
                    states.Add(s);
                }
                // close data reader
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            // finally block, closes connection after try or catch is executed
            finally
            {
                connection.Close();
            }
            // return the states List
            return states;
        }
    }
}
