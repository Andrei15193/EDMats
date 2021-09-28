using System.Collections.Generic;
using EDMats.Models.Materials;

namespace EDMats.Tests
{
    public class MaterialQuantityEqualityComparer : IEqualityComparer<MaterialQuantity>
    {
        public bool Equals(MaterialQuantity x, MaterialQuantity y)
            => new { x.Material, x.Amount }.Equals(new { y.Material, y.Amount });

        public int GetHashCode(MaterialQuantity obj)
            => new { obj.Material, obj.Amount }.GetHashCode();
    }
}