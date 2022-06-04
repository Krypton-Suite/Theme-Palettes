using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace PaletteDesigner.Pages
{
    public partial class InputControls : UserControl
    {
        private readonly List<KryptonTextBox> textBoxes;
        private readonly List<KryptonComboBox> comboBoxes;
        private readonly List<KryptonRichTextBox> richTextBoxes;
        private readonly List<KryptonNumericUpDown> numericUpDowns;

        public InputControls()
        {
            InitializeComponent();
            textBoxes = new List<KryptonTextBox>(new[]
            {
                textBoxDisabled,
                textBoxNormal,
                textBoxActive,


                multiDisabled,
                multiNormal,
                multiActive
            });

            richTextBoxes = new List<KryptonRichTextBox>(new[]
            {
                rtbDisabled,
                rtbNormal,
                rtbActive
            });

            comboBoxes = new List<KryptonComboBox>( new[]
            {
                comboBoxDisabled,
                comboBoxDisabled2,
                comboBoxNormal,
                comboBoxActive
            });
            numericUpDowns = new List<KryptonNumericUpDown>( new[]
            {
                numericDisabled,
                numericNormal,
                numericActive
            });

            // Input controls fixed states
            textBoxNormal.SetFixedState(false);
            textBoxActive.SetFixedState(true);
            rtbNormal.SetFixedState(false);
            rtbActive.SetFixedState(true);
            multiNormal.SetFixedState(false);
            multiActive.SetFixedState(true);
            comboBoxNormal.SetFixedState(false);
            comboBoxActive.SetFixedState(true);
            numericNormal.SetFixedState(false);
            numericActive.SetFixedState(true);

            KryptonNavigatorDesignInputControls_SelectedPageChanged(this, EventArgs.Empty);
        }

        public void ApplyPalette(KryptonPalette palette)
        {
            textBoxes.ForEach(control => control.Palette = palette);
            comboBoxes.ForEach(control => control.Palette = palette);
            richTextBoxes.ForEach(control => control.Palette = palette);
            numericUpDowns.ForEach(control => control.Palette = palette);

            kryptonPanel1.Palette = palette;
        }

        private void KryptonNavigatorDesignInputControls_SelectedPageChanged(object sender, System.EventArgs e)
        {
            // Update the design page text with the selected style information
            //pageDesignInputControls.TextTitle = kryptonNavigatorDesignInputControls.SelectedPage.Text;
            //pageDesignInputControls.TextDescription = kryptonNavigatorDesignInputControls.SelectedPage.TextDescription;

            InputControlStyle inputControlStyle;
            var alwaysActive = true;

            // Work out the input control style to be used
            switch (kryptonNavigatorDesignInputControls.SelectedIndex)
            {
                default:
                case 0:
                    inputControlStyle = InputControlStyle.Standalone;
                    break;
                case 1:
                    inputControlStyle = InputControlStyle.Ribbon;
                    alwaysActive = false;
                    break;
                case 2:
                    inputControlStyle = InputControlStyle.Custom1;
                    break;
            }

            // Update all the displayed controls with the new styles
            textBoxes.ForEach(control => control.InputControlStyle = inputControlStyle);
            comboBoxes.ForEach(control => control.InputControlStyle = inputControlStyle);
            richTextBoxes.ForEach(control => control.InputControlStyle = inputControlStyle);
            numericUpDowns.ForEach(control => control.InputControlStyle = inputControlStyle);

            textBoxes.ForEach(control => control.AlwaysActive = alwaysActive);
            comboBoxes.ForEach(control => control.AlwaysActive = alwaysActive);
            richTextBoxes.ForEach(control => control.AlwaysActive = alwaysActive);
            numericUpDowns.ForEach(control => control.AlwaysActive = alwaysActive);

        }

    }
}
