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

namespace Bin2019.windows
{
	public partial class Frm_Report_regstat : MyDialog
	{
		private BaseBusiness bo = null;
		public Frm_Report_regstat()
		{
			InitializeComponent();
		}

		private void Frm_Report_regstat_Load(object sender, EventArgs e)
		{
			bo = this.swapdata["BusinessObject"] as BaseBusiness;
			dateEdit2.EditValue = DateTime.Today;
			dateEdit1.EditValue = DateTime.Today.AddMonths(-1);
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			bo.swapdata["d_begin"] = dateEdit1.EditValue.ToString();
			bo.swapdata["d_end"] = dateEdit2.EditValue.ToString();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
