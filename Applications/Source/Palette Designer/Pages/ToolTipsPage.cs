using System.Windows.Forms;
using Krypton.Toolkit;

namespace PaletteDesigner.Pages
{
    public partial class ToolTipsPage : UserControl
    {
        public ToolTipsPage()
        {
            InitializeComponent();
        }

        public void ApplyPalette(KryptonPalette palette)
        {
            kryptonPanel1.Palette = palette;

            kryptonButton1.Palette = palette;

            kryptonGroupBox1.Palette = palette;

            krbToolTip.Palette = palette;

            krbSuperTip.Palette = palette;

            krbKeyTip.Palette = palette;

            kchkShowImage.Palette = palette;
        }
    }
}
