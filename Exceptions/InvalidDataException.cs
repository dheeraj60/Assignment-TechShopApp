using System;
namespace TechShopApp.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
    }
}