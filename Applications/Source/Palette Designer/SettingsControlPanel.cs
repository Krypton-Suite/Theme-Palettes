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

            kbtnCancel.Text = KryptonLanguageManager.GeneralToolkitStrings.Cancel;

            kbtnCancel.DialogResult = DialogResult.Cancel;

            kbtnOk.Text = KryptonLanguageManager.GeneralToolkitStrings.OK;

            kbtnOk.DialogResult = DialogResult.OK;

            kbtnReset.Text = KryptonLanguageManager.CustomToolkitStrings.Reset;

            AcceptButton = kbtnOk;

            CancelButton = kbtnCancel;
        }

        private void SettingsControlPanel_Load(object sender, EventArgs e)
        {
            kchkAskForConfirmation.Checked = _settingsManager.GetAskForSaveConfirmation();

            kchkStartMaximised.Checked = _settingsManager.GetMaximised();

            ktcmbTheme.SelectedIndex = _settingsManager.GetThemeSelectedIndex();
        }
    }
}
