using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EDMats.Controls
{
    public partial class ProcessRing : Viewbox
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color),
            typeof(Color),
            typeof(ProcessRing)
        );

        public ProcessRing()
            => InitializeComponent();

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
    }
}