using System;
using TechShopApp.Models;
using DatabaseConnection;
using TechShopApp.Exceptions;

namespace TechShopApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=LAPTOP-E8Q2MF96\\SQLEXPRESS;Database=TechShopDB;Trusted_Connection=True;";
            DatabaseConnector dbConnector = new DatabaseConnector(connectionString);

            // Example: Customer Registration
            try
            {
                Customer newCustomer = new Customer
                {
                    FirstName = "kiran",
                    LastName = "kumar",
                    Email = "dheeraj@gmail.com",
                    Phone = "123444789",
                    Address = "43356 El, Othertown, USA"
                };
                Customer.RegisterCustomer(dbConnector, newCustomer);
                Console.WriteLine("Customer registered successfully.");
            }
            catch (TechShopApp.Exceptions.InvalidDataException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            // Example: Adding a Product
            try
            {
                Product newProduct = new Product
                {
                    ProductName = "tubelights",
                    Description = "low voltage workable",
                    Price = 1280.00M
                };
                Product.AddProduct(dbConnector, newProduct);
                Console.WriteLine("Product added successfully.");
            }
            catch (TechShopApp.Exceptions.InvalidDataException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
