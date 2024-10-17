using System;
using System.Data.SqlClient;
using DatabaseConnection;
using TechShopApp.Exceptions;

namespace TechShopApp.Managers
{
    public class PaymentManager
    {
        public void RecordPayment(DatabaseConnector dbConnector, int orderId, decimal amount, string paymentMethod)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "INSERT INTO Payments (OrderID, Amount, PaymentMethod, PaymentDate, PaymentStatus) VALUES (@OrderID, @Amount, @PaymentMethod, @PaymentDate, @PaymentStatus)";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PaymentStatus", "Completed");
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Payment recorded successfully for Order ID: " + orderId);
            }
            catch (SqlException ex)
            {
                throw new PaymentFailedException("Failed to record payment: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public void UpdatePaymentStatus(DatabaseConnector dbConnector, int paymentId, string newStatus)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "UPDATE Payments SET PaymentStatus = @PaymentStatus WHERE PaymentID = @PaymentID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@PaymentStatus", newStatus);
                    cmd.Parameters.AddWithValue("@PaymentID", paymentId);
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Payment status updated successfully for Payment ID: " + paymentId);
            }
            catch (SqlException ex)
            {
                throw new PaymentFailedException("Failed to update payment status: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public void HandlePaymentFailure(DatabaseConnector dbConnector, int orderId, string failureReason)
        {
            try
            {
                dbConnector.OpenConnection();
                string query = "INSERT INTO PaymentFailures (OrderID, FailureReason, FailureDate) VALUES (@OrderID, @FailureReason, @FailureDate)";
                using (SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    cmd.Parameters.AddWithValue("@FailureReason", failureReason);
                    cmd.Parameters.AddWithValue("@FailureDate", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Payment failure recorded for Order ID: " + orderId);
            }
            catch (SqlException ex)
            {
                throw new PaymentFailedException("Failed to record payment failure: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }
    }
}
