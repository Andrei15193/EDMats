using System.Collections.Generic;

namespace EDMats.Services
{
    public class MaterialCategory
    {
        internal MaterialCategory(string name, IReadOnlyList<MaterialSubcategory> subcategories)
        {
            Name = name;
            Subcategories = subcategories;
        }

        public string Name { get; }

        public IReadOnlyList<MaterialSubcategory> Subcategories { get; }
    }
}