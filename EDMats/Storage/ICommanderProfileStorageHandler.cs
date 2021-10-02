using EDMats.Data;

namespace EDMats.Storage
{
    public interface ICommanderProfileStorageHandler
    {
        CommanderProfile Load();
        void Save(CommanderProfile commanderProfile);
    }
}