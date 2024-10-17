using System;
namespace TechShopApp.Exceptions
{
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException(string message) : base(message) { }
    }
}
