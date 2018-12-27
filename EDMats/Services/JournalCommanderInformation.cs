using System;
using System.Collections.Generic;

namespace EDMats.Services
{
    public class JournalCommanderInformation
    {
        public DateTime LatestUpdate { get; set; }

        public IReadOnlyCollection<MaterialQuantity> Materials { get; set; }
    }
}