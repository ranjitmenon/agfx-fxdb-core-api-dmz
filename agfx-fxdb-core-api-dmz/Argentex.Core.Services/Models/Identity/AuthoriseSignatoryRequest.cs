using System.Collections.Generic;

namespace Argentex.Core.Service.Models.Identity
{
    public class AuthoriseSignatoryRequest
    {
        public int ApproverAuthUserId { get; set; }
        public ICollection<int> UserIdsToAuthorise { get; set; }
    }
}
