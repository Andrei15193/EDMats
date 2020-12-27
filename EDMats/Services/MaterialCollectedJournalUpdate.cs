using EDMats.Data.Materials;

namespace EDMats.Services
{
    public class MaterialCollectedJournalUpdate : JournalUpdate
    {
        public MaterialQuantity CollectedMaterial { get; set; }
    }
}