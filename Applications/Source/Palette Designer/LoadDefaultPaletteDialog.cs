#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 */
#endregion

using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;
using System.Linq;

namespace PaletteDesigner
{
    public partial class LoadDefaultPaletteDialog : KryptonForm
    {
        public LoadDefaultPaletteDialog()
        {
            InitializeComponent();

            // Populate the combo box with available theme names without changing the global palette.
            kcmbThemes.Items.AddRange(PaletteModeStrings.SupportedThemesMap
                .Where(kvp => kvp.Value != PaletteMode.Custom)
                .Select(kvp => kvp.Key)
                .ToArray());

            if (kcmbThemes.Items.Count > 0)
            {
                kcmbThemes.SelectedIndex = 0;
            }
        }

        public string SelectedThemeName => kcmbThemes.GetItemText(kcmbThemes.SelectedItem);

        /// <summary>
        /// Gets the selected palette mode corresponding to the chosen theme.
        /// </summary>
        public PaletteMode SelectedPaletteMode => ThemeManager.GetThemeManagerMode(SelectedThemeName);
    }
}