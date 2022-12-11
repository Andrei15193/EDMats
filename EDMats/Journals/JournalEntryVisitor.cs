using EDMats.Journals.Entries;

namespace EDMats.Journals
{
    public abstract class JournalEntryVisitor
    {
        protected internal abstract void Visit(MaterialsJournalEntry materialsJournalEntry);

        protected internal abstract void Visit(MaterialCollectedJournalEntry materialCollectedJournalEntry);

        protected internal abstract void Visit(MissionCompletedJournalEntry missionCompletedJournalEntry);

        protected internal abstract void Visit(MaterialTradeJournalEntry materialTradeJournalEntry);
    }
}