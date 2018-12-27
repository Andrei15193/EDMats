using System;
using System.Collections.Generic;

namespace EDMats.Services
{
    public class JournalCommanderInformation
    {
        public DateTime LatestUpdate { get; set; }

        public IReadOnlyDictionary<Material, int> Materials { get; set; }
    }
}