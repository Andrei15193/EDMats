using System;
using System.Linq;
using EDMats.Models.Materials;

namespace EDMats.Journals.Entries
{
    public class MaterialCollectedJournalEntry : JournalEntry
    {
        private readonly MaterialQuantity _collectedMaterialQuantity;

        public MaterialCollectedJournalEntry(DateTime timestamp, MaterialQuantity collectedMaterialQuantity)
            : base(timestamp)
            => _collectedMaterialQuantity = collectedMaterialQuantity;

        public override void Populate(CommanderInfo commanderInfo)
        {
            var materialQuantity = commanderInfo.Materials.FirstOrDefault(materialQuantity => materialQuantity.Material == _collectedMaterialQuantity.Material);
            if (materialQuantity is null)
                commanderInfo.Materials.Add(_collectedMaterialQuantity);
            else
            {
                commanderInfo.Materials.Remove(materialQuantity);
                commanderInfo.Materials.Add(new MaterialQuantity(_collectedMaterialQuantity.Material, _collectedMaterialQuantity.Amount + materialQuantity.Amount));
            }
        }
    }
}