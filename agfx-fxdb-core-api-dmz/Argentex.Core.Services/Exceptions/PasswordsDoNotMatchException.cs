using System;

namespace Argentex.Core.Service.Exceptions
{
    public class PasswordsDoNotMatchException : Exception
    {
        public PasswordsDoNotMatchException() : base() { }

        public PasswordsDoNotMatchException(string message) : base(message) { }

        public PasswordsDoNotMatchException(string message, Exception inner) : base(message, inner) { }
    }
}
