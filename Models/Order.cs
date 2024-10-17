using System;
using System.Data.SqlClient;
using DatabaseConnection; 

namespace TechShopApp.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public static void PlaceOrder(DatabaseConnector dbConnector, Order order)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) " +
                               "VALUES (@CustomerID, @OrderDate, @TotalAmount)";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error placing order: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public static void UpdateOrder(DatabaseConnector dbConnector, Order order)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "UPDATE Orders SET CustomerID = @CustomerID, TotalAmount = @TotalAmount " +
                               "WHERE OrderID = @OrderID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@OrderID", order.OrderID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error updating order: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public static Order? GetOrderById(DatabaseConnector dbConnector, int orderId) 
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "SELECT * FROM Orders WHERE OrderID = @OrderID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Order
                            {
                                OrderID = (int)reader["OrderID"],
                                CustomerID = (int)reader["CustomerID"],
                                OrderDate = (DateTime)reader["OrderDate"],
                                TotalAmount = (decimal)reader["TotalAmount"],
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error retrieving order: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
            return null; 
        }
    }
}
