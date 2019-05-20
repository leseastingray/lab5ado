using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CustomerMaintenance
{
    class MMABooksDB
    {
        public static SqlConnection GetConnection()
        {
            //string connectionString = "Data Source=1913511-C18096;Initial Catalog=MMABooks;Integrated Security=True"; // lab PC
            string connectionString = "Data Source=1912851-C20251;Initial Catalog=MMABooks;Integrated Security=True"; //class laptop 51
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
