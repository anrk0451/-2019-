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
using Bin2019.BusinessObject;

namespace Bin2019.windows
{
	public partial class Frm_business03 : MyDialog
	{
		BaseBusiness bo = null;
		DataView dv_xxs;
		Sa01_ds sa01_ds = null;
 

		public Frm_business03()
		{
			InitializeComponent();
		}

		private void Frm_business03_Load(object sender, EventArgs e)
		{
			bo = this.swapdata["businessObject"] as BaseBusiness;
			sa01_ds = this.swapdata["dataset"] as Sa01_ds;
			//AC001 = this.swapdata["AC001"].ToString();
		 

			dv_xxs = new DataView(sa01_ds.Si01);
			dv_xxs.RowFilter = "item_type='03' ";
			gridControl1.DataSource = dv_xxs;
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			int[] handle_arry = gridView1.GetSelectedRows();
			if (handle_arry.Length == 0)
			{
				MessageBox.Show("请先选择项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			List<string> itemIdList = new List<string>();
			foreach (int r in handle_arry)
			{
				itemIdList.Add(gridView1.GetRowCellValue(r, "ITEM_ID").ToString());
			}

			bo.swapdata["xxs"] = itemIdList;
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}