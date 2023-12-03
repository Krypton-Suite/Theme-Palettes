namespace PaletteDesigner
{
    partial class FormPaletteUpgradeTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPaletteUpgradeTool));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kbtnUpgrade = new Krypton.Toolkit.KryptonButton();
            this.kryptonComboBox1 = new Krypton.Toolkit.KryptonComboBox();
            this.kcmdBrowseForOriginalFile = new Krypton.Toolkit.KryptonCommand();
            this.kcmdBrowseForUpgradedFile = new Krypton.Toolkit.KryptonCommand();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.krtbOutput = new Krypton.Toolkit.KryptonRichTextBox();
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.krtbInput = new Krypton.Toolkit.KryptonRichTextBox();
            this.bsaBrowseForOriginal = new Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonComboBox1);
            this.kryptonPanel1.Controls.Add(this.kbtnUpgrade);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 402);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(679, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.krtbInput);
            this.kryptonPanel2.Controls.Add(this.krtbOutput);
            this.kryptonPanel2.Controls.Add(this.klblStatus);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonPanel3);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(679, 402);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel3.Controls.Add(this.pictureBox1);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlCustom1;
            this.kryptonPanel3.Size = new System.Drawing.Size(679, 100);
            this.kryptonPanel3.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(679, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(579, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(579, 100);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Palette Upgrade Tool";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(13, 107);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(125, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Original Palette File";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(13, 235);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(136, 20);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Values.Text = "Upgraded Palette File";
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(575, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.UseAsADialogButton = true;
            this.kbtnCancel.Values.Text = "Cance&l";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // kbtnUpgrade
            // 
            this.kbtnUpgrade.Enabled = false;
            this.kbtnUpgrade.Location = new System.Drawing.Point(479, 12);
            this.kbtnUpgrade.Name = "kbtnUpgrade";
            this.kbtnUpgrade.Size = new System.Drawing.Size(90, 25);
            this.kbtnUpgrade.TabIndex = 2;
            this.kbtnUpgrade.Values.Text = "&Upgrade";
            this.kbtnUpgrade.Click += new System.EventHandler(this.kbtnUpgrade_Click);
            // 
            // kryptonComboBox1
            // 
            this.kryptonComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kryptonComboBox1.DropDownWidth = 121;
            this.kryptonComboBox1.IntegralHeight = false;
            this.kryptonComboBox1.Location = new System.Drawing.Point(13, 15);
            this.kryptonComboBox1.Name = "kryptonComboBox1";
            this.kryptonComboBox1.Size = new System.Drawing.Size(121, 21);
            this.kryptonComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonComboBox1.TabIndex = 3;
            this.kryptonComboBox1.Visible = false;
            // 
            // kcmdBrowseForOriginalFile
            // 
            this.kcmdBrowseForOriginalFile.Text = "&...";
            this.kcmdBrowseForOriginalFile.Execute += new System.EventHandler(this.kcmdBrowseForOriginalFile_Execute);
            // 
            // kcmdBrowseForUpgradedFile
            // 
            this.kcmdBrowseForUpgradedFile.Text = "&...";
            this.kcmdBrowseForUpgradedFile.Execute += new System.EventHandler(this.kcmdBrowseForUpgradedFile_Execute);
            // 
            // klblStatus
            // 
            this.klblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblStatus.Location = new System.Drawing.Point(13, 363);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(120, 23);
            this.klblStatus.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
            this.klblStatus.StateCommon.ShortText.Color2 = System.Drawing.Color.Red;
            this.klblStatus.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.klblStatus.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.klblStatus.StateCommon.ShortText.Trim = Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.klblStatus.TabIndex = 5;
            this.klblStatus.Values.Text = "kryptonLabel4";
            // 
            // krtbOutput
            // 
            this.krtbOutput.ButtonSpecs.Add(this.buttonSpecAny1);
            this.krtbOutput.Location = new System.Drawing.Point(13, 261);
            this.krtbOutput.Name = "krtbOutput";
            this.krtbOutput.Size = new System.Drawing.Size(652, 96);
            this.krtbOutput.TabIndex = 6;
            this.krtbOutput.Text = "";
            this.krtbOutput.TextChanged += new System.EventHandler(this.krtbOutput_TextChanged);
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Enabled = Krypton.Toolkit.ButtonEnabled.True;
            this.buttonSpecAny1.KryptonCommand = this.kcmdBrowseForUpgradedFile;
            this.buttonSpecAny1.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAny1.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAny1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Generic;
            this.buttonSpecAny1.UniqueName = "bb6174a6bb674e219e076702012a00aa";
            // 
            // krtbInput
            // 
            this.krtbInput.ButtonSpecs.Add(this.bsaBrowseForOriginal);
            this.krtbInput.Location = new System.Drawing.Point(13, 133);
            this.krtbInput.Name = "krtbInput";
            this.krtbInput.Size = new System.Drawing.Size(652, 96);
            this.krtbInput.TabIndex = 7;
            this.krtbInput.Text = "";
            // 
            // bsaBrowseForOriginal
            // 
            this.bsaBrowseForOriginal.Enabled = Krypton.Toolkit.ButtonEnabled.True;
            this.bsaBrowseForOriginal.KryptonCommand = this.kcmdBrowseForUpgradedFile;
            this.bsaBrowseForOriginal.Style = Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.bsaBrowseForOriginal.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.bsaBrowseForOriginal.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Generic;
            this.bsaBrowseForOriginal.UniqueName = "bb6174a6bb674e219e076702012a00aa";
            this.bsaBrowseForOriginal.Click += new System.EventHandler(this.bsaBrowseForOriginal_Click);
            // 
            // FormPaletteUpgradeTool
            // 
            this.AcceptButton = this.kbtnUpgrade;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(679, 452);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPaletteUpgradeTool";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Palette Upgrade Tool";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonPanel kryptonPanel2;
        private KryptonPanel kryptonPanel3;
        private KryptonBorderEdge kryptonBorderEdge1;
        private PictureBox pictureBox1;
        private KryptonLabel kryptonLabel1;
        private KryptonLabel kryptonLabel2;
        private KryptonLabel kryptonLabel3;
        private KryptonButton kbtnCancel;
        private KryptonButton kbtnUpgrade;
        private KryptonComboBox kryptonComboBox1;
        private KryptonCommand kcmdBrowseForOriginalFile;
        private KryptonCommand kcmdBrowseForUpgradedFile;
        private KryptonLabel klblStatus;
        private KryptonRichTextBox krtbOutput;
        private ButtonSpecAny buttonSpecAny1;
        private KryptonRichTextBox krtbInput;
        private ButtonSpecAny bsaBrowseForOriginal;
    }
}