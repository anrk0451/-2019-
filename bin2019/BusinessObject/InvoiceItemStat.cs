using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bin2019.BaseObject;
using Bin2019.windows;
using Oracle.ManagedDataAccess.Client;
using Bin2019.Misc;
using Bin2019.Action;
using DevExpress.XtraPrinting;

namespace Bin2019.BusinessObject
{
    public partial class InvoiceItemStat : BaseBusiness
    {
		private DataTable dt_cs_fin = new DataTable();
		private OracleDataAdapter csFinAdapter =
			new OracleDataAdapter("select * from v_itemstat_fin ", SqlAssist.conn);

		private DataTable dt_cs_tax = new DataTable();
		private OracleDataAdapter csTaxAdapter =
			new OracleDataAdapter("select * from v_itemstat_tax ", SqlAssist.conn);

		private string s_begin = string.Empty;
		private string s_end = string.Empty;
		private string[] classArry;

		public InvoiceItemStat()
        {
            InitializeComponent();			 
        }

		private void InvoiceItemStat_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = dt_cs_fin;
			gridControl2.DataSource = dt_cs_tax;
		}

		/// <summary>
		/// 查询条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			Frm_Report_ClassStat frm_stat = new Frm_Report_ClassStat();

			frm_stat.swapdata["BusinessObject"] = this;

			if (frm_stat.ShowDialog() == DialogResult.OK)
			{
				frm_stat.Dispose();

				classArry = this.swapdata["class"] as string[];

				if (this.swapdata["dbegin"] == null || this.swapdata["dbegin"] is System.DBNull)
				{
					s_begin = "1900-01-01";
				}
				else
				{
					s_begin = Convert.ToDateTime(this.swapdata["dbegin"]).ToString("yyyy-MM-dd");
				}

				if (this.swapdata["dend"] == null || this.swapdata["dend"] is System.DBNull)
				{
					s_end = "9999-12-31";
				}
				else
				{
					s_end = Convert.ToDateTime(this.swapdata["dend"]).ToString("yyyy-MM-dd");
				}

				this.RefreshData();

			}
		}

		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			int re = MiscAction.ClassStat(s_begin, s_end, classArry);
			if (re > 0)
			{

				gridView1.BeginUpdate();
				dt_cs_fin.Rows.Clear();
				csFinAdapter.Fill(dt_cs_fin);

				gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn4.SummaryItem.DisplayFormat = "合计 = {0:N2}";


				bs_bs.Caption = "           共有收费笔数:" + MiscAction.GetClassStat_BS().ToString() + "笔";
				gridView1.EndUpdate();


				gridView2.BeginUpdate();
				dt_cs_tax.Rows.Clear();
				csTaxAdapter.Fill(dt_cs_tax);

				gridColumn8.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn8.SummaryItem.DisplayFormat = "合计 = {0:N2}";
 
				gridView2.EndUpdate();

			}
			this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}

		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Value;                      //设置导出模式为文本
				
				if(tabPane1.SelectedPageIndex == 0)
					gridControl1.ExportToXlsx(fileDialog.FileName, options);
				else if(tabPane1.SelectedPageIndex == 1)
					gridControl2.ExportToXlsx(fileDialog.FileName, options);

				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			if (e.Info.IsRowIndicator)
			{
				if (e.RowHandle >= 0)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else if (e.RowHandle < 0 && e.RowHandle > -1000)
				{
					e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
					e.Info.DisplayText = "G" + e.RowHandle.ToString();
				}
			}
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			if (e.Info.IsRowIndicator)
			{
				if (e.RowHandle >= 0)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else if (e.RowHandle < 0 && e.RowHandle > -1000)
				{
					e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
					e.Info.DisplayText = "G" + e.RowHandle.ToString();
				}
			}
		}
	}
}
