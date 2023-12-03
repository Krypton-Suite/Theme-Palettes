#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace PaletteDesigner
{
    public partial class SettingsControlPanel : KryptonForm
    {
        #region Instance Fields

        private readonly SettingsManager _settingsManager = new();

        #endregion

        public SettingsControlPanel()
        {
            InitializeComponent();

            kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;

            kbtnCancel.DialogResult = DialogResult.Cancel;

            kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

            kbtnOk.DialogResult = DialogResult.OK;

            kbtnReset.Text = KryptonManager.Strings.CustomStrings.Reset;

            AcceptButton = kbtnOk;

            CancelButton = kbtnCancel;
        }

        private void SettingsControlPanel_Load(object sender, EventArgs e)
        {
            kchkAskForConfirmation.Checked = _settingsManager.GetAskForSaveConfirmation();

            kchkStartMaximised.Checked = _settingsManager.GetMaximised();

            ktcmbTheme.SelectedIndex = _settingsManager.GetThemeSelectedIndex();

            kchkUpgradePalette.Checked = _settingsManager.GetUpgradeOnImport();

            EnableResetButton(false);
        }

        private void kchkStartMaximised_CheckedChanged(object sender, EventArgs e)
        {
            _settingsManager.SetMaximised(kchkStartMaximised.Checked);

            EnableResetButton(true);
        }

        private void kchkUpgradePalette_CheckedChanged(object sender, EventArgs e)
        {
            _settingsManager.SetUpgradeOnImport(kchkUpgradePalette.Checked);

            EnableResetButton(true);
        }

        private void ktcmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settingsManager.SetTheme(ThemeManager.GetPaletteMode(kmTheme));

            EnableResetButton(true);
        }

        private void kchkAskForConfirmation_CheckedChanged(object sender, EventArgs e)
        {
            _settingsManager.SetAskForSaveConfirmation(kchkAskForConfirmation.Checked);

            EnableResetButton(true);
        }

        private void kbtnReset_Click(object sender, EventArgs e)
        {
            _settingsManager.ResetSettings(_settingsManager.GetAskForSaveConfirmation());

            EnableResetButton(true);
        }

        private void kbtnOk_Click(object sender, EventArgs e)
        {
            _settingsManager.SaveSettings(_settingsManager.GetAskForSaveConfirmation());

            DialogResult = DialogResult.OK;
        }

        private void EnableResetButton(bool enable) => kbtnReset.Enabled = enable;
    }
}
