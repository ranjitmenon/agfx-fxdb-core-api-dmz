using System;

namespace Argentex.Core.Service.Exceptions
{
    public class CurrencyNotFoundException : Exception
    {
        public CurrencyNotFoundException() : base() { }

        public CurrencyNotFoundException(string message) : base(message) { }

        public CurrencyNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
