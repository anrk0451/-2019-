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
using System.Globalization;
using Bin2019.Domain;
using DevExpress.XtraPrinting;

namespace Bin2019.BusinessObject
{
	public partial class FinanceDaySearch : BaseBusiness
	{
		private DataTable dt_finance = new DataTable("FINANCE");
	 
		private OracleDataAdapter finAdapter =
			new OracleDataAdapter("select * from v_financeDay where (to_char(fa200,'yyyy-mm-dd') between :begin and :end) and fa003 like :fa003 and fa195 like :fa195 ", SqlAssist.conn);

		private DataTable dt_detail = new DataTable("DETAIL");
		private OracleDataAdapter deAdapter =
			new OracleDataAdapter("select * from v_findetail where sa010 = :sa010", SqlAssist.conn);

		OracleParameter op_begin = null;
		OracleParameter op_end = null;
		OracleParameter op_fa003 = null;
		OracleParameter op_fa195 = null;
		OracleParameter op_sa010 = null;
		OracleParameter op_fa100 = null;

		public FinanceDaySearch()
		{
			InitializeComponent();
		}

		private void FinanceDaySearch_Load(object sender, EventArgs e)
		{
			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;

			op_fa003 = new OracleParameter("fa003", OracleDbType.Varchar2, 80);
			op_fa003.Direction = ParameterDirection.Input;

			op_fa195 = new OracleParameter("fa195", OracleDbType.Varchar2, 3);
			op_fa195.Direction = ParameterDirection.Input;

			////////// 除了管理员,只能查看自己的收费记录 //////////////
			op_fa100 = new OracleParameter("fa100", OracleDbType.Varchar2, 10);
			op_fa100.Direction = ParameterDirection.Input;
			if (Envior.cur_userId == AppInfo.ROOTID)
			{
				op_fa100.Value = "%";
			}
			else
			{
				op_fa100.Value = Envior.cur_userId;
			}
			/////////////////////////////////////////////////////////////

			op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
			op_sa010.Direction = ParameterDirection.Input;

			finAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end, op_fa003,op_fa195 });
			deAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010 });

			gridControl1.DataSource = dt_finance;
			gridControl2.DataSource = dt_detail;

			gridControl1.Visible = true;

			this.Show_Condition();

		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_finance.Rows.Clear();

			finAdapter.Fill(dt_finance);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
		}


		private void Show_Condition()
		{
			Frm_financeDaySearch frm_1 = new Frm_financeDaySearch();
			frm_1.swapdata["BusinessObject"] = this;
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				frm_1.Dispose();
				string s_begin = string.Empty;
				string s_end = string.Empty;
				string s_fa003 = string.Empty;
				string s_fa195 = string.Empty;

				if (this.swapdata["dbegin"] == null)
				{
					s_begin = "1900/01/01";
				}
				else
				{
					s_begin = Convert.ToDateTime(this.swapdata["dbegin"]).ToString("yyyy/MM/dd");
				}

				if (this.swapdata["dend"] == null)
				{
					s_end = "9999/12/31";
				}
				else
				{
					s_end = Convert.ToDateTime(this.swapdata["dend"]).ToString("yyyy/MM/dd");
				}

				if (this.swapdata["FA003"] == null || string.IsNullOrEmpty(this.swapdata["FA003"].ToString()))
				{
					s_fa003 = "%";
				}
				else
				{
					s_fa003 = this.swapdata["FA003"].ToString() + "%";
				}

				s_fa195 = this.swapdata["invtype"].ToString();

				op_begin.Value = s_begin;
				op_end.Value = s_end;
				op_fa003.Value = s_fa003;
				op_fa195.Value = s_fa195;

				gridView1.BeginUpdate();
				dt_finance.Rows.Clear();

				finAdapter.Fill(dt_finance);

				gridCol_Fa004.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridCol_Fa004.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
				gridColumn5.SummaryItem.DisplayFormat = "共计 = {0:N0}笔";

				gridView1.EndUpdate();
			}
		}

		/// <summary>
		/// 输入查询条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Show_Condition();
		}

		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!gridView1.IsFindPanelVisible)
				gridView1.ShowFindPanel();
			else
				gridView1.HideFindPanel();
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

		 
		/// <summary>
		/// 行焦点改变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// 收款作废
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			string s_reason = string.Empty;
			  
			//DateTime dt_fa200;     //收费日期

			if (rowHandle >= 0)
			{
				//如果上线日期以前的收费,不能作废
				if( ((DateTime)gridView1.GetRowCellValue(rowHandle, "FA200")).ToString("yyyy-MM-dd").CompareTo(AppInfo.ONLINE_DATE) < 0)
				{
					MessageBox.Show("新财政接口前收费数据,不能作废!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}
				
				if (MessageBox.Show("确认要作废吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
				if (Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "FA004")) < 0)
				{
					MessageBox.Show("退费业务不能作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				Frm_RemoveFinReason frm_reason = new Frm_RemoveFinReason();
				if(frm_reason.ShowDialog() == DialogResult.OK)
				{
					s_reason = frm_reason.swapdata["reason"].ToString();
				}
				frm_reason.Dispose();


				if (gridView1.GetRowCellValue(rowHandle, "FA002").ToString() == "2")  //寄存业务
				{

					decimal count = (decimal)SqlAssist.ExecuteScalar("select count(*) from v_rc04 where rc001='" + s_rc001 + "'", null);
					if (count <= 1)
					{
						if (MessageBox.Show("此记录是唯一一次交费记录,作废此记录将删除寄存登记信息,是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
					}
				}

				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				string s_fa195 = gridView1.GetRowCellValue(rowHandle, "FA195").ToString();   //发票类型
				string s_fa190 = gridView1.GetRowCellValue(rowHandle, "FA190").ToString();	 //开票标志
				string s_retCode = string.Empty;

				int re = MiscAction.FinanceRemove(s_fa001, s_reason,Envior.cur_userId);

				//MessageBox.Show("debug1");

				if (re > 0 && s_fa195 == "T")
				{
					////////// 税务发票作废 ///////////////////////////////////////////////////
					XtraMessageBox.Show("作废成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.RefreshData();
				}
				else if(re > 0 && s_fa195 == "F" && s_fa190 == "1")
				{
					//财政发票作废
					if (InvoiceAction.RemoveInvoice(s_fa001, s_reason) < 0)
					{
						return;
					}
					else
					{
						XtraMessageBox.Show("作废成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.RefreshData();
					}
				}
				//else  if(re>0 && s_fa195 == "T")
				//{
				//	XtraMessageBox.Show("作废成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//	this.RefreshData();
				//}
			}
		}

		/// <summary>
		/// 补开发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string s_fa001 = string.Empty;
			string s_fa195 = string.Empty;
			int rowHandle = gridView1.FocusedRowHandle;

			if (rowHandle >= 0)
			{
				//如果上线日期以前的收费,不能补开
				if (((DateTime)gridView1.GetRowCellValue(rowHandle, "FA200")).ToString("yyyy-MM-dd").CompareTo(AppInfo.ONLINE_DATE) < 0)
				{
					MessageBox.Show("新财政接口前收费数据,不能操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}


				if (gridView1.GetRowCellValue(rowHandle, "FA190").ToString() == "1")
				{
					MessageBox.Show("当前记录已开发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				s_fa195 = gridView1.GetRowCellValue(rowHandle, "FA195").ToString();
				s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				if(s_fa195 == "T")
				{
					XtraMessageBox.Show("只有财政发票可以补开!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}
				else if(s_fa195 == "F")
				{
					this.ReInvoice_Fin(s_fa001);
				}
			}
		}


		/// <summary>
		/// 补开税务发票
		/// </summary>
		private void ReInvoice_Tax(string fa001)
		{
			if (!Envior.canInvoice)
			{
				MessageBox.Show("当前用户没有打印发票权限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			 
			this.RefreshData();
		}

		/// <summary>
		/// 补开财政发票
		/// </summary>
		/// <param name="fa001"></param>
		private void ReInvoice_Fin(string fa001)
		{
			if (MessageBox.Show("是否现在开【财政发票】?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				//XtraMessageBox.Show(Envior.invoice_pBillBatchCode, "代码");
				if (InvoiceAction.GetInvoiceNextNum(Envior.invoice_pBillBatchCode) > 0)
				{
					string s_tip = "下一张发票号:" + Envior.NEXT_BILL_NUM + "\r\n" +
								   "发票代码:" + Envior.NEXT_BILL_CODE;
					if (MessageBox.Show(s_tip, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						if (InvoiceAction.Invoice(fa001) == 1)
						{
							this.RefreshData();
						}
					}
				}
			}
		}

		/// <summary>
		/// 打印税务结算单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if(rowHandle >= 0)
			{
				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				PrtServAction.Print_JSD(s_fa001);
			}
		}

		/// <summary>
		/// 打印清单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
 
		}

		/// <summary>
		/// 转换发票类型
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if(e.Column.FieldName.ToUpper() == "FA195")
			{
				if (e.Value.ToString() == "F")
					e.DisplayText = "财政发票";
				else if (e.Value.ToString() == "T")
					e.DisplayText = "税务发票";
			}
		}

		/// <summary>
		/// 打印 财政发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			string s_fa195 = string.Empty;
			string s_fa001 = string.Empty;

			if (rowHandle >= 0)
			{
				//如果上线日期以前的收费,不能作废
				if (((DateTime)gridView1.GetRowCellValue(rowHandle, "FA200")).ToString("yyyy-MM-dd").CompareTo(AppInfo.ONLINE_DATE) < 0)
				{
					MessageBox.Show("新财政接口前收费数据,不能操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}


				if ( !(gridView1.GetRowCellValue(rowHandle, "FA190").ToString() == "1"))
				{
					MessageBox.Show("当前记录还未开具发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				s_fa195 = gridView1.GetRowCellValue(rowHandle, "FA195").ToString();
				s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				if (s_fa195 == "T")
				{
					XtraMessageBox.Show("财政发票才可以打印!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}
				else if (s_fa195 == "F")
				{
					InvoiceAction.PrintInvoice(s_fa001);
				}
			}
		}

		private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

		private void barEditItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}

		private void OnlyMe_Filter()
		{
			if (Convert.ToBoolean(toggle_onlyme.EditValue))
			{
				XtraMessageBox.Show("debug");
				//gridView1.ActiveFilterString = "FA100 = '" + Envior.cur_userId + "'";
			}
			else
			{
				//gridView1.ActiveFilter.Clear();
			}
		}

		private void toggle_onlyme_EditValueChanged(object sender, EventArgs e)
		{
			this.OnlyMe_Filter();
		}
	}  
}
