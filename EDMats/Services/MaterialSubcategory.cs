using System.Collections.Generic;

namespace EDMats.Services
{
    public class MaterialSubcategory
    {
        internal MaterialSubcategory(string name, MaterialCategory category, IReadOnlyList<Material> materials)
        {
            Name = name;
            Category = category;
            Materials = materials;
        }

        public string Name { get; }

        public MaterialCategory Category { get; }

        public IReadOnlyList<Material> Materials { get; }
    }
}