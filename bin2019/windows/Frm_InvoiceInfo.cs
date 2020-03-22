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
using Bin2019.Misc;
using Bin2019.Action;

namespace Bin2019.windows
{
	public partial class Frm_InvoiceInfo : MyDialog
	{
		public Frm_InvoiceInfo()
		{
			InitializeComponent();
		}

		private void Frm_InvoiceInfo_Load(object sender, EventArgs e)
		{
			te_region.Text = Envior.invoice_region;						//区域代码
			te_deptcode.Text = Envior.invoice_dept;						//单位代码
			te_appid.Text = Envior.invoice_appid;						//应用账号
			te_version.Text = Envior.invoice_ver;						//版本号
			te_key.Text = Envior.invoice_key;							//签名私钥
			te_batchcode.Text = Envior.invoice_pBillBatchCode;          //票据代码
			te_kind.Text = Envior.invoice_kind;							//票据种类
		}


		private void b_ok_Click(object sender, EventArgs e)
		{
			string s_deptId = te_deptcode.Text;        //单位代码
			string s_region = te_region.Text;          //区域代码
			string s_appid = te_appid.Text;            //应用账号
			string s_ver = te_version.Text;            //版本号
			string s_key = te_key.Text;                //签名私钥
			string s_bcode = te_batchcode.Text;        //票据代码
			string s_kind = te_kind.Text;			   //票据种类

			if(MiscAction.SaveInvoiceBaseInfo(s_region,s_deptId,s_appid,s_ver,s_key,s_bcode,s_kind) == 1)
			{
				Envior.invoice_appid = s_appid;
				Envior.invoice_dept = s_deptId;
				Envior.invoice_key = s_key;
				Envior.invoice_region = s_region;
				Envior.invoice_ver = s_ver;
				Envior.invoice_pBillBatchCode = s_bcode;
				Envior.invoice_kind = s_kind;
				XtraMessageBox.Show("保存成功!","提示",MessageBoxButtons.OK);
			}
		}
	}
}