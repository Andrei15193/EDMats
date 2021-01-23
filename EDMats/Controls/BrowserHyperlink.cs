using System.Diagnostics;
using System.Windows.Documents;

namespace EDMats.Controls
{
    public class BrowserHyperlink : Hyperlink
    {
        public BrowserHyperlink()
        {
        }

        public BrowserHyperlink(Inline childInline)
            : base(childInline)
        {
        }

        public BrowserHyperlink(Inline childInline, TextPointer insertionPosition)
            : base(childInline, insertionPosition)
        {
        }

        public BrowserHyperlink(TextPointer start, TextPointer end)
            : base(start, end)
        {
        }

        protected override void OnClick()
        {
            Process.Start(new ProcessStartInfo(NavigateUri.AbsoluteUri));
        }
    }
}