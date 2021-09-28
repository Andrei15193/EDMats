﻿using System.Collections.Generic;

namespace EDMats.Models.Materials
{
    public sealed class MaterialType
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