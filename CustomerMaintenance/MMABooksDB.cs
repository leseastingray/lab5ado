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
            string connectionString = "Data Source=19158-S17809\\MSSQLSERVER2014;Initial Catalog=MMABooks;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
