using System;

namespace Argentex.Core.Service.Exceptions
{
    public class PaymentNotFoundException : Exception
    {
        public PaymentNotFoundException() : base() { }

        public PaymentNotFoundException(string message) : base(message) { }

        public PaymentNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
