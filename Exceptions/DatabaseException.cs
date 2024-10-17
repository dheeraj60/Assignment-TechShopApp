using System;
namespace TechShopApp.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }
    }
}