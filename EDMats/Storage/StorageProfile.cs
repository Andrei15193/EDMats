using System;
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

        public SortedDictionary<string, StorageModule> Modules { get; } = new SortedDictionary<string, StorageModule>(StringComparer.Ordinal);
    }
}