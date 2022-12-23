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
    public partial class Base : UserControl
    {
        public Base()
        {
            InitializeComponent();

        }

        public void ApplyPalette(KryptonCustomPaletteBase palette) => kryptonPanel1.Palette = palette;
    }
}
