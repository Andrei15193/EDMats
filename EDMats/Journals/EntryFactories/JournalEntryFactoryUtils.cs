using EDMats.Models.Materials;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace EDMats.Journals.EntryFactories
{
    internal static class JournalEntryFactoryUtils
    {
        public static string GetEventFrom(JObject journalEntryJson)
            => journalEntryJson.GetValue("event", StringComparison.OrdinalIgnoreCase).Value<string>();

        public static DateTime GetTimestampFrom(JObject journalEntryJson)
            => journalEntryJson.GetValue("timestamp", StringComparison.OrdinalIgnoreCase).Value<DateTime>();

        public static MaterialQuantity TryGetMaterialQuantityFrom(JObject materialQuantityJson)
            => TryGetMaterialQuantityFrom(materialQuantityJson, "Name");

        public static MaterialQuantity TryGetMaterialQuantityFrom(JObject materialQuantityJson, string materialNameProperty = "Name")
        {
            if (materialQuantityJson.TryGetValue(materialNameProperty, StringComparison.OrdinalIgnoreCase, out var nameToken)
                && materialQuantityJson.TryGetValue("Count", StringComparison.OrdinalIgnoreCase, out var countToken))
            {
                var materialQuantity = new MaterialQuantity(Material.FindById(nameToken.Value<string>()), countToken.Value<int>());

#warning Remove when all materials have been tested
                if (materialQuantityJson.TryGetValue($"{materialNameProperty}_Localised", StringComparison.OrdinalIgnoreCase, out var localisedName)
                    && materialQuantity.Material.Name != localisedName.Value<string>().Trim())
                    throw new InvalidDataException($"Expected '{materialQuantity.Material.Name}' material name, actual '{localisedName}' received.");

                return materialQuantity;
            }
            else
                return null;
        }
    }
}