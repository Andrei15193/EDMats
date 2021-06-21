using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EDMats.Storage
{
    public class ProfileStorageHandler : IProfileStorageHandler
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            Culture = CultureInfo.InvariantCulture,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
        private readonly IStorageHandler _storageHandler;

        public ProfileStorageHandler(IStorageHandler storageHandler)
            => _storageHandler = storageHandler;

        public StorageProfile LoadProfile(string profileName)
        {
            var profileNameToLowerCase = profileName.ToLowerInvariant();
            using (var profileReader = _storageHandler.GetTextReader(profileNameToLowerCase))
            {
                var profile = new StorageProfile(profileNameToLowerCase);
                var profileJson = profileReader.ReadToEnd();
                JsonConvert.PopulateObject(string.IsNullOrWhiteSpace(profileJson) ? "{}" : profileJson, profile, _jsonSerializerSettings);
                return profile;
            }
        }

        public void SaveProfile(StorageProfile profile)
        {
            using (var profileWriter = _storageHandler.GetTextWriter(profile.Name))
                profileWriter.Write(JsonConvert.SerializeObject(profile, _jsonSerializerSettings));
        }
    }
}