using System.Collections.Generic;

namespace EDMats.Models.Materials
{
    public sealed record MaterialCategory(string Name, MaterialType Type, IReadOnlyList<Material> Materials);
}