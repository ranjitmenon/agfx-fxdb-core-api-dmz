using System;

namespace Argentex.Core.Service.Exceptions
{
    public class ClientCompanyContactNotFoundException : Exception
    {
        public ClientCompanyContactNotFoundException() : base() { }

        public ClientCompanyContactNotFoundException(string message) : base(message) { }

        public ClientCompanyContactNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
