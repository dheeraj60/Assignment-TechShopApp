using System;
using System.Data.SqlClient;
using DatabaseConnection; 

namespace TechShopApp.Models
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastStockUpdate { get; set; }

        public static void AddInventory(DatabaseConnector dbConnector, Inventory inventory)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate) " +
                               "VALUES (@ProductID, @QuantityInStock, @LastStockUpdate)";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductID", inventory.ProductID);
                    cmd.Parameters.AddWithValue("@QuantityInStock", inventory.QuantityInStock);
                    cmd.Parameters.AddWithValue("@LastStockUpdate", inventory.LastStockUpdate);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error adding inventory: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public static void UpdateInventory(DatabaseConnector dbConnector, Inventory inventory)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "UPDATE Inventory SET QuantityInStock = @QuantityInStock, " +
                               "LastStockUpdate = @LastStockUpdate WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@QuantityInStock", inventory.QuantityInStock);
                    cmd.Parameters.AddWithValue("@LastStockUpdate", inventory.LastStockUpdate);
                    cmd.Parameters.AddWithValue("@ProductID", inventory.ProductID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error updating inventory: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public static Inventory? GetInventoryByProductId(DatabaseConnector dbConnector, int productId) 
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "SELECT * FROM Inventory WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Inventory
                            {
                                InventoryID = (int)reader["InventoryID"],
                                ProductID = (int)reader["ProductID"],
                                QuantityInStock = (int)reader["QuantityInStock"],
                                LastStockUpdate = (DateTime)reader["LastStockUpdate"],
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException("Error retrieving inventory: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
            return null;
        }
    }
}
