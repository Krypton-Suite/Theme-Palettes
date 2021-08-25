using System;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace PaletteDesigner.Pages
{
    public partial class InputControls : UserControl
    {
        private readonly KryptonTextBox[] textBoxes;
        private readonly KryptonComboBox[] comboBoxes;
        private readonly KryptonRichTextBox[] richTextBoxes;
        private readonly KryptonNumericUpDown[] numericUpDowns;

        public InputControls()
        {
            InitializeComponent();
            textBoxes = new[]
            {
                textBoxDisabled,
                textBoxNormal,
                textBoxActive,


                multiDisabled,
                multiNormal,
                multiActive
            };

            richTextBoxes = new[]
            {
                rtbDisabled,
                rtbNormal,
                rtbActive
            };

            comboBoxes = new[]
            {
                comboBoxDisabled,
                comboBoxNormal,
                comboBoxActive
            };
            numericUpDowns = new[]
            {
                numericDisabled,
                numericNormal,
                numericActive
            };

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
            foreach (var control in textBoxes)
            {
                control.Palette = palette;
            }
            foreach (var control in comboBoxes)
            {
                control.Palette = palette;
            }
            foreach (var control in richTextBoxes)
            {
                control.Palette = palette;
            }
            foreach (var control in numericUpDowns)
            {
                control.Palette = palette;
            }

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
            foreach (var control in textBoxes)
            {
                control.InputControlStyle = inputControlStyle;
            }
            foreach (var control in comboBoxes)
            {
                control.InputControlStyle = inputControlStyle;
            }
            foreach (var control in richTextBoxes)
            {
                control.InputControlStyle = inputControlStyle;
            }
            foreach (var control in numericUpDowns)
            {
                control.InputControlStyle = inputControlStyle;
            }

            textBoxDisabled.InputControlStyle = inputControlStyle;
            textBoxNormal.InputControlStyle = inputControlStyle;
            textBoxActive.InputControlStyle = inputControlStyle;
            comboBoxDisabled.InputControlStyle = inputControlStyle;
            comboBoxNormal.InputControlStyle = inputControlStyle;
            comboBoxActive.InputControlStyle = inputControlStyle;
            numericDisabled.InputControlStyle = inputControlStyle;
            numericNormal.InputControlStyle = inputControlStyle;
            numericActive.InputControlStyle = inputControlStyle;

            foreach (var control in textBoxes)
            {
                control.AlwaysActive = alwaysActive;
            }
            foreach (var control in comboBoxes)
            {
                control.AlwaysActive = alwaysActive;
            }
            foreach (var control in richTextBoxes)
            {
                control.AlwaysActive = alwaysActive;
            }
            foreach (var control in numericUpDowns)
            {
                control.AlwaysActive = alwaysActive;
            }
        }

    }
}
