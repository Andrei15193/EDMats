using System;
using System.Collections.Generic;
using EDMats.Data.Materials;

namespace EDMats.Services
{
    public class JournalCommanderInformation
    {
        public DateTime LatestUpdate { get; set; }

        public IReadOnlyCollection<MaterialQuantity> Materials { get; set; }
    }
}