namespace PaletteDesigner.Pages
{
    partial class ImageViewerPage
    {
        private System.ComponentModel.IContainer components = null;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private PaletteDesigner.Controls.FixedWheelImageBox imageBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusZoomLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripTextBox toolStripHex;
        private System.Windows.Forms.ToolStripLabel toolStripLabelR;
        private System.Windows.Forms.ToolStripTextBox toolStripR;
        private System.Windows.Forms.ToolStripLabel toolStripLabelG;
        private System.Windows.Forms.ToolStripTextBox toolStripG;
        private System.Windows.Forms.ToolStripLabel toolStripLabelB;
        private System.Windows.Forms.ToolStripTextBox toolStripB;
        private System.Windows.Forms.ToolStripStatusLabel colorPreviewLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonPaste;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownZoom;
        private System.Windows.Forms.ToolStripMenuItem menuZoom25;
        private System.Windows.Forms.ToolStripMenuItem menuZoom50;
        private System.Windows.Forms.ToolStripMenuItem menuZoom100;
        private System.Windows.Forms.ToolStripMenuItem menuZoom200;
        private System.Windows.Forms.ToolStripMenuItem menuZoom250;
        private System.Windows.Forms.ToolStripMenuItem menuZoom300;
        private System.Windows.Forms.ToolStripMenuItem menuZoom350;
        private System.Windows.Forms.ToolStripMenuItem menuZoom400;
        private System.Windows.Forms.ToolStripMenuItem menuZoom500;
        private System.Windows.Forms.ToolStripMenuItem menuZoom800;
        private System.Windows.Forms.ToolStripSeparator zoomSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuZoomFit;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonCrosshair;
        private System.Windows.Forms.ToolStripButton toolStripButtonRegionMode;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveRegions;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadRegions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.imageBox = new PaletteDesigner.Controls.FixedWheelImageBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusZoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripHex = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelR = new System.Windows.Forms.ToolStripLabel();
            this.toolStripR = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelG = new System.Windows.Forms.ToolStripLabel();
            this.toolStripG = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelB = new System.Windows.Forms.ToolStripLabel();
            this.toolStripB = new System.Windows.Forms.ToolStripTextBox();
            this.colorPreviewLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownZoom = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButtonCrosshair = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRegionMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveRegions = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoadRegions = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.imageBox);
            this.kryptonPanel1.Controls.Add(this.statusStrip1);
            this.kryptonPanel1.Controls.Add(this.toolStrip1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 600);
            this.kryptonPanel1.TabIndex = 0;
            //
            // imageBox
            //
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 22);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(800, 544);
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            this.imageBox.ZoomChanged += new System.EventHandler(this.ImageBox_ZoomChanged);
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageBox_Paint);
            this.imageBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseClick);
            this.imageBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseDoubleClick);
            this.imageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseDown);
            this.imageBox.MouseLeave += new System.EventHandler(this.ImageBox_MouseLeave);
            this.imageBox.MouseEnter += new System.EventHandler(this.ImageBox_MouseEnter);
            this.imageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseMove);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseUp);
            this.imageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageBox_KeyDown);
            this.imageBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ImageBox_KeyUp);
            this.imageBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(ImageBox_PreviewKeyDown);
            //
            // statusStrip1
            //
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusZoomLabel,
            this.statusLabel,
            this.toolStripHex,
            this.toolStripLabelR,
            this.toolStripR,
            this.toolStripLabelG,
            this.toolStripG,
            this.toolStripLabelB,
            this.toolStripB,
            this.colorPreviewLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 34);
            this.statusStrip1.TabIndex = 2;
            //
            // statusZoomLabel
            //
            this.statusZoomLabel.Name = "statusZoomLabel";
            this.statusZoomLabel.Size = new System.Drawing.Size(73, 29);
            this.statusZoomLabel.Text = "Zoom: 100%";
            //
            // statusLabel
            //
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 29);
            //
            // toolStripHex
            //
            this.toolStripHex.AutoSize = false;
            this.toolStripHex.Name = "toolStripHex";
            this.toolStripHex.ReadOnly = true;
            this.toolStripHex.Size = new System.Drawing.Size(80, 34);
            //
            // toolStripLabelR
            //
            this.toolStripLabelR.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toolStripLabelR.Name = "toolStripLabelR";
            this.toolStripLabelR.Size = new System.Drawing.Size(14, 34);
            this.toolStripLabelR.Text = "R";
            //
            // toolStripR
            //
            this.toolStripR.AutoSize = false;
            this.toolStripR.Name = "toolStripR";
            this.toolStripR.ReadOnly = true;
            this.toolStripR.Size = new System.Drawing.Size(40, 34);
            //
            // toolStripLabelG
            //
            this.toolStripLabelG.Name = "toolStripLabelG";
            this.toolStripLabelG.Size = new System.Drawing.Size(15, 32);
            this.toolStripLabelG.Text = "G";
            //
            // toolStripG
            //
            this.toolStripG.AutoSize = false;
            this.toolStripG.Name = "toolStripG";
            this.toolStripG.ReadOnly = true;
            this.toolStripG.Size = new System.Drawing.Size(40, 34);
            //
            // toolStripLabelB
            //
            this.toolStripLabelB.Name = "toolStripLabelB";
            this.toolStripLabelB.Size = new System.Drawing.Size(14, 32);
            this.toolStripLabelB.Text = "B";
            //
            // toolStripB
            //
            this.toolStripB.AutoSize = false;
            this.toolStripB.Name = "toolStripB";
            this.toolStripB.ReadOnly = true;
            this.toolStripB.Size = new System.Drawing.Size(40, 34);

            //
            // colorPreviewLabel
            //
            this.colorPreviewLabel.AutoSize = false;
            this.colorPreviewLabel.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.colorPreviewLabel.Name = "colorPreviewLabel";
            this.colorPreviewLabel.Size = new System.Drawing.Size(100, 26);
            this.colorPreviewLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
            this.colorPreviewLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.colorPreviewLabel.Text = "";
            this.colorPreviewLabel.BackColor = System.Drawing.Color.Transparent;
            //
            // toolStrip1
            //
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(4);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonPaste,
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomOut,
            this.toolStripDropDownZoom,
            this.toolStripButtonCrosshair,
            this.toolStripButtonRegionMode,
            this.toolStripButtonSaveRegions,
            this.toolStripButtonLoadRegions});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 22);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            //
            // toolStripButtonOpen
            //
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(76, 19);
            this.toolStripButtonOpen.Text = "Open Image";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.ToolStripButtonOpen_Click);
            //
            // toolStripButtonPaste
            //
            this.toolStripButtonPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPaste.Name = "toolStripButtonPaste";
            this.toolStripButtonPaste.Size = new System.Drawing.Size(39, 19);
            this.toolStripButtonPaste.Text = "Paste";
            this.toolStripButtonPaste.Click += new System.EventHandler(this.ToolStripButtonPaste_Click);
            //
            // toolStripButtonZoomIn
            //
            this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
            this.toolStripButtonZoomIn.Size = new System.Drawing.Size(23, 19);
            this.toolStripButtonZoomIn.Text = "+";
            this.toolStripButtonZoomIn.Click += new System.EventHandler(this.ToolStripButtonZoomIn_Click);
            //
            // toolStripButtonZoomOut
            //
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            this.toolStripButtonZoomOut.Size = new System.Drawing.Size(23, 19);
            this.toolStripButtonZoomOut.Text = "-";
            this.toolStripButtonZoomOut.Click += new System.EventHandler(this.ToolStripButtonZoomOut_Click);
            //
            // toolStripDropDownZoom
            //
            this.toolStripDropDownZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownZoom.Name = "toolStripDropDownZoom";
            this.toolStripDropDownZoom.Size = new System.Drawing.Size(52, 19);
            this.toolStripDropDownZoom.Text = "Zoom";
            //
            // Zoom dropdown items
            //
            this.menuZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom25.Name = "menuZoom25";
            this.menuZoom25.Size = new System.Drawing.Size(111, 22);
            this.menuZoom25.Tag = 25;
            this.menuZoom25.Text = "25%";
            this.menuZoom25.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom50.Name = "menuZoom50";
            this.menuZoom50.Size = new System.Drawing.Size(111, 22);
            this.menuZoom50.Tag = 50;
            this.menuZoom50.Text = "50%";
            this.menuZoom50.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom100.Name = "menuZoom100";
            this.menuZoom100.Size = new System.Drawing.Size(111, 22);
            this.menuZoom100.Tag = 100;
            this.menuZoom100.Text = "100%";
            this.menuZoom100.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom200.Name = "menuZoom200";
            this.menuZoom200.Size = new System.Drawing.Size(111, 22);
            this.menuZoom200.Tag = 200;
            this.menuZoom200.Text = "200%";
            this.menuZoom200.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom250 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom250.Name = "menuZoom250";
            this.menuZoom250.Size = new System.Drawing.Size(111, 22);
            this.menuZoom250.Tag = 250;
            this.menuZoom250.Text = "250%";
            this.menuZoom250.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom300 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom300.Name = "menuZoom300";
            this.menuZoom300.Size = new System.Drawing.Size(111, 22);
            this.menuZoom300.Tag = 300;
            this.menuZoom300.Text = "300%";
            this.menuZoom300.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom350 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom350.Name = "menuZoom350";
            this.menuZoom350.Size = new System.Drawing.Size(111, 22);
            this.menuZoom350.Tag = 350;
            this.menuZoom350.Text = "350%";
            this.menuZoom350.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom400.Name = "menuZoom400";
            this.menuZoom400.Size = new System.Drawing.Size(111, 22);
            this.menuZoom400.Tag = 400;
            this.menuZoom400.Text = "400%";
            this.menuZoom400.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom500 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom500.Name = "menuZoom500";
            this.menuZoom500.Size = new System.Drawing.Size(111, 22);
            this.menuZoom500.Tag = 500;
            this.menuZoom500.Text = "500%";
            this.menuZoom500.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.menuZoom800 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom800.Name = "menuZoom800";
            this.menuZoom800.Size = new System.Drawing.Size(111, 22);
            this.menuZoom800.Tag = 800;
            this.menuZoom800.Text = "800%";
            this.menuZoom800.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.zoomSeparator = new System.Windows.Forms.ToolStripSeparator();

            this.menuZoomFit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoomFit.Name = "menuZoomFit";
            this.menuZoomFit.Size = new System.Drawing.Size(111, 22);
            this.menuZoomFit.Tag = "Fit";
            this.menuZoomFit.Text = "Fit";
            this.menuZoomFit.Click += new System.EventHandler(this.ZoomMenuItem_Click);

            this.toolStripDropDownZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuZoom25,
            this.menuZoom50,
            this.menuZoom100,
            this.menuZoom200,
            this.menuZoom250,
            this.menuZoom300,
            this.menuZoom350,
            this.menuZoom400,
            this.menuZoom500,
            this.menuZoom800,
            this.zoomSeparator,
            this.menuZoomFit});
            //
            // toolStripButtonCrosshair
            //
            this.toolStripButtonCrosshair.Checked = true;
            this.toolStripButtonCrosshair.CheckOnClick = true;
            this.toolStripButtonCrosshair.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonCrosshair.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCrosshair.Enabled = false;
            this.toolStripButtonCrosshair.Name = "toolStripButtonCrosshair";
            this.toolStripButtonCrosshair.Size = new System.Drawing.Size(60, 19);
            this.toolStripButtonCrosshair.Text = "Crosshair";
            this.toolStripButtonCrosshair.Click += new System.EventHandler(this.ToolStripButtonCrosshair_Click);
            //
            // toolStripButtonRegionMode
            //
            this.toolStripButtonRegionMode.CheckOnClick = true;
            this.toolStripButtonRegionMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRegionMode.Name = "toolStripButtonRegionMode";
            this.toolStripButtonRegionMode.Size = new System.Drawing.Size(82, 19);
            this.toolStripButtonRegionMode.Text = "Region Mode";
            this.toolStripButtonRegionMode.Visible = false;
            this.toolStripButtonRegionMode.CheckedChanged += new System.EventHandler(this.ToolStripButtonRegionMode_CheckedChanged);
            //
            // toolStripButtonSaveRegions
            //
            this.toolStripButtonSaveRegions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSaveRegions.Name = "toolStripButtonSaveRegions";
            this.toolStripButtonSaveRegions.Size = new System.Drawing.Size(80, 19);
            this.toolStripButtonSaveRegions.Text = "Save Regions";
            this.toolStripButtonSaveRegions.Visible = false;
            this.toolStripButtonSaveRegions.Click += new System.EventHandler(this.ToolStripButtonSaveRegions_Click);
            //
            // toolStripButtonLoadRegions
            //
            this.toolStripButtonLoadRegions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLoadRegions.Name = "toolStripButtonLoadRegions";
            this.toolStripButtonLoadRegions.Size = new System.Drawing.Size(82, 19);
            this.toolStripButtonLoadRegions.Text = "Load Regions";
            this.toolStripButtonLoadRegions.Visible = false;
            this.toolStripButtonLoadRegions.Click += new System.EventHandler(this.ToolStripButtonLoadRegions_Click);
            //
            // ImageViewerPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ImageViewerPage";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}