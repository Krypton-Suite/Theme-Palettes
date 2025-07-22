#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner;

public partial class LoadDefaultPaletteDialog : KryptonForm
{
    public LoadDefaultPaletteDialog()
    {
        InitializeComponent();

        // Populate the combo box with available theme names without changing the global palette.
        if (kcmbThemes != null)
        {
            kcmbThemes.Items.AddRange([
                .. PaletteModeStrings.SupportedThemesMap
                    .Where(kvp => kvp.Value != PaletteMode.Custom)
                    .Select(kvp => kvp.Key)
            ]);

            if (kcmbThemes.Items.Count > 0)
            {
                kcmbThemes.SelectedIndex = 0;
            }
        }
    }

    public string SelectedThemeName
    {
        get
        {
            // Fallback: try to map Microsoft365Silver palette mode to its display name
            var silverName = PaletteModeStrings.SupportedThemesMap
                .FirstOrDefault(kvp => kvp.Value == PaletteMode.Microsoft365Silver).Key;

            if (kcmbThemes?.SelectedItem != null)
            {
                return kcmbThemes!.GetItemText(kcmbThemes.SelectedItem) ?? silverName;
            }

            return silverName ?? "Custom";
        }
    }

    /// <summary>
    /// Gets the selected palette mode corresponding to the chosen theme.
    /// </summary>
    public PaletteMode SelectedPaletteMode => ThemeManager.GetThemeManagerMode(SelectedThemeName);
}