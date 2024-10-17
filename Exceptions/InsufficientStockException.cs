using System;
namespace TechShopApp.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string message) : base(message) { }
    }
}
