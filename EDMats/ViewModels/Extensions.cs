using System;
using System.Collections.Generic;
using System.Linq;

namespace EDMats.ViewModels
{
    internal static class Extensions
    {
        internal static IEnumerable<StoredMaterial> ApplyFilter(this IEnumerable<StoredMaterial> storedMaterials, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return storedMaterials;

            var searchItems = filter.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            return storedMaterials
                .Where(storedMaterial => searchItems.Any(searchItem => storedMaterial.Name.IndexOf(searchItem, StringComparison.OrdinalIgnoreCase) >= 0));
        }
    }
}