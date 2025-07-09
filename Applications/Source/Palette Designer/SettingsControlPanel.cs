#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
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

            // Ensure the dialog buttons close the panel when modeless
            kbtnCancel.Click += KbtnCancel_Click;
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

            EnableResetButton(true);
        }

        private void kbtnOk_Click(object sender, EventArgs e)
        {

            var ask = _settingsManager.GetAskForSaveConfirmation();

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

            Close();
        }

        private void EnableResetButton(bool enable) => kbtnReset.Enabled = enable;

        private void KbtnCancel_Click(object sender, EventArgs e) => Close();
    }
}
