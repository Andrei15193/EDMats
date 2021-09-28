using System.Collections.Generic;

namespace EDMats.Models.Materials
{
    public sealed class MaterialCategory
    {
        internal MaterialCategory(string name, MaterialType type, IReadOnlyList<Material> materials)
        {
            Name = name;
            Type = type;
            Materials = materials;
        }

        public string Name { get; }

        public MaterialType Type { get; }

        public IReadOnlyList<Material> Materials { get; }
    }
}