using System.Collections.Generic;

namespace EDMats.Models.Materials
{
    public sealed record MaterialType(string Name, IReadOnlyList<MaterialCategory> Categories);
}