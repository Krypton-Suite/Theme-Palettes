#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace PaletteDesigner
{
    public partial class FormChromeTMS : KryptonForm
    {
        #region Identity
        public FormChromeTMS()
        {
            InitializeComponent();
        }
        #endregion

        #region Public

        public KryptonProgressBarToolStripItem ToolStripKryptonProgressBar => kryptonProgressBarToolStripItem1;

        public ToolStripRenderer OverrideToolStripRenderer
        {
            set
            {
                // Apply the new toolstrip renderer to the design page controls
                tmsMenuStrip.Renderer = value;
                tmsStatusStrip.Renderer = value;
                tmsToolStrip.Renderer = value;
                tmsToolStripContainer.TopToolStripPanel.Renderer = value;
                tmsToolStripContainer.BottomToolStripPanel.Renderer = value;
                tmsToolStripContainer.LeftToolStripPanel.Renderer = value;
                tmsToolStripContainer.RightToolStripPanel.Renderer = value;
                tmsToolStripContainer.ContentPanel.Renderer = value;
                bindingNavigator1.Renderer = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            bindingNavigatorMoveFirstItem.Enabled = Enabled;
            bindingNavigatorMoveLastItem.Enabled = Enabled;
            bindingNavigatorMoveNextItem.Enabled = Enabled;
            bindingNavigatorMovePreviousItem.Enabled = Enabled;
            bindingNavigatorPositionItem.Enabled = Enabled;

            base.OnLoad(e);
        }

        #endregion
    }
}
