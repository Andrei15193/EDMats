using System.Windows;
using System.Windows.Controls;
using EDMats.Models.Materials;

namespace EDMats.Pages.Controls
{
    public partial class MaterialView : TextBlock
    {
        public static readonly DependencyProperty MaterialProperty = DependencyProperty.Register(
            nameof(Material),
            typeof(Material),
            typeof(MaterialView)
        );

        public MaterialView()
            => InitializeComponent();

        public Material Material
        {
            get => (Material)GetValue(MaterialProperty);
            set => SetValue(MaterialProperty, value);
        }
    }
}