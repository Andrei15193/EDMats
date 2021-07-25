using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EDMats.Data.Materials;

namespace EDMats.Controls
{
    public partial class MaterialGradeView : Viewbox
    {
        public static readonly DependencyProperty MaterialGradeProperty = DependencyProperty.Register(
            nameof(MaterialGrade),
            typeof(MaterialGrade),
            typeof(MaterialGradeView),
            new PropertyMetadata(MaterialGrade.VeryCommon)
        );

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            nameof(Fill),
            typeof(Brush),
            typeof(MaterialGradeView)
        );

        public MaterialGradeView()
            => InitializeComponent();

        public MaterialGrade MaterialGrade
        {
            get => (MaterialGrade)GetValue(MaterialGradeProperty);
            set => SetValue(MaterialGradeProperty, value);
        }

        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }
    }
}