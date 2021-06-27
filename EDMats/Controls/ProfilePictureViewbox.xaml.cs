using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EDMats.Data;

namespace EDMats.Controls
{
    public partial class ProfilePictureViewbox : Viewbox
    {
        private static readonly IReadOnlyDictionary<ProfilePicture, string> _drawingResourceKeys = new Dictionary<ProfilePicture, string>()
        {
            { ProfilePicture.Adder, "Drawings_Adder" },
            { ProfilePicture.AllianceChallenger, "Drawings_AllianceChallenger" },
            { ProfilePicture.AllianceChieftain, "Drawings_AllianceChieftain" },
            { ProfilePicture.AllianceCrusader, "Drawings_AllianceCrusader" },
            { ProfilePicture.Anaconda, "Drawings_Anaconda" },
            { ProfilePicture.AspExplorer, "Drawings_AspExplorer" },
            { ProfilePicture.AspScout, "Drawings_AspScout" },
            { ProfilePicture.Beluga, "Drawings_Beluga" },
            { ProfilePicture.CobraMk3, "Drawings_CobraMk3" },
            { ProfilePicture.DiamondbackExplorer, "Drawings_DiamondbackExplorer" },
            { ProfilePicture.DiamondbackScout, "Drawings_DiamondbackScout" },
            { ProfilePicture.Dolphin, "Drawings_Dolphin" },
            { ProfilePicture.EagleMk2, "Drawings_EagleMk2" },
            { ProfilePicture.FederalAssaultShip, "Drawings_FederalAssaultShip" },
            { ProfilePicture.FederalCorvette, "Drawings_FederalCorvette" },
            { ProfilePicture.FederalDropship, "Drawings_FederalDropship" },
            { ProfilePicture.FederalGunship, "Drawings_FederalGunship" },
            { ProfilePicture.FerDeLance, "Drawings_FerDeLance" },
            { ProfilePicture.Hauler, "Drawings_Hauler" },
            { ProfilePicture.ImperialClipper, "Drawings_ImperialClipper" },
            { ProfilePicture.ImperialCourier, "Drawings_ImperialCourier" },
            { ProfilePicture.ImperialCutter, "Drawings_ImperialCutter" },
            { ProfilePicture.ImperialEagle, "Drawings_ImperialEagle" },
            { ProfilePicture.Keelback, "Drawings_Keelback" },
            { ProfilePicture.KraitMk2, "Drawings_KraitMk2" },
            { ProfilePicture.KraitPhantom, "Drawings_KraitPhantom" },
            { ProfilePicture.Mamba, "Drawings_Mamba" },
            { ProfilePicture.Orca, "Drawings_Orca" },
            { ProfilePicture.Python, "Drawings_Python" },
            { ProfilePicture.Type10, "Drawings_Type10" },
            { ProfilePicture.Type6, "Drawings_Type6" },
            { ProfilePicture.Type7, "Drawings_Type7" },
            { ProfilePicture.Type9, "Drawings_Type9" },
            { ProfilePicture.ViperMk3, "Drawings_ViperMk3" },
            { ProfilePicture.ViperMk4, "Drawings_ViperMk4" },
            { ProfilePicture.Vulture, "Drawings_Vulture" },
            { ProfilePicture.Sidewinder, "Drawings_Sidewinder" }
        };

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
            if (Picture == ProfilePicture.EliteDangerousLogo)
                Child = new Path
                {
                    Fill = (Brush)FindResource("GoldBrush"),
                    Data = (Geometry)FindResource("Elite_Dangerous_Logo")
                };
            else if (_drawingResourceKeys.TryGetValue(Picture, out var drawingResourceKey))
                Child = (UIElement)FindResource(drawingResourceKey);
            else
                Child = null;
        }
    }
}