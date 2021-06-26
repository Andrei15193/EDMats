using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EDMats.Data;

namespace EDMats.Controls
{
    public partial class ProfilePictureViewbox : Viewbox
    {
        public ProfilePictureViewbox()
        {
            InitializeComponent();
            _SetProfilePicture();
        }

        public static readonly DependencyProperty PictureProperty = DependencyProperty.Register(
            nameof(Picture),
            typeof(ProfilePicture),
            typeof(ProfilePictureViewbox),
            new PropertyMetadata(ProfilePicture.Sidewinder, _ProfilePictureChanged)
        );

        private static void _ProfilePictureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((ProfilePictureViewbox)d)._SetProfilePicture();

        public ProfilePicture Picture
        {
            get => (ProfilePicture)GetValue(PictureProperty);
            set => SetValue(PictureProperty, value);
        }

        private void _SetProfilePicture()
        {
            switch (Picture)
            {
                case ProfilePicture.Sidewinder:
                    Child = (UIElement)Resources["Drawings_Sidewinder"];
                    break;

                case ProfilePicture.EliteDangerousLogo:
                    Child = new Path
                    {
                        Fill = (Brush)Resources["GoldBrush"],
                        Data = (Geometry)Resources["Elite_Dangerous_Logo"]
                    };
                    break;

                default:
                    Child = null;
                    break;
            }
        }
    }
}