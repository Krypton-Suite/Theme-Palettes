#nullable enable

namespace PaletteDesigner;

partial class ExportBaseSchemeClass
{
    private System.ComponentModel.IContainer components = null!;
    private KryptonLabel lblInfo = null!;
    private KryptonTextBox txtClassName = null!;
    private KryptonTextBox txtFileName = null!;
    private KryptonTextBox txtFolder = null!;
    private KryptonButton btnBrowse = null!;
    private KryptonButton btnCreate = null!;
    private KryptonButton btnCancel = null!;

    private void InitializeComponent()
    {
            this.lblInfo = new Krypton.Toolkit.KryptonLabel();
            this.txtClassName = new Krypton.Toolkit.KryptonTextBox();
            this.txtFileName = new Krypton.Toolkit.KryptonTextBox();
            this.txtFolder = new Krypton.Toolkit.KryptonTextBox();
            this.btnBrowse = new Krypton.Toolkit.KryptonButton();
            this.btnCreate = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(15, 12);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(304, 20);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.TabStop = false;
            this.lblInfo.Values.Text = "Provide a valid C# class name and select target folder.";
            // 
            // txtClassName
            // 
            this.txtClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClassName.Location = new System.Drawing.Point(15, 45);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(404, 23);
            this.txtClassName.TabIndex = 0;
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(15, 80);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(404, 23);
            this.txtFileName.TabIndex = 1;
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(15, 115);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(364, 23);
            this.txtFolder.TabIndex = 2;
            this.txtFolder.ReadOnly = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(385, 115);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(34, 26);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnBrowse.Values.Text = "...";
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(209, 152);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 30);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCreate.Values.Text = "Create Class";
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(319, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ExportBaseSchemeClass
            // 
            this.AcceptButton = this.btnCreate;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(438, 198);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnCancel);
            this.Name = "ExportBaseSchemeClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Base Scheme Class";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
}