using System;
using System.Data.SqlClient;
using DatabaseConnection; 

namespace TechShopApp.Models
{
    public class Customer
    {
        public int CustomerID { get; private set; }

        public string FirstName { get; set; } 
        public string LastName { get; set; }  
        public string Email { get; set; }    
        public string? Phone { get; set; }   
        public string? Address { get; set; } 

        public static void RegisterCustomer(DatabaseConnector dbConnector, Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName) ||
                string.IsNullOrWhiteSpace(customer.LastName) ||
                string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new InvalidDataException("First Name, Last Name, and Email are required fields.");
            }

            try
            {
                dbConnector.OpenConnection();
                string query = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) " +
                               "VALUES (@FirstName, @LastName, @Email, @Phone, @Address)";

                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone", (object)customer.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object)customer.Address ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidDataException($"Error registering customer: {ex.Message}. " +
                    $"Inner Exception: {ex.InnerException?.Message}");
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

    }
}
