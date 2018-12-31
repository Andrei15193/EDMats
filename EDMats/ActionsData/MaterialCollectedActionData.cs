using System;
using EDMats.Services;

namespace EDMats.ActionsData
{
    public class MaterialCollectedActionData : NotificationActionData
    {
        public MaterialCollectedActionData(DateTime latestUpdate, MaterialQuantity collectedMaterial)
        {
            LatestUpdate = latestUpdate;
            CollectedMaterial = collectedMaterial;
        }

        public DateTime LatestUpdate { get; }

        public MaterialQuantity CollectedMaterial { get; }
    }
}