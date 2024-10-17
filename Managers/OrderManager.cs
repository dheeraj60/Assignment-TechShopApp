using System;
using TechShopApp.Models;
using DatabaseConnection;

namespace TechShopApp.Managers
{
    public class OrderManager
    {
        public void PlaceOrder(DatabaseConnector dbConnector, Order order)
        {
            Order.PlaceOrder(dbConnector, order);
        }

        public void UpdateOrder(DatabaseConnector dbConnector, Order order)
        {
            Order.UpdateOrder(dbConnector, order);
        }

        public Order? GetOrderById(DatabaseConnector dbConnector, int orderId)
        {
            return Order.GetOrderById(dbConnector, orderId);
        }
    }
}
