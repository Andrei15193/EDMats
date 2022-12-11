using EDMats.Journals.Entries;
using EDMats.Models.Materials;
using System;
using System.Linq;

namespace EDMats.Journals
{
    public class CommanderInfoJournalEntryVisitor : JournalEntryVisitor
    {
        public CommanderInfoJournalEntryVisitor()
            : this(new CommanderInfo())
        {
        }

        public CommanderInfoJournalEntryVisitor(CommanderInfo commanderInfo)
            => CommanderInfo = commanderInfo;

        public CommanderInfo CommanderInfo { get; }

        protected internal override void Visit(MaterialsJournalEntry materialsJournalEntry)
        {
            CommanderInfo.Materials.Clear();
            foreach (var inventoryMaterialQuantity in materialsJournalEntry.Raw.Concat(materialsJournalEntry.Manufactured).Concat(materialsJournalEntry.Encoded))
                _AddMaterial(inventoryMaterialQuantity);
        }

        protected internal override void Visit(MaterialCollectedJournalEntry materialCollectedJournalEntry)
            => _AddMaterial(materialCollectedJournalEntry.CollectedMaterialQuantity);

        protected internal override void Visit(MissionCompletedJournalEntry missionCompletedJournalEntry)
        {
            foreach (var materialReward in missionCompletedJournalEntry.MaterialsReward)
                _AddMaterial(materialReward);
        }

        protected internal override void Visit(MaterialTradeJournalEntry materialTradeJournalEntry)
        {
            _SubtractMaterial(materialTradeJournalEntry.Paid);
            _AddMaterial(materialTradeJournalEntry.Received);
        }

        private void _AddMaterial(MaterialQuantity materialQuantityToAdd)
        {
            var commanderMaterialQuantity = CommanderInfo.Materials.FirstOrDefault(materialQuantity => materialQuantity.Material == materialQuantityToAdd.Material);
            if (commanderMaterialQuantity is null)
                CommanderInfo.Materials.Add(materialQuantityToAdd);
            else
            {
                CommanderInfo.Materials.Remove(commanderMaterialQuantity);

                var newMaterialQuantity = commanderMaterialQuantity.Amount + materialQuantityToAdd.Amount;
                CommanderInfo.Materials.Add(new MaterialQuantity(materialQuantityToAdd.Material, newMaterialQuantity));
            }
        }

        private void _SubtractMaterial(MaterialQuantity materialQuantityToSubtract)
        {
            var commanderMaterialQuantity = CommanderInfo.Materials.FirstOrDefault(materialQuantity => materialQuantity.Material == materialQuantityToSubtract.Material);
            if (commanderMaterialQuantity is null)
                throw new InvalidOperationException("Cannot subtract material that the commander does not have.");
            else
            {
                CommanderInfo.Materials.Remove(commanderMaterialQuantity);

                var newMaterialQuantity = commanderMaterialQuantity.Amount - materialQuantityToSubtract.Amount;
                if (newMaterialQuantity > 0)
                    CommanderInfo.Materials.Add(new MaterialQuantity(materialQuantityToSubtract.Material, newMaterialQuantity));
            }
        }
    }
}