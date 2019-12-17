using System;

namespace Argentex.Core.Service.Exceptions
{
    public class ClientCompanyOpiNotFoundException : Exception
    {
        public ClientCompanyOpiNotFoundException() : base() { }

        public ClientCompanyOpiNotFoundException(string message) : base(message) { }

        public ClientCompanyOpiNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
