namespace EDMats.Storage
{
    public interface IProfileStorageHandler
    {
        StorageProfile LoadProfile(string profileName);
        void SaveProfile(StorageProfile profile);
    }
}