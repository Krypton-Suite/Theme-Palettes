#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

// @tobitege: remove this and related code once Input- and MessageBox visuals are fixed!
// This define will show standard MessageBoxes instead of KryptonMessageBoxes.
#define USE_SYSTEM_MESSAGEBOX

using PaletteDesigner.Properties;

namespace PaletteDesigner
{
    public class SettingsManager
    {
        #region Instance Fields

        private readonly Settings _settings = Settings.Default;

        #endregion

        #region Setters & Getters

        /// <summary>Sets the maximised.</summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void SetMaximised(bool value) => _settings.StartMaximised = value;

        /// <summary>Gets the maximised.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool GetMaximised() => _settings.StartMaximised;

        /// <summary>Sets the ask for save confirmation.</summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void SetAskForSaveConfirmation(bool value) => _settings.AskForSaveConfirmation = value;

        /// <summary>Gets the ask for save confirmation.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool GetAskForSaveConfirmation() => _settings.AskForSaveConfirmation;

        /// <summary>Sets the theme.</summary>
        /// <param name="paletteMode">The palette mode.</param>
        public void SetTheme(PaletteMode paletteMode) => _settings.Theme = paletteMode;

        /// <summary>Gets the theme.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public PaletteMode GetTheme() => _settings.Theme;

        /// <summary>Sets the index of the theme selected.</summary>
        /// <param name="value">The value.</param>
        public void SetThemeSelectedIndex(int value) => _settings.ThemeSelectedIndex = value;

        /// <summary>Gets the index of the theme selected.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public int GetThemeSelectedIndex() => _settings.ThemeSelectedIndex;

        /// <summary>Sets the upgrade on import.</summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void SetUpgradeOnImport(bool value) => _settings.UpgradeOnImport = value;

        /// <summary>Gets the upgrade on import.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool GetUpgradeOnImport() => _settings.UpgradeOnImport;

        #endregion

        #region Public

        /// <summary>
        /// Saves the settings, supplying an owner window so any confirmation dialog appears on top.
        /// </summary>
        /// <param name="owner">Owner window for the message box.</param>
        /// <param name="useConfirmation">Whether to prompt the user for confirmation.</param>
        public void SaveSettings(IWin32Window owner, bool useConfirmation = false)
        {
            if (useConfirmation)
            {
#if USE_SYSTEM_MESSAGEBOX
                DialogResult result = MessageBox.Show(owner,
                    "Do you want to save these settings with the current values?",
                    "Save Settings",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
#else
                DialogResult result = KryptonMessageBox.Show(owner,
                    "Do you want to save these settings with the current values?",
                    "Save Settings",
                    KryptonMessageBoxButtons.YesNo,
                    KryptonMessageBoxIcon.Question);
#endif

                if (result == DialogResult.Yes)
                {
                    _settings.Save();
                }
            }
            else
            {
                _settings.Save();
            }
        }

        /// <summary>
        /// Resets the settings with an owner window for the confirmation dialog.
        /// </summary>
        /// <param name="owner">Owner window for the message box.</param>
        /// <param name="useConfirmation">Whether to prompt the user for confirmation.</param>
        public void ResetSettings(IWin32Window owner, bool useConfirmation = false)
        {
            if (useConfirmation)
            {
#if USE_SYSTEM_MESSAGEBOX
                DialogResult result = MessageBox.Show(owner,
                    "Do you want to reset these settings with the default values?",
                    "Reset Settings",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
#else
                DialogResult result = KryptonMessageBox.Show(owner,
                    "Do you want to reset these settings with the default values?",
                    "Reset Settings",
                    KryptonMessageBoxButtons.YesNo,
                    KryptonMessageBoxIcon.Question);
#endif

                if (result == DialogResult.Yes)
                {
                    SetMaximised(false);

                    SetAskForSaveConfirmation(true);

                    SetTheme(PaletteMode.Microsoft365Blue);

                    SetThemeSelectedIndex(33);
                }
            }
            else
            {
                SetMaximised(false);

                SetAskForSaveConfirmation(true);

                SetTheme(PaletteMode.Microsoft365Blue);

                SetThemeSelectedIndex(33);

                SetUpgradeOnImport(false);
            }

            SaveSettings(owner, useConfirmation);
        }

        /// <summary>Saves the settings.</summary>
        /// <param name="useConfirmation">if set to <c>true</c> [use confirmation].</param>
        public void SaveSettings(bool useConfirmation = false)
        {
            SaveSettings(owner: null, useConfirmation);
        }

        /// <summary>Resets the settings.</summary>
        /// <param name="useConfirmation">if set to <c>true</c> [use confirmation].</param>
        public void ResetSettings(bool useConfirmation = false)
        {
            ResetSettings(owner: null, useConfirmation);
        }

        #endregion
    }
}