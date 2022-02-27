namespace PaletteDesigner.Pages
{
    partial class ToolTipsPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.krbToolTip = new Krypton.Toolkit.KryptonRadioButton();
            this.krbSuperTip = new Krypton.Toolkit.KryptonRadioButton();
            this.krbKeyTip = new Krypton.Toolkit.KryptonRadioButton();
            this.kchkShowImage = new Krypton.Toolkit.KryptonCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(650, 544);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(0, 470);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kchkShowImage);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbKeyTip);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbSuperTip);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbToolTip);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(650, 74);
            this.kryptonGroupBox1.TabIndex = 2;
            this.kryptonGroupBox1.Values.Heading = "Tool Tip Types";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(40, 61);
            this.kryptonButton1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(180, 35);
            this.kryptonButton1.TabIndex = 1;
            this.kryptonButton1.ToolTipValues.Description = "This is a Krypton ToolTip. There are three types, they are:-\r\n    - ToolTip\r\n    " +
    "- SuperTip\r\n    - KeyTip";
            this.kryptonButton1.ToolTipValues.EnableToolTips = true;
            this.kryptonButton1.ToolTipValues.Heading = "Krypton ToolTip Example";
            this.kryptonButton1.ToolTipValues.Image = null;
            this.kryptonButton1.Values.Text = "Hover Over Me";
            // 
            // krbToolTip
            // 
            this.krbToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.krbToolTip.Location = new System.Drawing.Point(15, 18);
            this.krbToolTip.Name = "krbToolTip";
            this.krbToolTip.Size = new System.Drawing.Size(63, 20);
            this.krbToolTip.TabIndex = 0;
            this.krbToolTip.Values.Text = "&ToolTip";
            // 
            // krbSuperTip
            // 
            this.krbSuperTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.krbSuperTip.Checked = true;
            this.krbSuperTip.Location = new System.Drawing.Point(85, 17);
            this.krbSuperTip.Name = "krbSuperTip";
            this.krbSuperTip.Size = new System.Drawing.Size(71, 20);
            this.krbSuperTip.TabIndex = 1;
            this.krbSuperTip.Values.Text = "S&uperTip";
            // 
            // krbKeyTip
            // 
            this.krbKeyTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.krbKeyTip.Location = new System.Drawing.Point(163, 17);
            this.krbKeyTip.Name = "krbKeyTip";
            this.krbKeyTip.Size = new System.Drawing.Size(59, 20);
            this.krbKeyTip.TabIndex = 2;
            this.krbKeyTip.Values.Text = "&KeyTip";
            // 
            // kchkShowImage
            // 
            this.kchkShowImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kchkShowImage.Location = new System.Drawing.Point(499, 17);
            this.kchkShowImage.Name = "kchkShowImage";
            this.kchkShowImage.Size = new System.Drawing.Size(135, 20);
            this.kchkShowImage.TabIndex = 3;
            this.kchkShowImage.Values.Text = "Show ToolTip &Image";
            // 
            // ToolTipsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ToolTipsPage";
            this.Size = new System.Drawing.Size(650, 544);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonRadioButton krbToolTip;
        private Krypton.Toolkit.KryptonCheckBox kchkShowImage;
        private Krypton.Toolkit.KryptonRadioButton krbKeyTip;
        private Krypton.Toolkit.KryptonRadioButton krbSuperTip;
    }
}
