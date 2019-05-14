using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CustomerMaintenance
{
    public class CustomerDB
    {
        // get Customer object, takes int customerID parameter
        public static Customer GetCustomer(int customerID)
        {
            SqlConnection connection = MMABooksDB.GetConnection();
            // SQL select statement. @ goes in front of variables for SQL
            string selectStatement
                = "SELECT CustomerID, Name, Address, City, State, ZipCode "
                + "FROM Customers "
                + "WHERE CustomerID = @CustomerID";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            // creates CustomerID parameter to hold value in customerID variable
            selectCommand.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                //
                if (custReader.Read())
                {
                    // note all the casting btwn types!
                    Customer customer = new Customer();
                    customer.CustomerID = (int)custReader["CustomerID"];
                    customer.Name = custReader["Name"].ToString();
                    customer.Address = custReader["Address"].ToString();
                    customer.City = custReader["City"].ToString();
                    customer.State = custReader["State"].ToString();
                    customer.ZipCode = custReader["ZipCode"].ToString();
                    custReader.Close();
                    return customer;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
