using System.Windows.Controls;
using System.Windows.Input;

namespace EDMats.Controls
{
    public partial class BlueprintRequirementsEditView : UserControl
    {
        public BlueprintRequirementsEditView()
            => InitializeComponent();

        private void _PreviewRepetitionInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            e.Handled = !_IsValidRepetitionNumber();

            bool _IsValidRepetitionNumber()
                => int.TryParse(textBox.Text + e.Text, out var number) && 0 <= number && number <= 50;
        }
    }
}