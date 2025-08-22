#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Pages;

public partial class GridPage : UserControl
{
    public GridPage()
    {
        InitializeComponent();

        // Consistent left-column theming
        kryptonNavigatorDesignGrids.Panel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
        borderDesignGrids.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;

        // Seed labels to match MainForm behavior
        labelGridDisabled.Values.Text = "Disabled";
        labelGridNormal.Values.Text = "Normal";

        // Fixed states
        dataGridViewDisabled.Enabled = false;

        // Populate the sample data set
        dataTable1.Rows.Add(@"One", @"Two", @"Three");
        dataTable1.Rows.Add(@"Uno", @"Dos", @"Tres");
        dataTable1.Rows.Add(@"Un", @"Deux", @"Trios");
        dataTable1.Rows.Add(@"Eins", @"Zwei", @"Drei");
    }

    // Public API for palette application
    public void ApplyPalette(Krypton.Toolkit.KryptonCustomPaletteBase palette)
    {
        kryptonPanel1.Palette = palette;
        dataGridViewDisabled.Palette = palette;
        dataGridViewNormal.Palette = palette;
    }

    // Internal update when the left navigator selection changes
    private void KryptonNavigatorDesignGrids_SelectedPageChanged(object? sender, EventArgs e)
    {
        if (kryptonNavigatorDesignGrids.SelectedPage == null)
        {
            return;
        }

        // Update the design page text with the selected style information
        pageTitleLabel.Values.Text = kryptonNavigatorDesignGrids.SelectedPage.Text;
        pageDescLabel.Values.Text = kryptonNavigatorDesignGrids.SelectedPage.TextDescription;

        // Work out the navigator mode required
        Krypton.Toolkit.DataGridViewStyle gridStyle = kryptonNavigatorDesignGrids.SelectedIndex switch
        {
            0 => Krypton.Toolkit.DataGridViewStyle.List,
            1 => Krypton.Toolkit.DataGridViewStyle.Sheet,
            2 => Krypton.Toolkit.DataGridViewStyle.Custom1,
            _ => Krypton.Toolkit.DataGridViewStyle.List
        };

        dataGridViewDisabled.GridStyles.Style = gridStyle;
        dataGridViewNormal.GridStyles.Style = gridStyle;
    }

    // Optional exposure if MainForm ever wants to read selected state
    public Krypton.Navigator.KryptonNavigator Navigator => kryptonNavigatorDesignGrids;
}