using System;
using System.Collections.Generic;
using System.Linq;
using EDMats.Models.Materials;

namespace EDMats.Journals.Entries
{
    public class MaterialsJournalEntry : JournalEntry
    {
        private readonly IReadOnlyCollection<MaterialQuantity> _raw;
        private readonly IReadOnlyCollection<MaterialQuantity> _manufactured;
        private readonly IReadOnlyCollection<MaterialQuantity> _encoded;

        public MaterialsJournalEntry(DateTime timestamp, IReadOnlyCollection<MaterialQuantity> raw, IReadOnlyCollection<MaterialQuantity> manufactured, IReadOnlyCollection<MaterialQuantity> encoded)
            : base(timestamp)
            => (_raw, _manufactured, _encoded) = (raw, manufactured, encoded);

        public override void Populate(CommanderInfo commanderInfo)
        {
            foreach (var inventoryMaterialQuantity in _raw.Concat(_manufactured).Concat(_encoded))
            {
                var materialQuantity = commanderInfo.Materials.FirstOrDefault(materialQuantity => materialQuantity.Material == inventoryMaterialQuantity.Material);
                if (materialQuantity is null)
                    commanderInfo.Materials.Add(inventoryMaterialQuantity);
                else
                {
                    commanderInfo.Materials.Remove(materialQuantity);
                    commanderInfo.Materials.Add(new MaterialQuantity(inventoryMaterialQuantity.Material, inventoryMaterialQuantity.Amount + materialQuantity.Amount));
                }
            }
        }
    }
}