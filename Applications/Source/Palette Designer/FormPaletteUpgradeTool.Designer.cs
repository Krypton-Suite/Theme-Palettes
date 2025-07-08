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
            this.kryptonComboBox1 = new Krypton.Toolkit.KryptonComboBox();
            this.kbtnUpgrade = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.krtbInput = new Krypton.Toolkit.KryptonRichTextBox();
            this.bsaBrowseForOriginal = new Krypton.Toolkit.ButtonSpecAny();
            this.kcmdBrowseForUpgradedFile = new Krypton.Toolkit.KryptonCommand();
            this.krtbOutput = new Krypton.Toolkit.KryptonRichTextBox();
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.kcmdBrowseForOriginalFile = new Krypton.Toolkit.KryptonCommand();
            this.kbtnBrowseInput = new Krypton.Toolkit.KryptonButton();
            this.kbtnBrowseOutput = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.kryptonComboBox1);
            this.kryptonPanel1.Controls.Add(this.kbtnUpgrade);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 494);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(905, 62);
            this.kryptonPanel1.TabIndex = 0;
            //
            // kryptonComboBox1
            //
            this.kryptonComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kryptonComboBox1.DropDownWidth = 121;
            this.kryptonComboBox1.IntegralHeight = false;
            this.kryptonComboBox1.Location = new System.Drawing.Point(17, 18);
            this.kryptonComboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonComboBox1.Name = "kryptonComboBox1";
            this.kryptonComboBox1.Size = new System.Drawing.Size(161, 26);
            this.kryptonComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonComboBox1.TabIndex = 3;
            this.kryptonComboBox1.Visible = false;
            //
            // kbtnUpgrade
            //
            this.kbtnUpgrade.Enabled = false;
            this.kbtnUpgrade.Location = new System.Drawing.Point(639, 15);
            this.kbtnUpgrade.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kbtnUpgrade.Name = "kbtnUpgrade";
            this.kbtnUpgrade.Size = new System.Drawing.Size(120, 31);
            this.kbtnUpgrade.TabIndex = 2;
            this.kbtnUpgrade.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnUpgrade.Values.Text = "&Upgrade";
            this.kbtnUpgrade.Click += new System.EventHandler(this.kbtnUpgrade_Click);
            //
            // kbtnCancel
            //
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(767, 16);
            this.kbtnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(120, 31);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cance&l";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            //
            // kryptonBorderEdge1
            //
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(905, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            //
            // kryptonPanel2
            //
            this.kryptonPanel2.Controls.Add(this.krtbInput);
            this.kryptonPanel2.Controls.Add(this.krtbOutput);
            this.kryptonPanel2.Controls.Add(this.klblStatus);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonPanel3);
            this.kryptonPanel2.Controls.Add(this.kbtnBrowseInput);
            this.kryptonPanel2.Controls.Add(this.kbtnBrowseOutput);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(905, 494);
            this.kryptonPanel2.TabIndex = 1;
            //
            // krtbInput
            //
            this.krtbInput.Location = new System.Drawing.Point(17, 164);
            this.krtbInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.krtbInput.Name = "krtbInput";
            this.krtbInput.Size = new System.Drawing.Size(869, 118);
            this.krtbInput.TabIndex = 7;
            this.krtbInput.Text = "";
            //
            // bsaBrowseForOriginal
            //
            this.bsaBrowseForOriginal.Enabled = Krypton.Toolkit.ButtonEnabled.True;
            this.bsaBrowseForOriginal.KryptonCommand = this.kcmdBrowseForUpgradedFile;
            this.bsaBrowseForOriginal.Text = "Browse";
            this.bsaBrowseForOriginal.UniqueName = "bb6174a6bb674e219e076702012a00aa";
            this.bsaBrowseForOriginal.Click += new System.EventHandler(this.bsaBrowseForOriginal_Click);
            //
            // kcmdBrowseForUpgradedFile
            //
            this.kcmdBrowseForUpgradedFile.AssignedButtonSpec = this.buttonSpecAny1;
            this.kcmdBrowseForUpgradedFile.Text = "&...";
            this.kcmdBrowseForUpgradedFile.Execute += new System.EventHandler(this.kcmdBrowseForUpgradedFile_Execute);
            //
            // krtbOutput
            //
            this.krtbOutput.Location = new System.Drawing.Point(17, 321);
            this.krtbOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.krtbOutput.Name = "krtbOutput";
            this.krtbOutput.Size = new System.Drawing.Size(869, 118);
            this.krtbOutput.TabIndex = 6;
            this.krtbOutput.Text = "";
            this.krtbOutput.TextChanged += new System.EventHandler(this.krtbOutput_TextChanged);
            //
            // buttonSpecAny1
            //
            this.buttonSpecAny1.Enabled = Krypton.Toolkit.ButtonEnabled.True;
            this.buttonSpecAny1.KryptonCommand = this.kcmdBrowseForUpgradedFile;
            this.buttonSpecAny1.UniqueName = "bb6174a6bb674e219e076702012a00aa";
            //
            // klblStatus
            //
            this.klblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblStatus.Location = new System.Drawing.Point(17, 447);
            this.klblStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(148, 28);
            this.klblStatus.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
            this.klblStatus.StateCommon.ShortText.Color2 = System.Drawing.Color.Red;
            this.klblStatus.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.klblStatus.TabIndex = 5;
            this.klblStatus.Values.Text = "kryptonLabel4";
            //
            // kryptonLabel3
            //
            this.kryptonLabel3.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(17, 289);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(168, 24);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Values.Text = "Upgraded Palette File";
            //
            // kryptonLabel2
            //
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(17, 132);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(154, 24);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Original Palette File";
            //
            // kryptonPanel3
            //
            this.kryptonPanel3.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel3.Controls.Add(this.pictureBox1);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlCustom1;
            this.kryptonPanel3.Size = new System.Drawing.Size(905, 123);
            this.kryptonPanel3.TabIndex = 0;
            //
            // kryptonLabel1
            //
            this.kryptonLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(772, 123);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Palette Upgrade Tool";
            //
            // pictureBox1
            //
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(772, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            //
            // kcmdBrowseForOriginalFile
            //
            this.kcmdBrowseForOriginalFile.Text = "&...";
            this.kcmdBrowseForOriginalFile.Execute += new System.EventHandler(this.kcmdBrowseForOriginalFile_Execute);
            //
            // kbtnBrowseInput
            //
            this.kbtnBrowseInput.Location = new System.Drawing.Point(793, 128);
            this.kbtnBrowseInput.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnBrowseInput.Name = "kbtnBrowseInput";
            this.kbtnBrowseInput.Size = new System.Drawing.Size(93, 28);
            this.kbtnBrowseInput.TabIndex = 8;
            this.kbtnBrowseInput.Values.Text = "Browse...";
            this.kbtnBrowseInput.Click += new System.EventHandler(this.kcmdBrowseForOriginalFile_Execute);
            //
            // kbtnBrowseOutput
            //
            this.kbtnBrowseOutput.Location = new System.Drawing.Point(793, 285);
            this.kbtnBrowseOutput.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnBrowseOutput.Name = "kbtnBrowseOutput";
            this.kbtnBrowseOutput.Size = new System.Drawing.Size(93, 28);
            this.kbtnBrowseOutput.TabIndex = 9;
            this.kbtnBrowseOutput.Values.Text = "Browse...";
            this.kbtnBrowseOutput.Click += new System.EventHandler(this.kcmdBrowseForUpgradedFile_Execute);
            //
            // FormPaletteUpgradeTool
            //
            this.AcceptButton = this.kbtnUpgrade;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(905, 556);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private KryptonButton kbtnBrowseInput;
        private KryptonButton kbtnBrowseOutput;
    }
}