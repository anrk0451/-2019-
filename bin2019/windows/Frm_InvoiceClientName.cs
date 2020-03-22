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

namespace Bin2019.windows
{
	public partial class Frm_InvoiceClientName : MyDialog
	{
		private string cuname = string.Empty;
		private string cuid = string.Empty;
		public Frm_InvoiceClientName()
		{
			InitializeComponent();
		}

		public Frm_InvoiceClientName(string cuname)
		{
			this.cuname = cuname;
			InitializeComponent();
		}

		public Frm_InvoiceClientName(string cuname,string cuid)
		{
			this.cuname = cuname;
			this.cuid = cuid;
			InitializeComponent();
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			string cuname = te_cuname.Text;
			string cuid = te_cuid.Text;
			if (string.IsNullOrEmpty(cuname))
			{
				te_cuname.ErrorText = "请输入交款人!";
				te_cuname.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				return;
			}
			if (string.IsNullOrEmpty(cuid))
			{
				te_cuid.ErrorText = "请输入交款人身份证号!";
				te_cuid.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				return;
			}
			this.swapdata["cuname"] = cuname;
			this.swapdata["cuid"] = cuid;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void Frm_InvoiceClientName_Load(object sender, EventArgs e)
		{
			te_cuname.Text = this.cuname;
			te_cuid.Text = this.cuid;
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}