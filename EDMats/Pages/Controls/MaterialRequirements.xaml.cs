using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EDMats.Models.Materials;

namespace EDMats.Pages.Controls
{
    public partial class MaterialRequirements : UserControl
    {
        public static readonly DependencyProperty RequirementsProperty = DependencyProperty.Register(
            nameof(Requirements),
            typeof(IEnumerable<MaterialQuantity>),
            typeof(MaterialRequirements)
        );

        public MaterialRequirements()
            => InitializeComponent();

        public IEnumerable<MaterialQuantity> Requirements
        {
            get => (IEnumerable<MaterialQuantity>)GetValue(RequirementsProperty);
            set => SetValue(RequirementsProperty, value);
        }
    }
}