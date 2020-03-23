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
using Oracle.ManagedDataAccess.Client;
using Bin2019.Misc;
using Bin2019.windows;
using DevExpress.XtraPrinting;

namespace Bin2019.BusinessObject
{
	public partial class FinanceRoll_Report : BaseBusiness
	{
		private DataTable dt_finance = new DataTable("FINANCE");

		private OracleDataAdapter finAdapter =
			new OracleDataAdapter("select * from v_financeRemoveReport where (to_char(rfa200,'yyyy-mm-dd') between :begin and :end ) ", SqlAssist.conn);

		private DataTable dt_detail = new DataTable("DETAIL");
		private OracleDataAdapter deAdapter =
			new OracleDataAdapter("select * from v_finremovedetail where sa010 = :sa010", SqlAssist.conn);

		OracleParameter op_begin = null;
		OracleParameter op_end = null;

		OracleParameter op_sa010 = null;

		public FinanceRoll_Report()
		{
			InitializeComponent();
		}

		private void FinanceRoll_Report_Load(object sender, EventArgs e)
		{

			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;

			op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
			op_sa010.Direction = ParameterDirection.Input;

			finAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
			deAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010 });

			gridControl1.DataSource = dt_finance;
			gridControl2.DataSource = dt_detail;

		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_finance.Rows.Clear();
			finAdapter.Fill(dt_finance);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;

		}

		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.DoSearch();
			this.Filter_IncludeInvoice();
		}

		public void DoSearch()
		{
			Frm_FinanceRoll_Report frm_1 = new Frm_FinanceRoll_Report();
			frm_1.swapdata["BusinessObject"] = this;
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				frm_1.Dispose();
				string s_begin = string.Empty;
				string s_end = string.Empty;

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


				op_begin.Value = s_begin;
				op_end.Value = s_end;

				this.Cursor = Cursors.WaitCursor;
				gridView1.BeginUpdate();
				dt_finance.Rows.Clear();
				
				finAdapter.Fill(dt_finance);

				gridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
				gridColumn1.SummaryItem.DisplayFormat = "共计 = {0:N0}笔";

				gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn5.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;
			}
		}

		private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
				gridControl1.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!gridView1.IsFindPanelVisible)
				gridView1.ShowFindPanel();
			else
				gridView1.HideFindPanel();
		}

		private void GridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

		private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (e.FocusedRowHandle >= 0)
			{
				this.RetrieveDetail(e.FocusedRowHandle);
			}
		}

		/// <summary>
		/// 检索明细
		/// </summary>
		/// <param name="rowHandle"></param>
		private void RetrieveDetail(int rowHandle)
		{
			if (rowHandle >= 0)
			{
				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				op_sa010.Value = s_fa001;
				gridView2.BeginUpdate();
				dt_detail.Rows.Clear();
				deAdapter.Fill(dt_detail);
				gridView2.EndUpdate();
			}
		}

		private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "FA195")
			{
				if (e.Value.ToString() == "T")
					e.DisplayText = "税务发票";
				else if (e.Value.ToString() == "F")
					e.DisplayText = "财政发票";
			}
		}

		/// <summary>
		/// 只包含作废发票 项目
		/// </summary>
		private void Filter_IncludeInvoice()
		{
			if (Convert.ToBoolean(toggle_1.EditValue))
			{
				gridView1.ActiveFilterString = "INVNUM is not null ";
			}
			else
			{
				gridView1.ActiveFilter.Clear();
			}
		}

		private void toggle_1_EditValueChanged(object sender, EventArgs e)
		{
			this.Filter_IncludeInvoice();
		}
	}
}
