using System.Collections.Generic;
using Newtonsoft.Json;

namespace EDMats.Storage
{
    public class StorageProfile
    {
        public StorageProfile(string name)
            => Name = name;

        [JsonIgnore]
        public string Name { get; }

        public StorageCommander Commander { get; } = new StorageCommander();

        public List<StorageBlueprint> Blueprints { get; } = new List<StorageBlueprint>();
    }
}