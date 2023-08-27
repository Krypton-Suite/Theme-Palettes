namespace PaletteUpgradeTool.UI
{
    partial class PaletteUpgradeTool
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteUpgradeTool));
            this.kpnlBackground = new Krypton.Toolkit.KryptonPanel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kbtnUpgrade = new Krypton.Toolkit.KryptonButton();
            this.krtbOutput = new Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kbtnBrowse = new Krypton.Toolkit.KryptonButton();
            this.krtbInput = new Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.tmrDetectModification = new System.Windows.Forms.Timer(this.components);
            this.kpnlTop = new Krypton.Toolkit.KryptonPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.kMan = new Krypton.Toolkit.KryptonManager(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBackground)).BeginInit();
            this.kpnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).BeginInit();
            this.kpnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlBackground
            // 
            this.kpnlBackground.Controls.Add(this.klblStatus);
            this.kpnlBackground.Controls.Add(this.kbtnClose);
            this.kpnlBackground.Controls.Add(this.kbtnUpgrade);
            this.kpnlBackground.Controls.Add(this.krtbOutput);
            this.kpnlBackground.Controls.Add(this.kryptonLabel3);
            this.kpnlBackground.Controls.Add(this.kbtnBrowse);
            this.kpnlBackground.Controls.Add(this.krtbInput);
            this.kpnlBackground.Controls.Add(this.kryptonLabel2);
            this.kpnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlBackground.Location = new System.Drawing.Point(0, 139);
            this.kpnlBackground.Margin = new System.Windows.Forms.Padding(4);
            this.kpnlBackground.Name = "kpnlBackground";
            this.kpnlBackground.Size = new System.Drawing.Size(1067, 412);
            this.kpnlBackground.TabIndex = 0;
            // 
            // klblStatus
            // 
            this.klblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.klblStatus.Location = new System.Drawing.Point(16, 359);
            this.klblStatus.Margin = new System.Windows.Forms.Padding(4);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(410, 37);
            this.klblStatus.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
            this.klblStatus.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.klblStatus.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.klblStatus.StateCommon.ShortText.Trim = Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.klblStatus.TabIndex = 11;
            this.klblStatus.Values.Text = "You must specify a valid input file.";
            // 
            // kbtnClose
            // 
            this.kbtnClose.Location = new System.Drawing.Point(931, 366);
            this.kbtnClose.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(120, 31);
            this.kbtnClose.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kbtnClose.StateCommon.Content.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kbtnClose.StateCommon.Content.ShortText.Trim = Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.kbtnClose.TabIndex = 10;
            this.kbtnClose.Values.Text = "C&lose";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kbtnUpgrade
            // 
            this.kbtnUpgrade.Enabled = false;
            this.kbtnUpgrade.Location = new System.Drawing.Point(931, 235);
            this.kbtnUpgrade.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnUpgrade.Name = "kbtnUpgrade";
            this.kbtnUpgrade.Size = new System.Drawing.Size(120, 31);
            this.kbtnUpgrade.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kbtnUpgrade.StateCommon.Content.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kbtnUpgrade.StateCommon.Content.ShortText.Trim = Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.kbtnUpgrade.TabIndex = 7;
            this.kbtnUpgrade.Values.Text = "&Upgrade";
            this.kbtnUpgrade.Click += new System.EventHandler(this.kbtnUpgrade_Click);
            // 
            // krtbOutput
            // 
            this.krtbOutput.Location = new System.Drawing.Point(16, 224);
            this.krtbOutput.Margin = new System.Windows.Forms.Padding(4);
            this.krtbOutput.Name = "krtbOutput";
            this.krtbOutput.Size = new System.Drawing.Size(905, 118);
            this.krtbOutput.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.krtbOutput.TabIndex = 6;
            this.krtbOutput.Text = "";
            this.krtbOutput.TextChanged += new System.EventHandler(this.krtbOutput_TextChanged);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kryptonLabel3.Location = new System.Drawing.Point(16, 185);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(195, 32);
            this.kryptonLabel3.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel3.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonLabel3.StateCommon.ShortText.Trim = Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "Output Palette File";
            // 
            // kbtnBrowse
            // 
            this.kbtnBrowse.Location = new System.Drawing.Point(931, 103);
            this.kbtnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnBrowse.Name = "kbtnBrowse";
            this.kbtnBrowse.Size = new System.Drawing.Size(120, 31);
            this.kbtnBrowse.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kbtnBrowse.StateCommon.Content.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kbtnBrowse.StateCommon.Content.ShortText.Trim = Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.kbtnBrowse.TabIndex = 4;
            this.kbtnBrowse.Values.Text = "&Browse";
            this.kbtnBrowse.Click += new System.EventHandler(this.kbtnBrowse_Click);
            // 
            // krtbInput
            // 
            this.krtbInput.Location = new System.Drawing.Point(16, 59);
            this.krtbInput.Margin = new System.Windows.Forms.Padding(4);
            this.krtbInput.Name = "krtbInput";
            this.krtbInput.ReadOnly = true;
            this.krtbInput.Size = new System.Drawing.Size(905, 118);
            this.krtbInput.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.krtbInput.TabIndex = 3;
            this.krtbInput.Text = "";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kryptonLabel2.Location = new System.Drawing.Point(16, 20);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(178, 32);
            this.kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel2.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonLabel2.StateCommon.ShortText.Trim = Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Input Palette File";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(16, 81);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(259, 37);
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonLabel1.StateCommon.ShortText.Trim = Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Palette Upgrade Tool";
            // 
            // tmrDetectModification
            // 
            this.tmrDetectModification.Enabled = true;
            this.tmrDetectModification.Interval = 250;
            // 
            // kpnlTop
            // 
            this.kpnlTop.Controls.Add(this.pictureBox1);
            this.kpnlTop.Controls.Add(this.kryptonLabel1);
            this.kpnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlTop.Location = new System.Drawing.Point(0, 0);
            this.kpnlTop.Margin = new System.Windows.Forms.Padding(4);
            this.kpnlTop.Name = "kpnlTop";
            this.kpnlTop.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlCustom1;
            this.kpnlTop.Size = new System.Drawing.Size(1067, 139);
            this.kpnlTop.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::PaletteUpgradeTool.Properties.Resources.Square_Design_64_x_64_New_Green;
            this.pictureBox1.Location = new System.Drawing.Point(920, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(147, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // PaletteUpgradeTool
            // 
            this.AcceptButton = this.kbtnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 551);
            this.Controls.Add(this.kpnlBackground);
            this.Controls.Add(this.kpnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteUpgradeTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Palette Upgrade Tool";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBackground)).EndInit();
            this.kpnlBackground.ResumeLayout(false);
            this.kpnlBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).EndInit();
            this.kpnlTop.ResumeLayout(false);
            this.kpnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlBackground;
        private Krypton.Toolkit.KryptonButton kbtnUpgrade;
        private Krypton.Toolkit.KryptonRichTextBox krtbOutput;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonButton kbtnBrowse;
        private Krypton.Toolkit.KryptonRichTextBox krtbInput;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonButton kbtnClose;
        private System.Windows.Forms.Timer tmrDetectModification;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonPanel kpnlTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Krypton.Toolkit.KryptonManager kMan;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Timer timer1;
    }
}