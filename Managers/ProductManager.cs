using System;
using TechShopApp.Models;
using DatabaseConnection;

namespace TechShopApp.Managers
{
    public class ProductManager
    {
        public void AddProduct(DatabaseConnector dbConnector, Product product)
        {
            Product.AddProduct(dbConnector, product);
        }
        public void UpdateProduct(DatabaseConnector dbConnector, Product product)
        {
            Product.UpdateProduct(dbConnector, product);
        }
        public Product GetProductById(DatabaseConnector dbConnector, int productId)
        {
            return Product.GetProductById(dbConnector, productId);
        }
    }
}