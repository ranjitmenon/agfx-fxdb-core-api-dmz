using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class NavMenuItem
    {
        public int Id { get; set; }
        public int NavMenuSectionId { get; set; }
        public int? AuthPermissionId { get; set; }
        public string DisplayText { get; set; }
        public string NavigateUrl { get; set; }
        public int? DisplayOrder { get; set; }

        public AuthPermission AuthPermission { get; set; }
        public NavMenuSection NavMenuSection { get; set; }
    }
}
