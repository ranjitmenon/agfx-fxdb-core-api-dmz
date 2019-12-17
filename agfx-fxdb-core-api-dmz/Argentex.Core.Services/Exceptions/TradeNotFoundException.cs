using System;

namespace Argentex.Core.Service.Exceptions
{
    public class TradeNotFoundException : Exception
    {
        public TradeNotFoundException() : base() { }

        public TradeNotFoundException(string message) : base(message) { }

        public TradeNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
