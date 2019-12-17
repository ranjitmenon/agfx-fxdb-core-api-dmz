using System;

namespace Argentex.Core.Service.Exceptions
{
    public class ClientCompanyNotFoundException : Exception
    {
        public ClientCompanyNotFoundException() : base() { }

        public ClientCompanyNotFoundException(string message) : base(message) { }

        public ClientCompanyNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
