using Bin2019.BaseObject;
using Bin2019.Misc;
using DevExpress.XtraGrid.Views.Base;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bin2019.windows
{
    public partial class Frm_InvoiceItem : MyDialog
    {
        DataTable dt_ii01 = new DataTable("II01");
        OracleDataAdapter ii01Adapter = new OracleDataAdapter("select * from ii01",SqlAssist.conn);
        OracleCommandBuilder builder = null; 

        public Frm_InvoiceItem()
        {
            InitializeComponent();
            builder = new OracleCommandBuilder(ii01Adapter);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 绘制行指示符
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

        private void Frm_InvoiceItem_Load(object sender, EventArgs e)
        {
            ii01Adapter.Fill(dt_ii01);
            gridControl1.DataSource = dt_ii01;
        }

        /// <summary>
        /// 新行初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            string newkey = string.Empty;
            newkey = Tools.GetEntityPK("II01");
            gridView1.SetRowCellValue(e.RowHandle, "II001", newkey);
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("II002"))       //服务名称
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "项目代码不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "II002") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "II002").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "代码已经存在!";
                            break;
                        }
                    }
                }
            }else if (colName.Equals("II003"))
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "项目名称不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "II003") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "II003").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "名称已经存在!";
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.ClearColumnErrors();
            if (!gridView1.PostEditor()) return;
            if (!gridView1.UpdateCurrentRow()) return;

			//保存前检查
			foreach(DataRow dr in dt_ii01.Rows)
			{
				if (!dr["II002"].ToString().StartsWith("F"))
				{
					gridView1.FocusedRowHandle = gridView1.FindRow(dr);
					MessageBox.Show("代码必须以F开头!","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}
			}
 
            ii01Adapter.Update(dt_ii01);
            MessageBox.Show("保存成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            if(rowHandle >= 0)
            {
                gridView1.DeleteRow(rowHandle);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.ShowEditor();
        }

        private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "II003").ToString();
            if (String.IsNullOrEmpty(value))
            {
                e.Valid = false;
                (sender as ColumnView).SetColumnError(gridView1.Columns["II003"], "名称不能为空!");
            }

			value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "II002").ToString();
			if (String.IsNullOrEmpty(value))
			{
				e.Valid = false;
				(sender as ColumnView).SetColumnError(gridView1.Columns["II002"], "代码不能为空!");
			}
		}
    }
}
