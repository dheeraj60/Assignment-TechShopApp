using System;
namespace TechShopApp.Exceptions
{
    public class IncompleteOrderException : Exception
    {
        public IncompleteOrderException(string message) : base(message) { }
    }
}