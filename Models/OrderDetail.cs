using System;
using System.Data.SqlClient;
using DatabaseConnection;

namespace TechShopApp.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public static void AddOrderDetail(DatabaseConnector dbConnector, OrderDetail orderDetail)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity) " +
                               "VALUES (@OrderID, @ProductID, @Quantity)";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                    cmd.Parameters.AddWithValue("@ProductID", orderDetail.ProductID);
                    cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error adding order detail: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public static void UpdateOrderDetail(DatabaseConnector dbConnector, OrderDetail orderDetail)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "UPDATE OrderDetails SET ProductID = @ProductID, Quantity = @Quantity " +
                               "WHERE OrderDetailID = @OrderDetailID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductID", orderDetail.ProductID);
                    cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    cmd.Parameters.AddWithValue("@OrderDetailID", orderDetail.OrderDetailID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error updating order detail: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public static OrderDetail? GetOrderDetailById(DatabaseConnector dbConnector, int orderDetailId)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new OrderDetail
                            {
                                OrderDetailID = (int)reader["OrderDetailID"],
                                OrderID = (int)reader["OrderID"],
                                ProductID = (int)reader["ProductID"],
                                Quantity = (int)reader["Quantity"],
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error retrieving order detail: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
            return null;
        }
    }
}
