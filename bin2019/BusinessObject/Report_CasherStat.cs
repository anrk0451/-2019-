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
using Bin2019.Action;
using DevExpress.XtraPrinting;

namespace Bin2019.BusinessObject
{
	public partial class Report_CasherStat : BaseBusiness
	{
		private DataTable dt_stat = new DataTable();
		private OracleDataAdapter statAdapter =
			new OracleDataAdapter("select * from rep_casherStat", SqlAssist.conn);

		private string s_begin = string.Empty;
		private string s_end = string.Empty;


		public Report_CasherStat()
		{
			InitializeComponent();
			gridControl1.DataSource = dt_stat;
		}

		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_Report_CasherStat frm_stat = new Frm_Report_CasherStat();

			frm_stat.swapdata["BusinessObject"] = this;

			if (frm_stat.ShowDialog() == DialogResult.OK)
			{
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
			frm_stat.Dispose();
		}

		/// <summary>
		/// 查询数据
		/// </summary>
		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			int re = MiscAction.CasherStat(s_begin, s_end);
			if (re > 0)
			{
				gridView1.BeginUpdate();
				dt_stat.Rows.Clear();
				statAdapter.Fill(dt_stat);

				gridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn3.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn4.SummaryItem.DisplayFormat = "{0:N2}";

				gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn5.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn6.SummaryItem.DisplayFormat = "{0:N2}";

				gridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn7.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn8.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn8.SummaryItem.DisplayFormat = "{0:N2}";

				gridColumn9.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn9.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn10.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn10.SummaryItem.DisplayFormat = "{0:N2}";

				gridView1.EndUpdate();

			}
			this.Cursor = Cursors.Arrow;
		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
	}
}
