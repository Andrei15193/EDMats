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
                => int.TryParse(textBox.Text.Substring(0, textBox.SelectionStart) + e.Text + textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength), out var number) && 0 <= number && number <= 50;
        }
    }
}