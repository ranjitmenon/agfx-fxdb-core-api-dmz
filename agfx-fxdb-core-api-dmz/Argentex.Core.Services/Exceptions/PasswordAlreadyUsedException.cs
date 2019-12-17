using System;

namespace Argentex.Core.Service.Exceptions
{
    public class PasswordAlreadyUsedException : Exception
    {
        public PasswordAlreadyUsedException() : base() { }

        public PasswordAlreadyUsedException(string message) : base(message) { }

        public PasswordAlreadyUsedException(string message, Exception inner) : base(message, inner) { }
    }
}
