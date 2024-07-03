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
            this.kpnlTop = new Krypton.Toolkit.KryptonPanel();
            this.kryptonWrapLabel1 = new Krypton.Toolkit.KryptonWrapLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnOptions = new Krypton.Toolkit.KryptonButton();
            this.kbtnUpgrade = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtInputDirectory = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtOutputDirectory = new Krypton.Toolkit.KryptonTextBox();
            this.klbFiles = new Krypton.Toolkit.KryptonListBox();
            this.kmMain = new Krypton.Toolkit.KryptonManager(this.components);
            this.kcmdInputDirectory = new Krypton.Toolkit.KryptonCommand();
            this.kcmdOutputDirectory = new Krypton.Toolkit.KryptonCommand();
            this.kcpbUpgrader = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.bsaInputDirectory = new Krypton.Toolkit.ButtonSpecAny();
            this.bsaOutputDirectory = new Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).BeginInit();
            this.kpnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlTop
            // 
            this.kpnlTop.Controls.Add(this.kryptonWrapLabel1);
            this.kpnlTop.Controls.Add(this.pictureBox1);
            this.kpnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlTop.Location = new System.Drawing.Point(0, 0);
            this.kpnlTop.Name = "kpnlTop";
            this.kpnlTop.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlCustom1;
            this.kpnlTop.Size = new System.Drawing.Size(804, 113);
            this.kpnlTop.TabIndex = 2;
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
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(694, 113);
            this.kryptonWrapLabel1.Text = "Palette Upgrade Tool (Beta)";
            this.kryptonWrapLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::PaletteUpgradeTool.Properties.Resources.Krypton_Icon_64_x_64;
            this.pictureBox1.Location = new System.Drawing.Point(694, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 113);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnOptions);
            this.kryptonPanel1.Controls.Add(this.kbtnUpgrade);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 600);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(804, 50);
            this.kryptonPanel1.TabIndex = 3;
            // 
            // kbtnOptions
            // 
            this.kbtnOptions.Location = new System.Drawing.Point(12, 13);
            this.kbtnOptions.Name = "kbtnOptions";
            this.kbtnOptions.Size = new System.Drawing.Size(90, 25);
            this.kbtnOptions.TabIndex = 2;
            this.kbtnOptions.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOptions.Values.Text = "O&ptions";
            this.kbtnOptions.Click += new System.EventHandler(this.kbtnOptions_Click);
            // 
            // kbtnUpgrade
            // 
            this.kbtnUpgrade.Enabled = false;
            this.kbtnUpgrade.Location = new System.Drawing.Point(606, 13);
            this.kbtnUpgrade.Name = "kbtnUpgrade";
            this.kbtnUpgrade.Size = new System.Drawing.Size(90, 25);
            this.kbtnUpgrade.TabIndex = 1;
            this.kbtnUpgrade.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnUpgrade.Values.Text = "&Upgrade";
            this.kbtnUpgrade.Click += new System.EventHandler(this.kbtnUpgrade_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(702, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 0;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cance&l";
            this.kbtnCancel.Values.UseAsADialogButton = true;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 599);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(804, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 113);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(804, 486);
            this.kryptonPanel2.TabIndex = 5;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(804, 486);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(798, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Input Directory:";
            // 
            // ktxtInputDirectory
            // 
            this.ktxtInputDirectory.ButtonSpecs.Add(this.bsaInputDirectory);
            this.ktxtInputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtInputDirectory.Location = new System.Drawing.Point(3, 29);
            this.ktxtInputDirectory.Name = "ktxtInputDirectory";
            this.ktxtInputDirectory.ShowEllipsisButton = true;
            this.ktxtInputDirectory.Size = new System.Drawing.Size(798, 24);
            this.ktxtInputDirectory.TabIndex = 1;
            this.ktxtInputDirectory.TextChanged += new System.EventHandler(this.ktxtInputDirectory_TextChanged);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 434);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(798, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Output Directory:";
            // 
            // ktxtOutputDirectory
            // 
            this.ktxtOutputDirectory.ButtonSpecs.Add(this.bsaOutputDirectory);
            this.ktxtOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtOutputDirectory.Location = new System.Drawing.Point(3, 460);
            this.ktxtOutputDirectory.Name = "ktxtOutputDirectory";
            this.ktxtOutputDirectory.ShowEllipsisButton = true;
            this.ktxtOutputDirectory.Size = new System.Drawing.Size(798, 24);
            this.ktxtOutputDirectory.TabIndex = 3;
            // 
            // klbFiles
            // 
            this.klbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klbFiles.Location = new System.Drawing.Point(3, 58);
            this.klbFiles.Name = "klbFiles";
            this.klbFiles.Size = new System.Drawing.Size(798, 370);
            this.klbFiles.TabIndex = 4;
            // 
            // kcmdInputDirectory
            // 
            this.kcmdInputDirectory.Text = ".&..";
            this.kcmdInputDirectory.Execute += new System.EventHandler(this.kcmdInputDirectory_Execute);
            // 
            // kcmdOutputDirectory
            // 
            this.kcmdOutputDirectory.Text = ".&..";
            this.kcmdOutputDirectory.Execute += new System.EventHandler(this.kcmdOutputDirectory_Execute);
            // 
            // kcpbUpgrader
            // 
            this.kcpbUpgrader.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            this.kcpbUpgrader.ThemeName = null;
            this.kcpbUpgrader.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // bsaInputDirectory
            // 
            this.bsaInputDirectory.Text = ".&..";
            this.bsaInputDirectory.UniqueName = "0d3b0510e4d7448fa99f215406d23080";
            // 
            // bsaOutputDirectory
            // 
            this.bsaOutputDirectory.Text = ".&..";
            this.bsaOutputDirectory.UniqueName = "9b8032b948c04f138e197e05f6460e29";
            // 
            // PaletteUpgradeTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 650);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kpnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "PaletteUpgradeTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Palette Upgrade Tool";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).EndInit();
            this.kpnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Krypton.Toolkit.KryptonWrapLabel kryptonWrapLabel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonButton kbtnCancel;
        private Krypton.Toolkit.KryptonButton kbtnUpgrade;
        private Krypton.Toolkit.KryptonButton kbtnOptions;
        private Krypton.Toolkit.KryptonManager kmMain;
        private Krypton.Toolkit.KryptonCommand kcmdInputDirectory;
        private Krypton.Toolkit.KryptonCommand kcmdOutputDirectory;
        private Krypton.Toolkit.KryptonCustomPaletteBase kcpbUpgrader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonTextBox ktxtInputDirectory;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonTextBox ktxtOutputDirectory;
        private Krypton.Toolkit.KryptonListBox klbFiles;
        private Krypton.Toolkit.ButtonSpecAny bsaInputDirectory;
        private Krypton.Toolkit.ButtonSpecAny bsaOutputDirectory;
    }
}