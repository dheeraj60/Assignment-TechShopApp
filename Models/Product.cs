using System;
using System.Data.SqlClient;
using DatabaseConnection; 

namespace TechShopApp.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; } 
        public string? Description { get; set; } 
        public decimal Price { get; set; }

        public static void AddProduct(DatabaseConnector dbConnector, Product product)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "INSERT INTO Products (ProductName, Description, Price) " +
                               "VALUES (@ProductName, @Description, @Price)";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error adding product: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public static void UpdateProduct(DatabaseConnector dbConnector, Product product)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "UPDATE Products SET ProductName = @ProductName, Description = @Description, " +
                               "Price = @Price WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error updating product: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public static Product? GetProductById(DatabaseConnector dbConnector, int productId) 
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "SELECT * FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                ProductID = (int)reader["ProductID"],
                                ProductName = reader["ProductName"]?.ToString(),
                                Description = reader["Description"]?.ToString(),
                                Price = (decimal)reader["Price"],
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error retrieving product: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
            return null;
        }
    }
}
