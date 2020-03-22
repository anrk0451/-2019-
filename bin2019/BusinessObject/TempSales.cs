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
using Bin2019.Misc;
using Oracle.ManagedDataAccess.Client;
using Bin2019.windows;
using Bin2019.Action;
using Bin2019.DataSet;
using DevExpress.XtraGrid.Views.Base;
using Bin2019.Domain;

namespace Bin2019.BusinessObject
{
	public partial class TempSales : BaseBusiness
	{
		Sa01_ds sa01_ds = new Sa01_ds();

		public TempSales()
		{
			InitializeComponent();
		}

		private void TempSales_Load(object sender, EventArgs e)
		{

			gridControl1.DataSource = sa01_ds.Sa01;
			//sa01_ds.sa01Adapter.Fill(sa01_ds.Sa01);			 
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

		private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.FocusedRowHandle >= 0)
			{
				EditItem(gridView1.FocusedRowHandle);
			}
		}

		private void EditItem(int rowHandle)
		{
			Frm_salesEdit frm_modi = new Frm_salesEdit();
			frm_modi.swapdata["DATAROW"] = sa01_ds.Sa01.Rows[gridView1.GetDataSourceRowIndex(rowHandle)];
			frm_modi.ShowDialog();
		}

		private void GridView1_DoubleClick(object sender, EventArgs e)
		{
			int row = -1;
			if ((row = (sender as ColumnView).FocusedRowHandle) >= 0)
			{
				this.EditItem(row);
			}
		}

		/// <summary>
		/// 删除项目
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;

			if (rowHandle < 0)
			{
				MessageBox.Show("请先选择要删除的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			gridView1.DeleteRow(rowHandle); 
		}

		
		 
		private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_businessMisc frm_misc = new Frm_businessMisc();
			frm_misc.swapdata["businessObject"] = this;
			frm_misc.swapdata["dataset"] = sa01_ds;


			if (frm_misc.ShowDialog() == DialogResult.OK)
			{
				List<string> itemId_list = this.swapdata["itemIdList"] as List<string>;
				List<string> itemType_list = this.swapdata["itemTypeList"] as List<string>;
				List<decimal> price_list = this.swapdata["priceList"] as List<decimal>;
				List<int> nums_list = this.swapdata["numsList"] as List<int>;
				int re = 0;

				for (int i = 0; i < itemId_list.Count; i++)
				{
					if (itemType_list[i] == "10" || itemType_list[i] == "11")
					{
						re = gridView1.LocateByValue("SA002", itemType_list[i]);
						if (re > 0)
						{

							if (itemType_list[i] == "10")
							{
								if (MessageBox.Show("已经选择【骨灰盒】,是否要替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
							}
							else if (itemId_list[i] == "11")
							{
								if (MessageBox.Show("已经选择【纸棺】,是否要替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
							}
							gridView1.DeleteRow(re);
						}
					}

					re = gridView1.LocateByValue("SA004", itemId_list[i]);
					if (re >= 0)
					{
						if (MessageBox.Show("【" + gridView1.GetRowCellValue(re, "SA003").ToString() + "】已经存在,要替换吗?",
							"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) continue;
						gridView1.DeleteRow(re);
					}

					DataRow dr = sa01_ds.Sa01.Rows.Add();
					dr["SA003"] = MiscAction.GetItemFullName(itemId_list[i]);
					dr["SA002"] = itemType_list[i];
					dr["SA004"] = itemId_list[i];
					dr["PRICE"] = price_list[i];
					dr["SA005"] = "1";
					dr["NUMS"] = nums_list[i];
					dr["SA007"] = price_list[i] * nums_list[i];
					dr["INVOICECODE"] = InvoiceAction.GetItemInvoiceCode(itemType_list[i], itemId_list[i]);   //发票编码(含财政、税务)
					dr.EndEdit();
				}
				//RefreshSalesData();
			}
		}

		/// <summary>
		/// 财政项目结算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.RowCount == 0)
			{
				MessageBox.Show("没选择项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			///检查是否有价格为0 的项目、以及是否有财政发票编码
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				if (Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRICE")) <= 0)
				{
					MessageBox.Show("尚有未输入价格的项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					gridView1.FocusedRowHandle = i;
					return;
				}
				if(string.IsNullOrEmpty(gridView1.GetRowCellValue(i, "INVOICECODE").ToString()))
				{
					XtraMessageBox.Show("第" + (i+1).ToString() + "行项目没有财政发票编码", "提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					gridView1.FocusedRowHandle = i;
					return;
				}
			}

			string s_cuname = string.Empty;
			string s_cuid = string.Empty;

			if (string.IsNullOrEmpty(textEdit1.Text))
			{
				textEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit1.ErrorText = "请输入交款人(单位)!";
				return;
			}
			else if (string.IsNullOrEmpty(textEdit2.Text))
			{
				textEdit2.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit2.ErrorText = "请输入交款人身份证号!";
				return;
			}
			else
			{
				s_cuname = textEdit1.EditValue.ToString();
				s_cuid = textEdit2.Text;
			}

			List<string> itemId_List = new List<string>();
			List<string> itemType_List = new List<string>();
			List<decimal> prict_List = new List<decimal>();
			List<decimal> nums_List = new List<decimal>();
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				itemId_List.Add(gridView1.GetRowCellValue(i, "SA004").ToString());
				itemType_List.Add(gridView1.GetRowCellValue(i, "SA002").ToString());
				prict_List.Add(decimal.Parse(gridView1.GetRowCellValue(i, "PRICE").ToString()));
				nums_List.Add(decimal.Parse(gridView1.GetRowCellValue(i, "NUMS").ToString()));
			}
			string settleId = Tools.GetEntityPK("FA01");
			int re = FireAction.TempSalesSettle(
						s_cuname, s_cuid, settleId, itemId_List.ToArray(), itemType_List.ToArray(), prict_List.ToArray(), nums_List.ToArray(), Envior.cur_userId,"F");
			if (re > 0)
			{
				if (MessageBox.Show("办理成功!现在打印【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					if (InvoiceAction.GetInvoiceNextNum(Envior.invoice_pBillBatchCode) > 0)
					{
						string s_tip = "下一张发票号:" + Envior.NEXT_BILL_NUM + "\r\n" +
									   "发票代码:" + Envior.NEXT_BILL_CODE;
						if (MessageBox.Show(s_tip, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
						{
							InvoiceAction.Invoice(settleId);
						}
					}
				}

				////打印结算单
				//if (MessageBox.Show("现在打印【结算单】吗?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				//{
				//	PrtServAction.Print_JSD(settleId, this.Handle.ToInt32());
				//}

				textEdit1.Text = "";
				textEdit2.Text = "";
				sa01_ds.Sa01.Rows.Clear();
			}
		}

		/// <summary>
		/// 税务项目结算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.RowCount == 0)
			{
				MessageBox.Show("没选择项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			///检查是否有价格为0 的项目、以及是否有财政发票编码
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				if (Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRICE")) <= 0)
				{
					MessageBox.Show("尚有未输入价格的项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					gridView1.FocusedRowHandle = i;
					return;
				}
				if (!String.IsNullOrEmpty(gridView1.GetRowCellValue(i, "INVOICECODE").ToString()))
				{
					XtraMessageBox.Show("第" + (i + 1).ToString() + "行项目是财政发票项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}

			string s_cuname = string.Empty;
			string s_cuid = string.Empty;

			if (string.IsNullOrEmpty(textEdit1.Text))
			{
				textEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit1.ErrorText = "请输入交款人(单位)!";
				return;
			}			
			else
			{
				s_cuname = textEdit1.EditValue.ToString();
				s_cuid = textEdit2.Text;
			}

			List<string> itemId_List = new List<string>();
			List<string> itemType_List = new List<string>();
			List<decimal> prict_List = new List<decimal>();
			List<decimal> nums_List = new List<decimal>();
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				itemId_List.Add(gridView1.GetRowCellValue(i, "SA004").ToString());
				itemType_List.Add(gridView1.GetRowCellValue(i, "SA002").ToString());
				prict_List.Add(decimal.Parse(gridView1.GetRowCellValue(i, "PRICE").ToString()));
				nums_List.Add(decimal.Parse(gridView1.GetRowCellValue(i, "NUMS").ToString()));
			}
			string settleId = Tools.GetEntityPK("FA01");
			int re = FireAction.TempSalesSettle(
						s_cuname, s_cuid, settleId, itemId_List.ToArray(), itemType_List.ToArray(), prict_List.ToArray(), nums_List.ToArray(), Envior.cur_userId,"T");
			if (re > 0)
			{
				//打印结算单
				if (MessageBox.Show("办理成功!现在打印【结算单】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					PrtServAction.Print_JSD(settleId);
				}
				textEdit1.Text = "";
				textEdit2.Text = "";
				sa01_ds.Sa01.Rows.Clear();
			}
		}

		private void panelControl1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
