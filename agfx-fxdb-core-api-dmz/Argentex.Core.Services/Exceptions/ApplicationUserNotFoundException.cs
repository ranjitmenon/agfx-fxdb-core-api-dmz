using System;

namespace Argentex.Core.Service.Exceptions
{
    public class ApplicationUserNotFoundException : Exception
    {
        public ApplicationUserNotFoundException() : base() { }

        public ApplicationUserNotFoundException(string message) : base(message) { }

        public ApplicationUserNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
