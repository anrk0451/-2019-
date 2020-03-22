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
using Oracle.ManagedDataAccess.Client;
using Bin2019.Misc;

namespace Bin2019.windows
{
	public partial class Frm_ReportCheckin : MyDialog
	{
		private BaseBusiness bo = null;

		public Frm_ReportCheckin()
		{
			InitializeComponent();
		}

		private void Frm_ReportCheckin_Load(object sender, EventArgs e)
		{
			bo = this.swapdata["BusinessObject"] as BaseBusiness;

			dateEdit2.EditValue = DateTime.Today;
			dateEdit1.EditValue = DateTime.Today.AddMonths(-1);
			te_ac003.Focus();
	 
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			bo.swapdata["dbegin"] = dateEdit1.EditValue.ToString();
			bo.swapdata["dend"] = dateEdit2.EditValue.ToString();
			bo.swapdata["ac003"] = te_ac003.Text;
 
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}