#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Pages;

public partial class ControlsPage : UserControl
{
    public ControlsPage()
    {
        InitializeComponent();

        // Mirror the pattern used by other pages:
        // - Left navigator uses alternate panel background
        kryptonNavigatorDesignControls.Panel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
        borderDesignControls.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;

        // Seed labels like in MainForm
        labelControlsDisabled.Values.Text = "Disabled";
        labelControlsNormal.Values.Text = "Normal";

        // Fixed states for demo controls to match original behavior
        control1Disabled.SetFixedState(Krypton.Toolkit.PaletteState.Disabled);
        control1Normal.SetFixedState(Krypton.Toolkit.PaletteState.Normal);
    }

    // Public API consistent with other pages
    public void ApplyPalette(Krypton.Toolkit.KryptonCustomPaletteBase palette)
    {
        kryptonPanel1.Palette = palette;
    }

    // Internal update when the left navigator selection changes
    private void KryptonNavigatorDesignControls_SelectedPageChanged(object? sender, EventArgs e)
    {
        if (kryptonNavigatorDesignControls.SelectedPage == null)
        {
            return;
        }

        // Determine group styles based on selected index
        Krypton.Toolkit.PaletteBackStyle backStyle;
        Krypton.Toolkit.PaletteBorderStyle borderStyle;

        switch (kryptonNavigatorDesignControls.SelectedIndex)
        {
            default:
                backStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
                borderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlClient;
                break;
            case 1:
                backStyle = Krypton.Toolkit.PaletteBackStyle.ControlAlternate;
                borderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlAlternate;
                break;
            case 2:
                backStyle = Krypton.Toolkit.PaletteBackStyle.ControlGroupBox;
                borderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlGroupBox;
                break;
            case 3:
                backStyle = Krypton.Toolkit.PaletteBackStyle.ControlToolTip;
                borderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlToolTip;
                break;
            case 4:
                backStyle = Krypton.Toolkit.PaletteBackStyle.ControlRibbon;
                borderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlRibbon;
                break;
            case 5:
                backStyle = Krypton.Toolkit.PaletteBackStyle.ControlCustom1;
                borderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlCustom1;
                break;
        }

        // Apply styles to demo groups in this page
        control1Disabled.GroupBackStyle = backStyle;
        control1Disabled.GroupBorderStyle = borderStyle;
        control1Normal.GroupBackStyle = backStyle;
        control1Normal.GroupBorderStyle = borderStyle;
    }

    // Optional exposure if MainForm ever wants to read selected state
    public Krypton.Navigator.KryptonNavigator Navigator => kryptonNavigatorDesignControls;
}