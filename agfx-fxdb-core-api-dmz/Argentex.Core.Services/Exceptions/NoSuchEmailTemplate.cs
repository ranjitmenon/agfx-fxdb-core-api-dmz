using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Argentex.Core.Service.Exceptions
{
    public class NoSuchEmailTemplate : Exception
    {
        public NoSuchEmailTemplate()
        {
        }

        public NoSuchEmailTemplate(string message) : base(message)
        {
        }

        public NoSuchEmailTemplate(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchEmailTemplate(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
