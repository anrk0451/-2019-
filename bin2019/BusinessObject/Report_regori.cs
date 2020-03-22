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
	public partial class Report_regori : BaseBusiness
	{
		private DataTable dt_ori = new DataTable();
		private OracleDataAdapter oriAdapter =
			new OracleDataAdapter("select * from v_orireport where (to_char(rc200,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);

		OracleParameter op_begin = null;
		OracleParameter op_end = null;

		public Report_regori()
		{
			InitializeComponent();
			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;


			oriAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
			gridControl1.DataSource = dt_ori;

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

		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_Report_RegOri frm_1 = new Frm_Report_RegOri();
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
				dt_ori.Rows.Clear();
				oriAdapter.Fill(dt_ori);
				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;
			}
			frm_1.Dispose();
		}

		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_ori.Rows.Clear();
			oriAdapter.Fill(dt_ori);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
	}
}
