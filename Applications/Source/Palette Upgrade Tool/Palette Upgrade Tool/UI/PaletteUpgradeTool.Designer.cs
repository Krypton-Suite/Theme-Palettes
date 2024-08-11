namespace PaletteUpgradeTool
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPictureBox1 = new Krypton.Toolkit.KryptonPictureBox();
            this.kryptonWrapLabel1 = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonCommand1 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonCommand2 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenu1 = new Krypton.Toolkit.KryptonContextMenu();
            this.kcpbUpgrader = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtInputDirectory = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtOutputDirectory = new Krypton.Toolkit.KryptonTextBox();
            this.klbFiles = new Krypton.Toolkit.KryptonListBox();
            this.kbtnOptions = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kbtnUpgrade = new Krypton.Toolkit.KryptonButton();
            this.bsaBrowseInputDirectory = new Krypton.Toolkit.ButtonSpecAny();
            this.bsaBrowseOutputDirectory = new Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonWrapLabel1);
            this.kryptonPanel1.Controls.Add(this.kryptonPictureBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlCustom1;
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 113);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonPictureBox1
            // 
            this.kryptonPictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPictureBox1.Image = global::PaletteUpgradeTool.Properties.Resources.Krypton_Icon_64_x_64;
            this.kryptonPictureBox1.Location = new System.Drawing.Point(700, 0);
            this.kryptonPictureBox1.Name = "kryptonPictureBox1";
            this.kryptonPictureBox1.Size = new System.Drawing.Size(100, 113);
            this.kryptonPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.kryptonPictureBox1.TabIndex = 0;
            this.kryptonPictureBox1.TabStop = false;
            // 
            // kryptonWrapLabel1
            // 
            this.kryptonWrapLabel1.AutoSize = false;
            this.kryptonWrapLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonWrapLabel1.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.kryptonWrapLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kryptonWrapLabel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
            this.kryptonWrapLabel1.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(700, 113);
            this.kryptonWrapLabel1.Text = "Palette Upgrade Tool (Beta) β";
            this.kryptonWrapLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.kryptonWrapLabel1.UseCompatibleTextRendering = true;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kbtnUpgrade);
            this.kryptonPanel2.Controls.Add(this.kbtnCancel);
            this.kryptonPanel2.Controls.Add(this.kbtnOptions);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 610);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel2.Size = new System.Drawing.Size(800, 50);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kryptonCommand1
            // 
            this.kryptonCommand1.Text = "kryptonCommand1";
            // 
            // kryptonCommand2
            // 
            this.kryptonCommand2.Text = "kryptonCommand2";
            // 
            // kcpbUpgrader
            // 
            this.kcpbUpgrader.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            this.kcpbUpgrader.ThemeName = null;
            this.kcpbUpgrader.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 609);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(800, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 113);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(800, 496);
            this.kryptonPanel3.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ktxtInputDirectory, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ktxtOutputDirectory, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.klbFiles, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 496);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(794, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Input Directory:";
            // 
            // ktxtInputDirectory
            // 
            this.ktxtInputDirectory.ButtonSpecs.Add(this.bsaBrowseInputDirectory);
            this.ktxtInputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtInputDirectory.Location = new System.Drawing.Point(3, 29);
            this.ktxtInputDirectory.Name = "ktxtInputDirectory";
            this.ktxtInputDirectory.Size = new System.Drawing.Size(794, 24);
            this.ktxtInputDirectory.TabIndex = 1;
            this.ktxtInputDirectory.Text = "kryptonTextBox1";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 444);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(794, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Output Directory:";
            // 
            // ktxtOutputDirectory
            // 
            this.ktxtOutputDirectory.ButtonSpecs.Add(this.bsaBrowseOutputDirectory);
            this.ktxtOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtOutputDirectory.Location = new System.Drawing.Point(3, 470);
            this.ktxtOutputDirectory.Name = "ktxtOutputDirectory";
            this.ktxtOutputDirectory.Size = new System.Drawing.Size(794, 24);
            this.ktxtOutputDirectory.TabIndex = 3;
            this.ktxtOutputDirectory.Text = "kryptonTextBox2";
            // 
            // klbFiles
            // 
            this.klbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klbFiles.Location = new System.Drawing.Point(3, 58);
            this.klbFiles.Name = "klbFiles";
            this.klbFiles.Size = new System.Drawing.Size(794, 380);
            this.klbFiles.TabIndex = 4;
            // 
            // kbtnOptions
            // 
            this.kbtnOptions.Location = new System.Drawing.Point(12, 13);
            this.kbtnOptions.Name = "kbtnOptions";
            this.kbtnOptions.Size = new System.Drawing.Size(90, 25);
            this.kbtnOptions.TabIndex = 0;
            this.kbtnOptions.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOptions.Values.Text = "O&ptions";
            this.kbtnOptions.Click += new System.EventHandler(this.kbtnOptions_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(698, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cance&l";
            this.kbtnCancel.Values.UseAsADialogButton = true;
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // kbtnUpgrade
            // 
            this.kbtnUpgrade.Enabled = false;
            this.kbtnUpgrade.Location = new System.Drawing.Point(602, 13);
            this.kbtnUpgrade.Name = "kbtnUpgrade";
            this.kbtnUpgrade.Size = new System.Drawing.Size(90, 25);
            this.kbtnUpgrade.TabIndex = 2;
            this.kbtnUpgrade.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnUpgrade.Values.Text = "&Upgrade";
            this.kbtnUpgrade.Click += new System.EventHandler(this.kbtnUpgrade_Click);
            // 
            // bsaBrowseInputDirectory
            // 
            this.bsaBrowseInputDirectory.Text = ".&..";
            this.bsaBrowseInputDirectory.UniqueName = "c205ee999e4247bcbfe0a548e67162bb";
            this.bsaBrowseInputDirectory.Click += new System.EventHandler(this.bsaBrowseInputDirectory_Click);
            // 
            // bsaBrowseOutputDirectory
            // 
            this.bsaBrowseOutputDirectory.Text = ".&..";
            this.bsaBrowseOutputDirectory.UniqueName = "1c9813f6c04040889c4bd1bdc08cd88c";
            this.bsaBrowseOutputDirectory.Click += new System.EventHandler(this.bsaBrowseOutputDirectory_Click);
            // 
            // PaletteUpgradeTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 660);
            this.Controls.Add(this.kryptonPanel3);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.Name = "PaletteUpgradeTool";
            this.Text = "Palette Upgrade Tool (Beta) β";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonPictureBox kryptonPictureBox1;
        private Krypton.Toolkit.KryptonWrapLabel kryptonWrapLabel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonCommand kryptonCommand1;
        private Krypton.Toolkit.KryptonCommand kryptonCommand2;
        private Krypton.Toolkit.KryptonContextMenu kryptonContextMenu1;
        private Krypton.Toolkit.KryptonCustomPaletteBase kcpbUpgrader;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonTextBox ktxtInputDirectory;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonTextBox ktxtOutputDirectory;
        private Krypton.Toolkit.KryptonListBox klbFiles;
        private Krypton.Toolkit.KryptonButton kbtnOptions;
        private Krypton.Toolkit.KryptonButton kbtnCancel;
        private Krypton.Toolkit.KryptonButton kbtnUpgrade;
        private Krypton.Toolkit.ButtonSpecAny bsaBrowseInputDirectory;
        private Krypton.Toolkit.ButtonSpecAny bsaBrowseOutputDirectory;
    }
}