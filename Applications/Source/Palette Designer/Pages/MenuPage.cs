#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

using System.Windows.Forms;

using Krypton.Toolkit;

namespace PaletteDesigner.Pages
{
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
            //VisualContextMenu = CreateContextMenu(kcmEverything, kcmEverything.Palette, kcmEverything.PaletteMode,
            //    _redirector, _redirectorImages,
            //    kcmEverything.Items, kcmEverything.Enabled, false);

            //// Need to know when the visual control is removed
            //VisualContextMenu.Disposed += OnContextMenuDisposed;

            //// Request the menu be shown immediately
            //VisualContextMenu.Show(screenRect, horz, vert, false, constrain);

            //// Override the horz, vert setting so that sub menus appear right and below
            //VisualContextMenu.ShowHorz = KryptonContextMenuPositionH.After;
            //VisualContextMenu.ShowVert = KryptonContextMenuPositionV.Top;

        }

        public void ApplyPalette(KryptonPalette palette)
        {
            kcmEverything.Palette = palette;
            kryptonPanel1.Palette = palette;
        }

        private void kryptonButton1_Click(object sender, System.EventArgs e) => ShowMenu(kryptonButton1, true);

        private void kryptonButton2_Click(object sender, System.EventArgs e) => ShowMenu(kryptonButton2, false);

        private void ShowMenu(Control c, bool enabled)
        {
            kcmEverything.Enabled = enabled;
            kcmEverything.Show(c);
        }

        private void kcmEverything_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
