#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace PaletteDesigner.Pages
{
    public partial class InputControls : UserControl
    {
        private readonly List<KryptonTextBox> _textBoxes;
        private readonly List<KryptonComboBox> _comboBoxes;
        private readonly List<KryptonRichTextBox> _richTextBoxes;
        private readonly List<KryptonNumericUpDown> _numericUpDowns;

        public InputControls()
        {
            InitializeComponent();
            _textBoxes = new List<KryptonTextBox>(new[]
            {
                textBoxDisabled,
                textBoxNormal,
                textBoxActive,


                multiDisabled,
                multiNormal,
                multiActive
            });

            _richTextBoxes = new List<KryptonRichTextBox>(new[]
            {
                rtbDisabled,
                rtbNormal,
                rtbActive
            });

            _comboBoxes = new List<KryptonComboBox>(new[]
            {
                comboBoxDisabled,
                comboBoxDisabled2,
                comboBoxNormal,
                comboBoxNormal2,
                comboBoxActive,
                comboBoxActive2
            });
            _numericUpDowns = new List<KryptonNumericUpDown>(new[]
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

        public void ApplyPalette(KryptonCustomPaletteBase palette)
        {
            _textBoxes.ForEach(control => control.LocalCustomPalette = palette);
            _comboBoxes.ForEach(control => control.LocalCustomPalette = palette);
            _richTextBoxes.ForEach(control => control.LocalCustomPalette = palette);
            _numericUpDowns.ForEach(control => control.LocalCustomPalette = palette);

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
                //case 0:
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
            _textBoxes.ForEach(control => control.InputControlStyle = inputControlStyle);
            _comboBoxes.ForEach(control => control.InputControlStyle = inputControlStyle);
            _richTextBoxes.ForEach(control => control.InputControlStyle = inputControlStyle);
            _numericUpDowns.ForEach(control => control.InputControlStyle = inputControlStyle);

            _textBoxes.ForEach(control => control.AlwaysActive = alwaysActive);
            _comboBoxes.ForEach(control => control.AlwaysActive = alwaysActive);
            _richTextBoxes.ForEach(control => control.AlwaysActive = alwaysActive);
            _numericUpDowns.ForEach(control => control.AlwaysActive = alwaysActive);

        }

    }
}
