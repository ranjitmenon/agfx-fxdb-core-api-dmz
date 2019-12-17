using System;

namespace Argentex.Core.Service.Exceptions
{
    public class FixQuoteException : Exception
    {
        public FixQuoteException() : base() { }

        public FixQuoteException(string message) : base(message) { }

        public FixQuoteException(string message, Exception inner) : base(message, inner) { }
    }
}
