using System;
namespace TechShopApp.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message) { }
    }
}
