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
using Bin2019.DataSet;
using Bin2019.windows;
using Oracle.ManagedDataAccess.Client;
using Bin2019.Misc;
using DevExpress.XtraPrinting;

namespace Bin2019.BusinessObject
{
	public partial class Report_FireCheckin : BaseBusiness
	{
		private DataTable dt_in = new DataTable();
		private OracleDataAdapter inAdapter =
			new OracleDataAdapter("select * from v_Checkin where (to_char(ac200,'yyyy-mm-dd') between :begin and :end) and ac003 like :ac003", SqlAssist.conn);

		OracleParameter op_begin = null;
		OracleParameter op_end = null;
		OracleParameter op_ac003 = null;
  
		public Report_FireCheckin()
		{
			InitializeComponent();
			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;

			op_ac003 = new OracleParameter("ac003", OracleDbType.Varchar2, 20);
			op_ac003.Direction = ParameterDirection.Input;
 
			inAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end, op_ac003 });
			gridControl1.DataSource = dt_in;
		}


		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_ReportCheckin frm_1 = new Frm_ReportCheckin();
			frm_1.swapdata["BusinessObject"] = this;
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				string s_begin = string.Empty;
				string s_end = string.Empty;
				string s_ac003 = string.Empty;

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

				if (this.swapdata["ac003"] == null || string.IsNullOrEmpty(this.swapdata["ac003"].ToString()))
				{
					s_ac003 = "%";
				}
				else
				{
					s_ac003 = this.swapdata["ac003"].ToString() + "%";
				}
 
				op_begin.Value = s_begin;
				op_end.Value = s_end;
				op_ac003.Value = s_ac003;

				this.Cursor = Cursors.WaitCursor;
				gridView1.BeginUpdate();
				dt_in.Rows.Clear();
				inAdapter.Fill(dt_in);
				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;
			}
			frm_1.Dispose();
		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_in.Rows.Clear();
			inAdapter.Fill(dt_in);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
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
