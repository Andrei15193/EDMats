using System.Collections.Generic;

namespace EDMats.Services
{
    public class MaterialType
    {
        internal MaterialType(string name, IReadOnlyList<MaterialCategory> categories)
        {
            Name = name;
            Categories = categories;
        }

        public string Name { get; }

        public IReadOnlyList<MaterialCategory> Categories { get; }
    }
}