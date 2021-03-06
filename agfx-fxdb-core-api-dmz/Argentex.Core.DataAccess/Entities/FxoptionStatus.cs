﻿using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxoptionStatus
    {
        public FxoptionStatus()
        {
            Fxoption = new HashSet<Fxoption>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Fxoption> Fxoption { get; set; }
    }
}
