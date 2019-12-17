using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException()
        {
        }

        public IdentityException(string message) : base(message)
        {
        }

        public IdentityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IdentityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
