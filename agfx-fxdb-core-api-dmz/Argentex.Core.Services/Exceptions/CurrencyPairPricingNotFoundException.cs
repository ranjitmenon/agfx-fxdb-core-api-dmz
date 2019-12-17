using System;

namespace Argentex.Core.Service.Exceptions
{
    public class CurrencyPairPricingNotFoundException : Exception
    {
        public CurrencyPairPricingNotFoundException() : base() { }

        public CurrencyPairPricingNotFoundException(string message) : base(message) { }

        public CurrencyPairPricingNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
