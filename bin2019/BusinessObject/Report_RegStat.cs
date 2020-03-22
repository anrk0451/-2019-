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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace Bin2019.BusinessObject
{
	public partial class Report_RegStat : BaseBusiness
	{
		private DataTable dt_stat = new DataTable();
		private OracleDataAdapter statAdapter =
			new OracleDataAdapter("select * from v_reg_stat where (to_char(jbrq,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);

		OracleParameter op_begin = null;
		OracleParameter op_end = null;

		public Report_RegStat()
		{
			InitializeComponent();
			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;

			statAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
			gridControl1.DataSource = dt_stat;
		}

		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_Report_regstat frm_1 = new Frm_Report_regstat();
			frm_1.swapdata["BusinessObject"] = this;
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				string s_begin = string.Empty;
				string s_end = string.Empty;

				if (this.swapdata["d_begin"] == null || this.swapdata["d_begin"] is System.DBNull)
				{
					s_begin = "1900-01-01";
				}
				else
				{
					s_begin = Convert.ToDateTime(this.swapdata["d_begin"]).ToString("yyyy-MM-dd");
				}

				if (this.swapdata["d_end"] == null || this.swapdata["d_end"] is System.DBNull)
				{
					s_end = "9999-12-31";
				}
				else
				{
					s_end = Convert.ToDateTime(this.swapdata["d_end"]).ToString("yyyy-MM-dd");
				}

				op_begin.Value = s_begin;
				op_end.Value = s_end;

				this.Cursor = Cursors.WaitCursor;
				gridView1.BeginUpdate();
				dt_stat.Rows.Clear();
				statAdapter.Fill(dt_stat);
				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;
				gridColumn14.Group();
				gridColumn12.Group();
				 
			}
			frm_1.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_stat.Rows.Clear();
			statAdapter.Fill(dt_stat);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
			//gridColumn14.Group();

		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

		private void Report_RegStat_Load(object sender, EventArgs e)
		{
			///加入分组合计
			gridView1.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
			GridGroupSummaryItem item = new GridGroupSummaryItem();
			//item.FieldName = "ROOMNAME";
			item.SummaryType = DevExpress.Data.SummaryItemType.Count;
			item.DisplayFormat = "小计{0}";
			gridView1.GroupSummary.Add(item);
 
		}
		 
	}
}
