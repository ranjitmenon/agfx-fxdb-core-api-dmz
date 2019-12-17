using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class IntroducingBroker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int UpdateAuthUserId { get; set; }
    }
}
