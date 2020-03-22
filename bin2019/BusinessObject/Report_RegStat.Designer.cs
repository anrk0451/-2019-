namespace Bin2019.BusinessObject
{
	partial class Report_RegStat
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
			this.components = new System.ComponentModel.Container();
			DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
			this.lookup_ac005 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.lookup_ac052 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.lookup_ac100 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)(this.lookup_ac005)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lookup_ac052)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lookup_ac100)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			this.SuspendLayout();
			// 
			// lookup_ac005
			// 
			this.lookup_ac005.AutoHeight = false;
			this.lookup_ac005.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lookup_ac005.Name = "lookup_ac005";
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			gridLevelNode1.RelationName = "Level1";
			this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
			this.gridControl1.Location = new System.Drawing.Point(0, 34);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit2,
            this.lookup_ac005,
            this.lookup_ac052,
            this.lookup_ac100});
			this.gridControl1.Size = new System.Drawing.Size(1131, 549);
			this.gridControl1.TabIndex = 22;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Appearance.FooterPanel.Options.UseTextOptions = true;
			this.gridView1.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.gridView1.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
			this.gridView1.Appearance.HeaderPanel.Options.UseBorderColor = true;
			this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
			this.gridView1.Appearance.HeaderPanel.Options.UseImage = true;
			this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn16,
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn6,
            this.gridColumn11});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", this.gridColumn14, "")});
			this.gridView1.IndicatorWidth = 45;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
			this.gridView1.OptionsBehavior.Editable = false;
			this.gridView1.OptionsCustomization.AllowFilter = false;
			this.gridView1.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.True;
			this.gridView1.OptionsFilter.AllowFilterEditor = false;
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
			this.gridView1.OptionsView.EnableAppearanceOddRow = true;
			this.gridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
			this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.gridView1.OptionsView.ShowFooter = true;
			this.gridView1.PaintStyleName = "Skin";
			this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
			 
			// 
			// gridColumn12
			// 
			this.gridColumn12.Caption = "操作类型";
			this.gridColumn12.FieldName = "TYPE";
			this.gridColumn12.MinWidth = 25;
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 1;
			this.gridColumn12.Width = 94;
			// 
			// gridColumn13
			// 
			this.gridColumn13.Caption = "寄存室编号";
			this.gridColumn13.FieldName = "ROOMID";
			this.gridColumn13.MinWidth = 25;
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.OptionsColumn.AllowShowHide = false;
			this.gridColumn13.Width = 94;
			// 
			// gridColumn14
			// 
			this.gridColumn14.Caption = "寄存室";
			this.gridColumn14.FieldName = "ROOMNAME";
			this.gridColumn14.MinWidth = 25;
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 0;
			this.gridColumn14.Width = 140;
			// 
			// gridColumn16
			// 
			this.gridColumn16.Caption = "逝者编号";
			this.gridColumn16.FieldName = "RC001";
			this.gridColumn16.Name = "gridColumn16";
			this.gridColumn16.Visible = true;
			this.gridColumn16.VisibleIndex = 2;
			this.gridColumn16.Width = 114;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "寄存证号";
			this.gridColumn2.FieldName = "RC109";
			this.gridColumn2.MinWidth = 25;
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 3;
			this.gridColumn2.Width = 136;
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "逝者姓名";
			this.gridColumn1.FieldName = "RC003";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "AC001", "共{0}条!")});
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 4;
			this.gridColumn1.Width = 123;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "寄存位置";
			this.gridColumn6.FieldName = "POSITION";
			this.gridColumn6.MinWidth = 25;
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 5;
			this.gridColumn6.Width = 213;
			// 
			// gridColumn11
			// 
			this.gridColumn11.Caption = "经办日期";
			this.gridColumn11.FieldName = "JBRQ";
			this.gridColumn11.MinWidth = 25;
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 6;
			this.gridColumn11.Width = 114;
			// 
			// repositoryItemTextEdit2
			// 
			this.repositoryItemTextEdit2.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.repositoryItemTextEdit2.AutoHeight = false;
			this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
			this.repositoryItemTextEdit2.NullValuePrompt = "请输入角色名";
			this.repositoryItemTextEdit2.NullValuePromptShowForEmptyValue = true;
			// 
			// lookup_ac052
			// 
			this.lookup_ac052.AutoHeight = false;
			this.lookup_ac052.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lookup_ac052.Name = "lookup_ac052";
			// 
			// lookup_ac100
			// 
			this.lookup_ac100.AutoHeight = false;
			this.lookup_ac100.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lookup_ac100.Name = "lookup_ac100";
			this.lookup_ac100.NullText = "";
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 34);
			this.barDockControlLeft.Manager = this.barManager1;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 549);
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3});
			this.barManager1.MaxItemId = 3;
			// 
			// bar1
			// 
			this.bar1.BarName = "工具";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
			this.bar1.OptionsBar.AllowQuickCustomization = false;
			this.bar1.OptionsBar.DrawBorder = false;
			this.bar1.Text = "工具";
			// 
			// barButtonItem3
			// 
			this.barButtonItem3.Caption = "查询条件";
			this.barButtonItem3.Id = 2;
			this.barButtonItem3.ImageOptions.SvgImage = global::Bin2019.Properties.Resources.scatterchartlabeloptions;
			this.barButtonItem3.Name = "barButtonItem3";
			this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
			// 
			// barButtonItem1
			// 
			this.barButtonItem1.Caption = "刷新";
			this.barButtonItem1.Id = 0;
			this.barButtonItem1.ImageOptions.SvgImage = global::Bin2019.Properties.Resources.changeview;
			this.barButtonItem1.Name = "barButtonItem1";
			this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
			// 
			// barButtonItem2
			// 
			this.barButtonItem2.Caption = "导出";
			this.barButtonItem2.Id = 1;
			this.barButtonItem2.ImageOptions.SvgImage = global::Bin2019.Properties.Resources.open2;
			this.barButtonItem2.Name = "barButtonItem2";
			this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager1;
			this.barDockControlTop.Size = new System.Drawing.Size(1131, 34);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 583);
			this.barDockControlBottom.Manager = this.barManager1;
			this.barDockControlBottom.Size = new System.Drawing.Size(1131, 0);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(1131, 34);
			this.barDockControlRight.Manager = this.barManager1;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 549);
			// 
			// Report_RegStat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "Report_RegStat";
			this.Size = new System.Drawing.Size(1131, 583);
			this.Load += new System.EventHandler(this.Report_RegStat_Load);
			((System.ComponentModel.ISupportInitialize)(this.lookup_ac005)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lookup_ac052)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lookup_ac100)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup_ac005;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup_ac052;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup_ac100;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarButtonItem barButtonItem3;
		private DevExpress.XtraBars.BarButtonItem barButtonItem1;
		private DevExpress.XtraBars.BarButtonItem barButtonItem2;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
	}
}
