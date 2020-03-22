using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bin2019.BaseObject;
using Bin2019.DataSet;
using Bin2019.Misc;
using Bin2019.Action;
using Bin2019.Domain;
using Oracle.ManagedDataAccess.Client;

namespace Bin2019.windows
{
	public partial class Frm_fireSettle : MyDialog
	{
		Sa01_ds sa01_ds = null;
		string AC001 = string.Empty;
		string invtype = string.Empty;
		List<int> rowList;
		DataTable dt_source;

		public Frm_fireSettle()
		{
			InitializeComponent();
		}

		private void Frm_fireSettle_Load(object sender, EventArgs e)
		{
			AC001 = this.swapdata["AC001"].ToString();
			sa01_ds = this.swapdata["dataset"] as Sa01_ds;
			rowList = this.swapdata["rowList"] as List<int>;
			invtype = this.swapdata["invtype"].ToString();     //发票类型 F-财政发票 T-税务发票

			///拷贝要结算的记录!!!
			dt_source = sa01_ds.Sa01.Clone();
			foreach (int i in rowList)
			{
				dt_source.Rows.Add(sa01_ds.Sa01.Rows[i].ItemArray);
			}

			gridControl1.DataSource = dt_source;
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

		private void B_ok_Click(object sender, EventArgs e)
		{
			string cuname = string.Empty;
			string cuid = string.Empty;
			
			//OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			//op_ac001.Direction = ParameterDirection.Input;
			//op_ac001.Value = AC001;
			//cuname = SqlAssist.ExecuteScalar("select ac003 from ac01 where ac001 = :ac001", new OracleParameter[] { op_ac001 }).ToString();

			OracleDataReader reader = SqlAssist.ExecuteReader("select ac003,ac014 from ac01 where ac001 = '" + AC001 + "'");
			if (reader.Read())
			{
				cuname = reader["AC003"].ToString();
				cuid = reader["AC014"].ToString();
			}
			reader.Dispose();
 
			if(invtype == "F")
			{
				//输入交款人信息
				Frm_InvoiceClientName frm_clientName = new Frm_InvoiceClientName(cuname,cuid);
				if (frm_clientName.ShowDialog() == DialogResult.OK)
				{
					cuname = frm_clientName.swapdata["cuname"].ToString();
					cuid = frm_clientName.swapdata["cuid"].ToString();
					frm_clientName.Dispose();
				}
				else
				{
					frm_clientName.Dispose();
					return;
				}
			}
			

			string settleId = Tools.GetEntityPK("FA01");
			List<string> sa001_list = new List<string>();
			foreach (DataRow r in dt_source.Rows)
			{
				sa001_list.Add(r["SA001"].ToString());
			}

			int result = FireAction.FireBusinessSettle(settleId,
													   AC001,
													   cuname,
													   cuid,
													   sa001_list.ToArray(),
													   Envior.cur_userId,
													   invtype
			);
			if (result > 0)
			{
				b_ok.Enabled = false;

				MessageBox.Show("结算办理成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

				int fire_row = gridView1.LocateByValue("SA002", "06");
				//如果有火化,打印火化证明
				if (fire_row >= 0)
				{   //打印火化证明
					if(MessageBox.Show("现在打印火化证明!", "提示", MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
						PrtServAction.Print_HHZM(AC001);
				}

				if(invtype == "F")  //财政发票
				{
					if (MessageBox.Show("是否现在开【财政发票】?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						if (InvoiceAction.GetInvoiceNextNum(Envior.invoice_pBillBatchCode) > 0)
						{
							string s_tip = "下一张发票号:" + Envior.NEXT_BILL_NUM + "\r\n" +
										   "发票代码:" + Envior.NEXT_BILL_CODE;
							if (MessageBox.Show(s_tip, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
							{
								if (InvoiceAction.Invoice(settleId) == 1)
								{
									//XtraMessageBox.Show("发票开具成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
								}
							}
						}
					}
				}
				else
				{
					//// 非财政发票结算:打印结算单
					if (MessageBox.Show("现在打印【结算单】吗?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						PrtServAction.Print_JSD(settleId);
					}
				}
                
                   
                DialogResult = DialogResult.OK;
				this.Dispose();
			}
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}