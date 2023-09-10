﻿#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace PaletteDesigner.Pages
{
    public partial class ToolTipsPage : UserControl
    {
        public ToolTipsPage()
        {
            InitializeComponent();

            foreach (var item in Enum.GetValues(typeof(LabelStyle)))
            {
                comboBoxLabelStyles.Items.Add(item);
            }
        }

        public void ApplyPalette(KryptonCustomPaletteBase palette)
        {
            kryptonPanel1.Palette = palette;

            kryptonButton1.Palette = palette;

            kryptonGroupBox1.Palette = palette;

            krbToolTip.Palette = palette;

            krbSuperTip.Palette = palette;

            krbKeyTip.Palette = palette;

            kchkShowImage.Palette = palette;
        }

        private void kchkShowImage_CheckedChanged(object sender, EventArgs e)
        {
            if (kchkShowImage.Checked)
            {
                kryptonButton1.ToolTipValues.Image = Resources.Square_Design_32_x_32_Green;
            }
            else
            {
                kryptonButton1.ToolTipValues.Image = null;
            }
        }

        private void krbToolTip_CheckedChanged(object sender, EventArgs e)
        {
            kryptonButton1.ToolTipValues.ToolTipStyle = LabelStyle.ToolTip;
        }

        private void krbSuperTip_CheckedChanged(object sender, EventArgs e)
        {
            kryptonButton1.ToolTipValues.ToolTipStyle = LabelStyle.SuperTip;
        }

        private void krbKeyTip_CheckedChanged(object sender, EventArgs e)
        {
            kryptonButton1.ToolTipValues.ToolTipStyle = LabelStyle.KeyTip;
        }

        private void comboBoxLabelStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // kryptonButton1.ToolTipValues.ToolTipStyle = comboBoxLabelStyles.Text.ToEnum<LabelStyle>();
        }

        public static T ToEnum<T>(string value) => (T)Enum.Parse(typeof(T), value, true);
    }
}
