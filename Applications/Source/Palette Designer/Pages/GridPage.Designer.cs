namespace PaletteDesigner.Pages
{
    partial class GridPage
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.labelGridNormal = new Krypton.Toolkit.KryptonLabel();
            this.labelGridDisabled = new Krypton.Toolkit.KryptonLabel();
            this.pageDescLabel = new Krypton.Toolkit.KryptonLabel();
            this.pageTitleLabel = new Krypton.Toolkit.KryptonLabel();
            this.dataGridViewNormal = new Krypton.Toolkit.KryptonDataGridView();
            this.kryptonDataGridViewTextBoxColumn1 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.kryptonDataGridViewTextBoxColumn2 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.kryptonDataGridViewTextBoxColumn3 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dataSetGrid = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataGridViewDisabled = new Krypton.Toolkit.KryptonDataGridView();
            this.kryptonDataGridViewTextBoxColumn4 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.kryptonDataGridViewTextBoxColumn5 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.kryptonDataGridViewTextBoxColumn6 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.borderDesignGrids = new Krypton.Toolkit.KryptonPanel();
            this.kryptonNavigatorDesignGrids = new Krypton.Navigator.KryptonNavigator();
            this.kryptonGridList = new Krypton.Navigator.KryptonPage();
            this.kryptonGridSheet = new Krypton.Navigator.KryptonPage();
            this.kryptonGridCustom1 = new Krypton.Navigator.KryptonPage();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNormal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisabled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.borderDesignGrids)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigatorDesignGrids)).BeginInit();
            this.kryptonNavigatorDesignGrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGridSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGridCustom1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.labelGridNormal);
            this.kryptonPanel1.Controls.Add(this.labelGridDisabled);
            this.kryptonPanel1.Controls.Add(this.pageDescLabel);
            this.kryptonPanel1.Controls.Add(this.pageTitleLabel);
            this.kryptonPanel1.Controls.Add(this.dataGridViewNormal);
            this.kryptonPanel1.Controls.Add(this.dataGridViewDisabled);
            this.kryptonPanel1.Controls.Add(this.borderDesignGrids);
            this.kryptonPanel1.Controls.Add(this.kryptonNavigatorDesignGrids);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(644, 544);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // labelGridNormal
            // 
            this.labelGridNormal.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.labelGridNormal.Location = new System.Drawing.Point(280, 60);
            this.labelGridNormal.Name = "labelGridNormal";
            this.labelGridNormal.Size = new System.Drawing.Size(78, 29);
            this.labelGridNormal.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.labelGridNormal.TabIndex = 6;
            this.labelGridNormal.Values.Text = "Normal";
            // 
            // labelGridDisabled
            // 
            this.labelGridDisabled.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.labelGridDisabled.Location = new System.Drawing.Point(280, 240);
            this.labelGridDisabled.Name = "labelGridDisabled";
            this.labelGridDisabled.Size = new System.Drawing.Size(88, 29);
            this.labelGridDisabled.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.labelGridDisabled.TabIndex = 5;
            this.labelGridDisabled.Values.Text = "Disabled";
            // 
            // pageDescLabel
            // 
            this.pageDescLabel.Location = new System.Drawing.Point(90, 36);
            this.pageDescLabel.Name = "pageDescLabel";
            this.pageDescLabel.Size = new System.Drawing.Size(159, 20);
            this.pageDescLabel.TabIndex = 4;
            this.pageDescLabel.Values.Text = "List is the default grid style.";
            // 
            // pageTitleLabel
            // 
            this.pageTitleLabel.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.pageTitleLabel.Location = new System.Drawing.Point(90, 3);
            this.pageTitleLabel.Name = "pageTitleLabel";
            this.pageTitleLabel.Size = new System.Drawing.Size(123, 29);
            this.pageTitleLabel.TabIndex = 3;
            this.pageTitleLabel.Values.Text = "Design Grids";
            // 
            // dataGridViewNormal
            // 
            this.dataGridViewNormal.AutoGenerateColumns = false;
            this.dataGridViewNormal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewNormal.ColumnHeadersHeight = 36;
            this.dataGridViewNormal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kryptonDataGridViewTextBoxColumn1,
            this.kryptonDataGridViewTextBoxColumn2,
            this.kryptonDataGridViewTextBoxColumn3});
            this.dataGridViewNormal.DataMember = "Table1";
            this.dataGridViewNormal.DataSource = this.dataSetGrid;
            this.dataGridViewNormal.Location = new System.Drawing.Point(100, 95);
            this.dataGridViewNormal.Name = "dataGridViewNormal";
            this.dataGridViewNormal.RowHeadersWidth = 51;
            this.dataGridViewNormal.Size = new System.Drawing.Size(308, 136);
            this.dataGridViewNormal.TabIndex = 2;
            // 
            // kryptonDataGridViewTextBoxColumn1
            // 
            this.kryptonDataGridViewTextBoxColumn1.DataPropertyName = "Column1";
            this.kryptonDataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.kryptonDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.kryptonDataGridViewTextBoxColumn1.Name = "kryptonDataGridViewTextBoxColumn1";
            this.kryptonDataGridViewTextBoxColumn1.Width = 75;
            // 
            // kryptonDataGridViewTextBoxColumn2
            // 
            this.kryptonDataGridViewTextBoxColumn2.DataPropertyName = "Column2";
            this.kryptonDataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.kryptonDataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.kryptonDataGridViewTextBoxColumn2.Name = "kryptonDataGridViewTextBoxColumn2";
            this.kryptonDataGridViewTextBoxColumn2.Width = 75;
            // 
            // kryptonDataGridViewTextBoxColumn3
            // 
            this.kryptonDataGridViewTextBoxColumn3.DataPropertyName = "Column3";
            this.kryptonDataGridViewTextBoxColumn3.HeaderText = "Column3";
            this.kryptonDataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.kryptonDataGridViewTextBoxColumn3.Name = "kryptonDataGridViewTextBoxColumn3";
            this.kryptonDataGridViewTextBoxColumn3.Width = 75;
            // 
            // dataSetGrid
            // 
            this.dataSetGrid.DataSetName = "NewDataSet";
            this.dataSetGrid.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3});
            this.dataTable1.Namespace = "";
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "Column1";
            this.dataColumn1.ColumnName = "Column1";
            this.dataColumn1.Namespace = "";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "Column2";
            this.dataColumn2.ColumnName = "Column2";
            this.dataColumn2.Namespace = "";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "Column3";
            this.dataColumn3.ColumnName = "Column3";
            this.dataColumn3.Namespace = "";
            // 
            // dataGridViewDisabled
            // 
            this.dataGridViewDisabled.AutoGenerateColumns = false;
            this.dataGridViewDisabled.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDisabled.ColumnHeadersHeight = 36;
            this.dataGridViewDisabled.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kryptonDataGridViewTextBoxColumn4,
            this.kryptonDataGridViewTextBoxColumn5,
            this.kryptonDataGridViewTextBoxColumn6});
            this.dataGridViewDisabled.DataMember = "Table1";
            this.dataGridViewDisabled.DataSource = this.dataSetGrid;
            this.dataGridViewDisabled.Enabled = false;
            this.dataGridViewDisabled.Location = new System.Drawing.Point(100, 275);
            this.dataGridViewDisabled.Name = "dataGridViewDisabled";
            this.dataGridViewDisabled.RowHeadersWidth = 51;
            this.dataGridViewDisabled.Size = new System.Drawing.Size(308, 136);
            this.dataGridViewDisabled.TabIndex = 21;
            // 
            // kryptonDataGridViewTextBoxColumn4
            // 
            this.kryptonDataGridViewTextBoxColumn4.DataPropertyName = "Column1";
            this.kryptonDataGridViewTextBoxColumn4.HeaderText = "Column1";
            this.kryptonDataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.kryptonDataGridViewTextBoxColumn4.Name = "kryptonDataGridViewTextBoxColumn4";
            this.kryptonDataGridViewTextBoxColumn4.Width = 75;
            // 
            // kryptonDataGridViewTextBoxColumn5
            // 
            this.kryptonDataGridViewTextBoxColumn5.DataPropertyName = "Column2";
            this.kryptonDataGridViewTextBoxColumn5.HeaderText = "Column2";
            this.kryptonDataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.kryptonDataGridViewTextBoxColumn5.Name = "kryptonDataGridViewTextBoxColumn5";
            this.kryptonDataGridViewTextBoxColumn5.Width = 75;
            // 
            // kryptonDataGridViewTextBoxColumn6
            // 
            this.kryptonDataGridViewTextBoxColumn6.DataPropertyName = "Column3";
            this.kryptonDataGridViewTextBoxColumn6.HeaderText = "Column3";
            this.kryptonDataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.kryptonDataGridViewTextBoxColumn6.Name = "kryptonDataGridViewTextBoxColumn6";
            this.kryptonDataGridViewTextBoxColumn6.Width = 75;
            // 
            // borderDesignGrids
            // 
            this.borderDesignGrids.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderDesignGrids.Location = new System.Drawing.Point(71, 0);
            this.borderDesignGrids.Name = "borderDesignGrids";
            this.borderDesignGrids.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.borderDesignGrids.Size = new System.Drawing.Size(1, 544);
            this.borderDesignGrids.TabIndex = 8;
            // 
            // kryptonNavigatorDesignGrids
            // 
            this.kryptonNavigatorDesignGrids.AutoSize = true;
            this.kryptonNavigatorDesignGrids.Bar.BarMapExtraText = Krypton.Navigator.MapKryptonPageText.None;
            this.kryptonNavigatorDesignGrids.Bar.BarMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.kryptonNavigatorDesignGrids.Bar.BarMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.kryptonNavigatorDesignGrids.Bar.BarMultiline = Krypton.Navigator.BarMultiline.Singleline;
            this.kryptonNavigatorDesignGrids.Bar.BarOrientation = Krypton.Toolkit.VisualOrientation.Left;
            this.kryptonNavigatorDesignGrids.Bar.CheckButtonStyle = Krypton.Toolkit.ButtonStyle.LowProfile;
            this.kryptonNavigatorDesignGrids.Bar.ItemAlignment = Krypton.Toolkit.RelativePositionAlign.Near;
            this.kryptonNavigatorDesignGrids.Bar.ItemMaximumSize = new System.Drawing.Size(200, 200);
            this.kryptonNavigatorDesignGrids.Bar.ItemMinimumSize = new System.Drawing.Size(20, 20);
            this.kryptonNavigatorDesignGrids.Bar.ItemOrientation = Krypton.Toolkit.ButtonOrientation.FixedTop;
            this.kryptonNavigatorDesignGrids.Bar.ItemSizing = Krypton.Navigator.BarItemSizing.SameWidthAndHeight;
            this.kryptonNavigatorDesignGrids.Bar.TabBorderStyle = Krypton.Toolkit.TabBorderStyle.RoundedOutsizeMedium;
            this.kryptonNavigatorDesignGrids.Bar.TabStyle = Krypton.Toolkit.TabStyle.HighProfile;
            this.kryptonNavigatorDesignGrids.Button.ButtonDisplayLogic = Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigatorDesignGrids.Button.CloseButtonAction = Krypton.Navigator.CloseButtonAction.RemovePageAndDispose;
            this.kryptonNavigatorDesignGrids.Button.CloseButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigatorDesignGrids.Button.ContextButtonAction = Krypton.Navigator.ContextButtonAction.SelectPage;
            this.kryptonNavigatorDesignGrids.Button.ContextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonNavigatorDesignGrids.Button.ContextMenuMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.kryptonNavigatorDesignGrids.Button.ContextMenuMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.kryptonNavigatorDesignGrids.Button.NextButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.kryptonNavigatorDesignGrids.Button.NextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonNavigatorDesignGrids.Button.PreviousButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.kryptonNavigatorDesignGrids.Button.PreviousButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonNavigatorDesignGrids.ControlKryptonFormFeatures = false;
            this.kryptonNavigatorDesignGrids.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonNavigatorDesignGrids.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigatorDesignGrids.NavigatorMode = Krypton.Navigator.NavigatorMode.BarCheckButtonOnly;
            this.kryptonNavigatorDesignGrids.Owner = null;
            this.kryptonNavigatorDesignGrids.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonNavigatorDesignGrids.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
            this.kryptonGridList,
            this.kryptonGridSheet,
            this.kryptonGridCustom1});
            this.kryptonNavigatorDesignGrids.Panel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonNavigatorDesignGrids.SelectedIndex = 0;
            this.kryptonNavigatorDesignGrids.Size = new System.Drawing.Size(71, 544);
            this.kryptonNavigatorDesignGrids.StateCommon.Bar.BarPaddingInside = new System.Windows.Forms.Padding(-1);
            this.kryptonNavigatorDesignGrids.StateCommon.Bar.BarPaddingOnly = new System.Windows.Forms.Padding(5);
            this.kryptonNavigatorDesignGrids.StateCommon.Bar.BarPaddingOutside = new System.Windows.Forms.Padding(-1);
            this.kryptonNavigatorDesignGrids.StateCommon.Bar.BarPaddingTabs = new System.Windows.Forms.Padding(-1);
            this.kryptonNavigatorDesignGrids.StateCommon.Bar.ButtonPadding = new System.Windows.Forms.Padding(-1);
            this.kryptonNavigatorDesignGrids.TabIndex = 7;
            this.kryptonNavigatorDesignGrids.Text = "kryptonNavigator1";
            this.kryptonNavigatorDesignGrids.SelectedPageChanged += new System.EventHandler(this.KryptonNavigatorDesignGrids_SelectedPageChanged);
            // 
            // kryptonGridList
            // 
            this.kryptonGridList.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonGridList.Flags = 65535;
            this.kryptonGridList.LastVisibleSet = true;
            this.kryptonGridList.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonGridList.Name = "kryptonGridList";
            this.kryptonGridList.Size = new System.Drawing.Size(50, 500);
            this.kryptonGridList.Text = "List";
            this.kryptonGridList.ToolTipTitle = "Page ToolTip";
            this.kryptonGridList.UniqueName = "7E5F74E3999D471E7E5F74E3999D471E";
            // 
            // kryptonGridSheet
            // 
            this.kryptonGridSheet.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonGridSheet.Flags = 65535;
            this.kryptonGridSheet.LastVisibleSet = true;
            this.kryptonGridSheet.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonGridSheet.Name = "kryptonGridSheet";
            this.kryptonGridSheet.Size = new System.Drawing.Size(50, 500);
            this.kryptonGridSheet.Text = "Sheet";
            this.kryptonGridSheet.TextDescription = "Sheet is used when a worksheet style is required.";
            this.kryptonGridSheet.ToolTipTitle = "Page ToolTip";
            this.kryptonGridSheet.UniqueName = "39D1B70212CD455D39D1B70212CD455D";
            // 
            // kryptonGridCustom1
            // 
            this.kryptonGridCustom1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonGridCustom1.Flags = 65535;
            this.kryptonGridCustom1.LastVisibleSet = true;
            this.kryptonGridCustom1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonGridCustom1.Name = "kryptonGridCustom1";
            this.kryptonGridCustom1.Size = new System.Drawing.Size(50, 500);
            this.kryptonGridCustom1.Text = "Custom 1";
            this.kryptonGridCustom1.TextDescription = "Custom 1 style inherits from List and is intended for your own custom use.";
            this.kryptonGridCustom1.ToolTipTitle = "Page ToolTip";
            this.kryptonGridCustom1.UniqueName = "4BC73FB0DC704F514BC73FB0DC704F51";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(90, 25);
            this.kryptonLabel2.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(90, 25);
            this.kryptonLabel1.TabIndex = 0;
            // 
            // GridPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "GridPage";
            this.Size = new System.Drawing.Size(644, 544);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNormal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisabled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.borderDesignGrids)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigatorDesignGrids)).EndInit();
            this.kryptonNavigatorDesignGrids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGridSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGridCustom1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonPanel borderDesignGrids;
        private Krypton.Navigator.KryptonNavigator kryptonNavigatorDesignGrids;
        private Krypton.Navigator.KryptonPage kryptonGridList;
        private Krypton.Navigator.KryptonPage kryptonGridSheet;
        private Krypton.Navigator.KryptonPage kryptonGridCustom1;
        private Krypton.Toolkit.KryptonDataGridView dataGridViewNormal;
        private Krypton.Toolkit.KryptonDataGridView dataGridViewDisabled;
        private Krypton.Toolkit.KryptonLabel labelGridDisabled;
        private Krypton.Toolkit.KryptonLabel labelGridNormal;
        private Krypton.Toolkit.KryptonLabel pageTitleLabel;
        private Krypton.Toolkit.KryptonLabel pageDescLabel;
        private System.Data.DataSet dataSetGrid;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private KryptonDataGridViewTextBoxColumn kryptonDataGridViewTextBoxColumn1;
        private KryptonDataGridViewTextBoxColumn kryptonDataGridViewTextBoxColumn2;
        private KryptonDataGridViewTextBoxColumn kryptonDataGridViewTextBoxColumn3;
        private KryptonDataGridViewTextBoxColumn kryptonDataGridViewTextBoxColumn4;
        private KryptonDataGridViewTextBoxColumn kryptonDataGridViewTextBoxColumn5;
        private KryptonDataGridViewTextBoxColumn kryptonDataGridViewTextBoxColumn6;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
   }
}