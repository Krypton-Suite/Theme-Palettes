#nullable enable

namespace PaletteDesigner
{
    partial class LoadDefaultPaletteDialog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Method required for Designer support – do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonPanelButtons = new Krypton.Toolkit.KryptonPanel();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanelContent = new Krypton.Toolkit.KryptonPanel();
            this.kcmbThemes = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelButtons)).BeginInit();
            this.kryptonPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelContent)).BeginInit();
            this.kryptonPanelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbThemes)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelButtons
            //
            this.kryptonPanelButtons.Controls.Add(this.kbtnOk);
            this.kryptonPanelButtons.Controls.Add(this.kbtnCancel);
            this.kryptonPanelButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanelButtons.Location = new System.Drawing.Point(0, 96);
            this.kryptonPanelButtons.Name = "kryptonPanelButtons";
            this.kryptonPanelButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanelButtons.Size = new System.Drawing.Size(384, 50);
            this.kryptonPanelButtons.TabIndex = 0;
            //
            // kbtnOk
            //
            this.kbtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(198, 12);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(80, 25);
            this.kbtnOk.TabIndex = 1;
            this.kbtnOk.Values.Text = "OK";
            //
            // kbtnCancel
            //
            this.kbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(284, 12);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(80, 25);
            this.kbtnCancel.TabIndex = 2;
            this.kbtnCancel.Values.Text = "Cancel";
            //
            // kryptonBorderEdge1
            //
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderSecondary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(384, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            //
            // kryptonPanelContent
            //
            this.kryptonPanelContent.Controls.Add(this.kcmbThemes);
            this.kryptonPanelContent.Controls.Add(this.kryptonLabel1);
            this.kryptonPanelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelContent.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelContent.Name = "kryptonPanelContent";
            this.kryptonPanelContent.Size = new System.Drawing.Size(384, 96);
            this.kryptonPanelContent.TabIndex = 1;
            //
            // kcmbThemes
            //
            this.kcmbThemes.DropDownWidth = 350;
            this.kcmbThemes.IntegralHeight = false;
            this.kcmbThemes.Location = new System.Drawing.Point(15, 43);
            this.kcmbThemes.Name = "kcmbThemes";
            this.kcmbThemes.Size = new System.Drawing.Size(350, 22);
            this.kcmbThemes.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbThemes.TabIndex = 0;
            //
            // kryptonLabel1
            //
            this.kryptonLabel1.Location = new System.Drawing.Point(15, 17);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(178, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Select a default palette to load:";
            //
            // LoadDefaultPaletteDialog
            //
            this.AcceptButton = this.kbtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(384, 146);
            this.Controls.Add(this.kryptonPanelContent);
            this.Controls.Add(this.kryptonPanelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadDefaultPaletteDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load Default Palette";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelButtons)).EndInit();
            this.kryptonPanelButtons.ResumeLayout(false);
            this.kryptonPanelButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelContent)).EndInit();
            this.kryptonPanelContent.ResumeLayout(false);
            this.kryptonPanelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbThemes)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private KryptonPanel kryptonPanelButtons = null!;
        private KryptonButton kbtnOk = null!;
        private KryptonButton kbtnCancel = null!;
        private KryptonBorderEdge kryptonBorderEdge1 = null!;
        private KryptonPanel kryptonPanelContent = null!;
        private KryptonComboBox kcmbThemes = null!;
        private KryptonLabel kryptonLabel1 = null!;
    }
}