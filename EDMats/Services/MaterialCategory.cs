using System.Collections.Generic;

namespace EDMats.Services
{
    public class MaterialCategory
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