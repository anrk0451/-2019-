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
	public partial class Frm_Report_CasherStat : MyDialog
	{
		private BaseBusiness bo = null;

		public Frm_Report_CasherStat()
		{
			InitializeComponent();
		}

		private void Form_Report_CasherStat_Load(object sender, EventArgs e)
		{
			bo = this.swapdata["BusinessObject"] as BaseBusiness;
			dateEdit2.EditValue = DateTime.Today;
			dateEdit1.EditValue = DateTime.Today;
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			bo.swapdata["dbegin"] = dateEdit1.EditValue.ToString();
			bo.swapdata["dend"] = dateEdit2.EditValue.ToString();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}