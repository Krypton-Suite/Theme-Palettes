#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner;

public partial class SettingsControlPanel : KryptonForm
{
    #region Instance Fields

    private readonly SettingsManager _settingsManager = new();
    private readonly KryptonManager _manager;
    // Pending changes until OK
    private bool _newAskForConfirmation;
    private bool _newStartMaximised;
    private int _newThemeIndex;
    private bool _newUpgradeOnImport;
    private PaletteMode _originalPaletteMode;
    private bool _committed;

    #endregion

    /// <summary>
    /// Initializes a new instance of the SettingsControlPanel using existing global KryptonManager.
    /// </summary>
    public SettingsControlPanel(KryptonManager manager)
    {
        InitializeComponent();

        _manager = manager;
        _originalPaletteMode = _manager.GlobalPaletteMode;
        _committed = false;
        FormClosing += SettingsControlPanel_FormClosing;

        kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;
        kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;
        kbtnReset.Text = KryptonManager.Strings.CustomStrings.Reset;
    }

    private void SettingsControlPanel_Load(object sender, EventArgs e)
    {
        // Load persisted settings into controls and pending variables
        _newAskForConfirmation = _settingsManager.GetAskForSaveConfirmation();
        kchkAskForConfirmation.Checked = _newAskForConfirmation;

        _newStartMaximised = _settingsManager.GetMaximised();
        kchkStartMaximised.Checked = _newStartMaximised;

        _newThemeIndex = _settingsManager.GetThemeSelectedIndex();
        ktcmbTheme.SelectedIndex = _newThemeIndex;

        _newUpgradeOnImport = _settingsManager.GetUpgradeOnImport();
        kchkUpgradePalette.Checked = _newUpgradeOnImport;

        // Ensure dialog theme is up to date
        PaletteMode = _settingsManager.GetTheme();
        EnableResetButton(false);

        // Restore window position (fixed size dialog)
        var savedBounds = _settingsManager.GetSettingsControlPanelBounds();
        if (!savedBounds.IsEmpty)
        {
            var adjusted = WindowBoundsHelper.AdjustBoundsToVisibleScreens(savedBounds, preserveSize: true);
            StartPosition = FormStartPosition.Manual;
            Location = adjusted.Location;
        }
    }

    private void kchkStartMaximised_CheckedChanged(object sender, EventArgs e)
    {
        // Buffer change
        _newStartMaximised = kchkStartMaximised.Checked;
        EnableResetButton(true);
    }

    private void kchkUpgradePalette_CheckedChanged(object sender, EventArgs e)
    {
        // Buffer change
        _newUpgradeOnImport = kchkUpgradePalette.Checked;
        EnableResetButton(true);
    }

    private void ktcmbTheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Buffer change of selected theme
        _newThemeIndex = ktcmbTheme.SelectedIndex;
        EnableResetButton(true);
    }

    private void kchkAskForConfirmation_CheckedChanged(object sender, EventArgs e)
    {
        // Buffer change
        _newAskForConfirmation = kchkAskForConfirmation.Checked;
        EnableResetButton(true);
    }

    private void kbtnReset_Click(object sender, EventArgs e)
    {
        var ask = _settingsManager.GetAskForSaveConfirmation();

        if (ask && TopMost)
        {
            bool oldTopMost = TopMost;
            try
            {
                TopMost = false;
                _settingsManager.ResetSettings(this, true);
            }
            finally
            {
                TopMost = oldTopMost;
            }
        }
        else
        {
            _settingsManager.ResetSettings(this, ask);
        }

        // Reload UI and pending values after reset
        _newAskForConfirmation = _settingsManager.GetAskForSaveConfirmation();
        kchkAskForConfirmation.Checked = _newAskForConfirmation;
        _newStartMaximised = _settingsManager.GetMaximised();
        kchkStartMaximised.Checked = _newStartMaximised;
        _newThemeIndex = _settingsManager.GetThemeSelectedIndex();
        ktcmbTheme.SelectedIndex = _newThemeIndex;
        _newUpgradeOnImport = _settingsManager.GetUpgradeOnImport();
        kchkUpgradePalette.Checked = _newUpgradeOnImport;

        EnableResetButton(true);
    }

    private void kbtnOk_Click(object sender, EventArgs e)
    {
        // Commit buffered changes
        _settingsManager.SetAskForSaveConfirmation(_newAskForConfirmation);
        _committed = true;
        _settingsManager.SetMaximised(_newStartMaximised);
        _settingsManager.SetUpgradeOnImport(_newUpgradeOnImport);
        // Apply theme selection
        var selectedName = ktcmbTheme.GetItemText(ktcmbTheme.SelectedItem);
        if (selectedName != null)
        {
            var selectedMode = ThemeManager.GetThemeManagerMode(selectedName);
            _settingsManager.SetTheme(selectedMode);
        }

        _settingsManager.SetThemeSelectedIndex(_newThemeIndex);

        var ask = _newAskForConfirmation;

        if (ask && TopMost)
        {
            bool oldTopMost = TopMost;
            try
            {
                TopMost = false;
                _settingsManager.SaveSettings(this, true);
            }
            finally
            {
                TopMost = oldTopMost;
            }
        }
        else
        {
            _settingsManager.SaveSettings(this, ask);
        }

        // Apply the new global palette
        ThemeManager.ApplyTheme(_settingsManager.GetTheme(), new KryptonManager());
        Close();
    }

    private void KbtnCancel_Click(object sender, EventArgs e) => Close();

    private void SettingsControlPanel_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (!_committed)
        {
            _manager.GlobalPaletteMode = _originalPaletteMode;
        }

        // Save window bounds
        _settingsManager.SetSettingsControlPanelBounds(new Rectangle(Location, Size));
        _settingsManager.SaveSettings(this, false);
    }

    private void EnableResetButton(bool enable) => kbtnReset.Enabled = enable;
}