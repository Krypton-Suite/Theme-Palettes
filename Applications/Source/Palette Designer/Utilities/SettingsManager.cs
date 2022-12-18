﻿#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

using System.Windows.Forms;

using Krypton.Toolkit;

using PaletteDesigner.Properties;

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
                }
            }
            else
            {
                SetMaximised(false);
            }

            SaveSettings(useConfirmation);
        }

        #endregion
    }
}