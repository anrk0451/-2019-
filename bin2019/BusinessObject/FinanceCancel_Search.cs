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

namespace Bin2019.BusinessObject
{
	public partial class FinanceCancel_Search : BaseBusiness
	{
		private DataTable dt_fin = new DataTable();
		private OracleDataAdapter finAdapter =
			new OracleDataAdapter("select * from v_financeremovereport where (to_char(rfa200,'yyyy-mm-dd') between :begin and :end) ", SqlAssist.conn);

		OracleParameter op_begin = null;
		OracleParameter op_end = null;

		public FinanceCancel_Search()
		{
			InitializeComponent();

			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;
 
			finAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
			gridControl1.DataSource = dt_fin;
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

		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_FinanceRoll_Report frm_1 = new Frm_FinanceRoll_Report();
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

				 
				op_begin.Value = s_begin;
				op_end.Value = s_end;
 
				this.Cursor = Cursors.WaitCursor;
				gridView1.BeginUpdate();
				dt_fin.Rows.Clear();
				finAdapter.Fill(dt_fin);
				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;
			}
			frm_1.Dispose();
		}
	}
}
