using System.Windows;
using System.Windows.Controls;
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

                default:
                    Child = null;
                    break;
            }
        }
    }
}