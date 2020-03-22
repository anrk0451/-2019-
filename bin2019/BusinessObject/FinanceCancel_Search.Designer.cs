namespace Bin2019.BusinessObject
{
	partial class FinanceCancel_Search
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
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridCol_Fa004 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridControl2 = new DevExpress.XtraGrid.GridControl();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			this.splitContainerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			this.SuspendLayout();
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
			this.bar1.OptionsBar.AllowQuickCustomization = false;
			this.bar1.OptionsBar.DrawBorder = false;
			this.bar1.Text = "工具";
			// 
			// barButtonItem1
			// 
			this.barButtonItem1.Caption = "查询条件";
			this.barButtonItem1.Id = 0;
			this.barButtonItem1.ImageOptions.SvgImage = global::Bin2019.Properties.Resources.scatterchartlabeloptions1;
			this.barButtonItem1.Name = "barButtonItem1";
			this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
			// 
			// barButtonItem2
			// 
			this.barButtonItem2.Caption = "刷新";
			this.barButtonItem2.Id = 1;
			this.barButtonItem2.ImageOptions.SvgImage = global::Bin2019.Properties.Resources.changeview1;
			this.barButtonItem2.Name = "barButtonItem2";
			// 
			// barButtonItem3
			// 
			this.barButtonItem3.Caption = "导出";
			this.barButtonItem3.Id = 2;
			this.barButtonItem3.ImageOptions.SvgImage = global::Bin2019.Properties.Resources.open21;
			this.barButtonItem3.Name = "barButtonItem3";
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager1;
			this.barDockControlTop.Size = new System.Drawing.Size(1064, 34);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 529);
			this.barDockControlBottom.Manager = this.barManager1;
			this.barDockControlBottom.Size = new System.Drawing.Size(1064, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 34);
			this.barDockControlLeft.Manager = this.barManager1;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 495);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(1064, 34);
			this.barDockControlRight.Manager = this.barManager1;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 495);
			// 
			// splitContainerControl1
			// 
			this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl1.Horizontal = false;
			this.splitContainerControl1.Location = new System.Drawing.Point(0, 34);
			this.splitContainerControl1.Name = "splitContainerControl1";
			this.splitContainerControl1.Panel1.Controls.Add(this.gridControl1);
			this.splitContainerControl1.Panel1.Text = "Panel1";
			this.splitContainerControl1.Panel2.Controls.Add(this.gridControl2);
			this.splitContainerControl1.Panel2.Text = "Panel2";
			this.splitContainerControl1.Size = new System.Drawing.Size(1064, 495);
			this.splitContainerControl1.SplitterPosition = 299;
			this.splitContainerControl1.TabIndex = 4;
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.Location = new System.Drawing.Point(0, 0);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
			this.gridControl1.Size = new System.Drawing.Size(1064, 299);
			this.gridControl1.TabIndex = 4;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
			this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridView1.Appearance.Row.Options.UseFont = true;
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridCol_Fa004,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn25,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.IndicatorWidth = 45;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.Editable = false;
			this.gridView1.OptionsCustomization.AllowFilter = false;
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			this.gridView1.OptionsView.ShowFooter = true;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "结算流水号";
			this.gridColumn5.FieldName = "FA001";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 0;
			this.gridColumn5.Width = 108;
			// 
			// gridColumn17
			// 
			this.gridColumn17.Caption = "交款人(单位)";
			this.gridColumn17.FieldName = "FA003";
			this.gridColumn17.Name = "gridColumn17";
			this.gridColumn17.Visible = true;
			this.gridColumn17.VisibleIndex = 1;
			this.gridColumn17.Width = 206;
			// 
			// gridColumn18
			// 
			this.gridColumn18.Caption = "gridColumn3";
			this.gridColumn18.FieldName = "FA002";
			this.gridColumn18.Name = "gridColumn18";
			this.gridColumn18.OptionsColumn.AllowShowHide = false;
			// 
			// gridColumn19
			// 
			this.gridColumn19.Caption = "业务类型";
			this.gridColumn19.FieldName = "FA002_TEXT";
			this.gridColumn19.Name = "gridColumn19";
			this.gridColumn19.Visible = true;
			this.gridColumn19.VisibleIndex = 2;
			this.gridColumn19.Width = 95;
			// 
			// gridCol_Fa004
			// 
			this.gridCol_Fa004.Caption = "金额";
			this.gridCol_Fa004.DisplayFormat.FormatString = "N2";
			this.gridCol_Fa004.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridCol_Fa004.FieldName = "FA004";
			this.gridCol_Fa004.Name = "gridCol_Fa004";
			this.gridCol_Fa004.Visible = true;
			this.gridCol_Fa004.VisibleIndex = 3;
			this.gridCol_Fa004.Width = 134;
			// 
			// gridColumn21
			// 
			this.gridColumn21.Caption = "发票号";
			this.gridColumn21.FieldName = "INVNUM";
			this.gridColumn21.Name = "gridColumn21";
			this.gridColumn21.Visible = true;
			this.gridColumn21.VisibleIndex = 7;
			this.gridColumn21.Width = 280;
			// 
			// gridColumn22
			// 
			this.gridColumn22.Caption = "gridColumn7";
			this.gridColumn22.FieldName = "FA100";
			this.gridColumn22.Name = "gridColumn22";
			this.gridColumn22.OptionsColumn.AllowShowHide = false;
			// 
			// gridColumn23
			// 
			this.gridColumn23.Caption = "收款人";
			this.gridColumn23.FieldName = "HANDLER";
			this.gridColumn23.Name = "gridColumn23";
			this.gridColumn23.Visible = true;
			this.gridColumn23.VisibleIndex = 4;
			this.gridColumn23.Width = 95;
			// 
			// gridColumn24
			// 
			this.gridColumn24.Caption = "收费时间";
			this.gridColumn24.DisplayFormat.FormatString = "g";
			this.gridColumn24.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumn24.FieldName = "FA200";
			this.gridColumn24.Name = "gridColumn24";
			this.gridColumn24.Visible = true;
			this.gridColumn24.VisibleIndex = 5;
			this.gridColumn24.Width = 153;
			// 
			// gridColumn25
			// 
			this.gridColumn25.Caption = "gridColumn16";
			this.gridColumn25.FieldName = "AC001";
			this.gridColumn25.Name = "gridColumn25";
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "开票标志";
			this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
			this.gridColumn1.FieldName = "FA190";
			this.gridColumn1.MinWidth = 25;
			this.gridColumn1.Name = "gridColumn1";
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.DisplayValueChecked = "1";
			this.repositoryItemCheckEdit1.DisplayValueUnchecked = "0";
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			this.repositoryItemCheckEdit1.ValueChecked = "1";
			this.repositoryItemCheckEdit1.ValueUnchecked = "0";
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "发票类型";
			this.gridColumn2.FieldName = "FA195";
			this.gridColumn2.MinWidth = 25;
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 6;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "作废人";
			this.gridColumn3.FieldName = "RHANDLER";
			this.gridColumn3.MinWidth = 25;
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 8;
			this.gridColumn3.Width = 74;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "作废时间";
			this.gridColumn4.FieldName = "RFA200";
			this.gridColumn4.MinWidth = 25;
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 9;
			this.gridColumn4.Width = 158;
			// 
			// gridControl2
			// 
			this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl2.Location = new System.Drawing.Point(0, 0);
			this.gridControl2.MainView = this.gridView2;
			this.gridControl2.MenuManager = this.barManager1;
			this.gridControl2.Name = "gridControl2";
			this.gridControl2.Size = new System.Drawing.Size(1064, 181);
			this.gridControl2.TabIndex = 1;
			this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
			// 
			// gridView2
			// 
			this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
			this.gridView2.GridControl = this.gridControl2;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsCustomization.AllowFilter = false;
			this.gridView2.OptionsCustomization.AllowGroup = false;
			this.gridView2.OptionsView.ColumnAutoWidth = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			this.gridView2.OptionsView.ShowViewCaption = true;
			this.gridView2.ViewCaption = "收费明细";
			// 
			// gridColumn10
			// 
			this.gridColumn10.Caption = "gridColumn10";
			this.gridColumn10.FieldName = "SA001";
			this.gridColumn10.Name = "gridColumn10";
			// 
			// gridColumn11
			// 
			this.gridColumn11.Caption = "gridColumn11";
			this.gridColumn11.FieldName = "SA002";
			this.gridColumn11.Name = "gridColumn11";
			// 
			// gridColumn12
			// 
			this.gridColumn12.Caption = "项目名称";
			this.gridColumn12.FieldName = "SA003";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 0;
			this.gridColumn12.Width = 120;
			// 
			// gridColumn13
			// 
			this.gridColumn13.Caption = "单价";
			this.gridColumn13.DisplayFormat.FormatString = "N2";
			this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn13.FieldName = "PRICE";
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.Visible = true;
			this.gridColumn13.VisibleIndex = 1;
			this.gridColumn13.Width = 100;
			// 
			// gridColumn14
			// 
			this.gridColumn14.Caption = "数量";
			this.gridColumn14.DisplayFormat.FormatString = "N1";
			this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn14.FieldName = "NUMS";
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 2;
			this.gridColumn14.Width = 85;
			// 
			// gridColumn15
			// 
			this.gridColumn15.Caption = "金额";
			this.gridColumn15.DisplayFormat.FormatString = "N2";
			this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn15.FieldName = "SA007";
			this.gridColumn15.Name = "gridColumn15";
			this.gridColumn15.Visible = true;
			this.gridColumn15.VisibleIndex = 3;
			this.gridColumn15.Width = 125;
			// 
			// FinanceCancel_Search
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "FinanceCancel_Search";
			this.Size = new System.Drawing.Size(1064, 529);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
		private DevExpress.XtraGrid.Columns.GridColumn gridCol_Fa004;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.GridControl gridControl2;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
		private DevExpress.XtraBars.BarButtonItem barButtonItem1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraBars.BarButtonItem barButtonItem2;
		private DevExpress.XtraBars.BarButtonItem barButtonItem3;
	}
}
