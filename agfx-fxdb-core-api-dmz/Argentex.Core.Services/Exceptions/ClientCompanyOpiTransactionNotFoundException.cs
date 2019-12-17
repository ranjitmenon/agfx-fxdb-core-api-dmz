using System;

namespace Argentex.Core.Service.Exceptions
{
    public class ClientCompanyOpiTransactionNotFoundException : Exception
    {
        public ClientCompanyOpiTransactionNotFoundException() : base() { }

        public ClientCompanyOpiTransactionNotFoundException(string message) : base(message) { }

        public ClientCompanyOpiTransactionNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
