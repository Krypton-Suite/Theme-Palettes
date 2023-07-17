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
    public class SettingsManager
    {
        #region Instance Fields

        private readonly Settings _settings = null;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="SettingsManager" /> class.</summary>
        public SettingsManager()
        {
            _settings = new Settings();
        }

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

        #endregion

        #region Public

        /// <summary>Saves the settings.</summary>
        /// <param name="useConfirmation">if set to <c>true</c> [use confirmation].</param>
        public void SaveSettings(bool useConfirmation = false)
        {
            if (useConfirmation)
            {
                DialogResult result = KryptonMessageBox.Show("Do you want to save these settings with the current values?",
                    "Save Settings",
                    KryptonMessageBoxButtons.YesNo,
                    KryptonMessageBoxIcon.Question);

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

        /// <summary>Resets the settings.</summary>
        /// <param name="useConfirmation">if set to <c>true</c> [use confirmation].</param>
        public void ResetSettings(bool useConfirmation = false)
        {
            if (useConfirmation)
            {
                DialogResult result = KryptonMessageBox.Show("Do you want to reset these settings with the default values?",
                    "Reset Settings",
                    KryptonMessageBoxButtons.YesNo,
                    KryptonMessageBoxIcon.Question);

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
            }

            SaveSettings(useConfirmation);
        }

        #endregion
    }
}