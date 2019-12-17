﻿using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyIndustrySector
    {
        public ClientCompanyIndustrySector()
        {
            ClientCompany = new HashSet<ClientCompany>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<ClientCompany> ClientCompany { get; set; }
    }
}
