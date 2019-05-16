using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CustomerMaintenance
{
    public class ProductDB
    {
        // get Product object, takes string productCode parameter
        public static Product GetProduct(string productCode)
        {
            SqlConnection connection = MMABooksDB.GetConnection();
            // SQL select statement. @ goes in front of variables for SQL. * = all
            string selectStatement
                = "SELECT * "
                + "FROM Products "
                + "WHERE ProductCode = @ProductCode";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            // creates ProductCode parameter to hold value in productCode variable
            selectCommand.Parameters.AddWithValue("@ProductCode", productCode);

            try
            {
                connection.Open();
                SqlDataReader prodReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                //
                if (prodReader.Read())
                {
                    // note all the casting btwn types!
                    Product product = new Product();
                    product.ProductCode = prodReader["ProductCode"].ToString();
                    product.Description = prodReader["Description"].ToString();
                    product.UnitPrice = (decimal)prodReader["UnitPrice"];
                    product.OnHandQuantity = (int)prodReader["OnHandQuantity"];
                    prodReader.Close();
                    return product;
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
        public static void AddProduct(Product product)
        {
            SqlConnection connection = MMABooksDB.GetConnection();
            string insertStatement =
                "INSERT Products " +
                "(ProductCode, Description, UnitPrice, OnHandQuantity) " +
                "VALUES (@ProductCode, @Description, @UnitPrice, @OnHandQuantity)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@ProductCode", product.ProductCode);
            insertCommand.Parameters.AddWithValue(
                "@Description", product.Description);
            insertCommand.Parameters.AddWithValue(
                "@UnitPrice", product.UnitPrice);
            insertCommand.Parameters.AddWithValue(
                "@OnHandQuantity", product.OnHandQuantity);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
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
        public static bool DeleteProduct(Product product)
        {
            // this method requires all parameters to match, in order to handle
            //  concurrency. this way, if another person edits a record, it won't delete
            SqlConnection connection = MMABooksDB.GetConnection();
            string deleteStatement =
                "DELETE FROM Products " +
                "WHERE ProductCode = @ProductCode " +
                "AND Description = @Description " +
                "AND UnitPrice = @UnitPrice " +
                "AND OnHandQuantity = @OnHandQuantity";
            SqlCommand deleteCommand =
                new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue(
                "@ProductCode", product.ProductCode);
            deleteCommand.Parameters.AddWithValue(
                "@Description", product.Description);
            deleteCommand.Parameters.AddWithValue(
                "@UnitPrice", product.UnitPrice);
            deleteCommand.Parameters.AddWithValue(
                "@OnHandQuantity", product.OnHandQuantity);
            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
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
        public static bool UpdateProduct(Product oldProduct,
        Product newProduct)
        {
            SqlConnection connection = MMABooksDB.GetConnection();
            string updateStatement =
                "UPDATE Products SET " +
                "ProductCode = @ProductCode, " +
                "Description = @Description, " +
                "UnitPrice = @UnitPrice, " +
                "OnHandQuantity = @OnHandQuantity " +
                "WHERE ProductCode = @OldProductCode " +
                "AND Description = @OldDescription " +
                "AND UnitPrice = @OldUnitPrice " +
                "AND OnHandQuantity = @OldOnHandQuantity";
            SqlCommand updateCommand =
                new SqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue(
                "@NewProductCode", newProduct.ProductCode);
            updateCommand.Parameters.AddWithValue(
                "@NewDescription", newProduct.Description);
            updateCommand.Parameters.AddWithValue(
                "@NewUnitPrice", newProduct.UnitPrice);
            updateCommand.Parameters.AddWithValue(
                "@NewOnHandQuantity", newProduct.OnHandQuantity);
            updateCommand.Parameters.AddWithValue(
                "@OldProductCode", oldProduct.ProductCode);
            updateCommand.Parameters.AddWithValue(
                "@OldDescription", oldProduct.Description);
            updateCommand.Parameters.AddWithValue(
                "@OldUnitPrice", oldProduct.UnitPrice);
            updateCommand.Parameters.AddWithValue(
                "@OldOnHandQuantity", oldProduct.OnHandQuantity);
            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
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
