using System;
using System.Collections.Generic;

namespace Argentex.Core.Service.Models.Identity
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public ICollection<string> Roles { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int ClientCompanyId { get; internal set; }
        public int AuthUserId { get; internal set; }
        public DateTime PasswordLastChanged { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsSuccesfullLogin { get; set; }        
        public bool IsOnline { get; set; }
    }
}
