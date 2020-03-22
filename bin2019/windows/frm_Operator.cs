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
using Bin2019.Dao;
using Bin2019.Domain;
using SqlSugar;
using Bin2019.Misc;
using Oracle.ManagedDataAccess.Client;

namespace Bin2019.windows
{
    public partial class frm_Operator : MyDialog
    {
        Ro01_ds ro01_ds = new Ro01_ds();
        Uc01 uc01 = null;
        Uc01_dao uc01_dao = new Uc01_dao();

        string s_uc001 = string.Empty;     //操作员编号


        public frm_Operator()
        {
            InitializeComponent();
            ro01_ds.ro01Adapter.Fill(ro01_ds.Ro01);    
        }

        private void Frm_Operator_Load(object sender, EventArgs e)
        {
            clbx_roles.DataSource = ro01_ds.Ro01;
            clbx_roles.DisplayMember = "RO003";
            clbx_roles.ValueMember = "RO001";
            ro01_ds.ro01Adapter.Fill(ro01_ds.Ro01);

            if (this.swapdata["action"].ToString() == "add")
            {
                this.Text = "新建用户";
                uc01 = new Uc01();
            }
            else if (this.swapdata["action"].ToString() == "edit")
            {
                this.Text = "编辑用户";
                s_uc001 = this.swapdata["uc001"].ToString();

                uc01 = uc01_dao.GetSingle(s => s.uc001 == s_uc001);
 
                txtedit_uc002.Text = uc01.uc002;
                txtedit_uc003.Text = uc01.uc003;
 
                txtedit_pwd.ReadOnly = true;
                txtedit_pwd2.ReadOnly = true;

                Ur_Mapper_dao ur_Mapper_dao = new Ur_Mapper_dao();
                List<Ur_Mapper> mapper = ur_Mapper_dao.GetList(s => s.uc001 == s_uc001);
                if(mapper.Count > 0)
                {
                    for (int i = 0; i < clbx_roles.ItemCount; i++)
                    {
                        string ro001 = clbx_roles.GetItemValue(i).ToString();
                        if (mapper.FindIndex(x => x.ro001.Equals(ro001)) >= 0)
                        {
                            clbx_roles.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void Sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Sb_ok_Click(object sender, EventArgs e)
        {
            //数据校验
            string s_uc002 = txtedit_uc002.Text;
            string s_uc003 = txtedit_uc003.Text;
            string s_uc004 = txtedit_pwd.Text;
            string s_uc004_2 = txtedit_pwd2.Text;

            if (String.IsNullOrEmpty(s_uc002))
            {
                txtedit_uc002.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_uc002.ErrorText = "用户登录代码必须输入!";
                txtedit_uc002.Focus();
                return;
            }

            if (String.IsNullOrEmpty(s_uc003))
            {
                txtedit_uc003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_uc003.ErrorText = "用户姓名必须输入!";
                txtedit_uc003.Focus();
                return;
            }

            if (this.swapdata["action"].ToString() == "add")
            {
                if (String.IsNullOrEmpty(s_uc004))
                {
                    txtedit_pwd.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtedit_pwd.ErrorText = "密码必须输入!";
                    txtedit_pwd.Focus();
                    return;
                }
                else if (!String.Equals(s_uc004, s_uc004_2))
                {
                    txtedit_pwd2.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtedit_pwd2.ErrorText = "密码不一致!";
                    txtedit_pwd2.Focus();
                    return;
                }
            }

			 
            /////// 保存过程 ///////
            uc01.uc002 = txtedit_uc002.Text;
            uc01.uc003 = txtedit_uc003.Text;

            List<string> ro001_list = new List<string>();
            foreach (DataRowView item in clbx_roles.CheckedItems)
            {
                ro001_list.Add(item["ro001"].ToString());
            }

            if (this.swapdata["action"].ToString() == "add")
            {
                uc01.uc001 = Tools.GetEntityPK("UC01");
                uc01.uc004 = Tools.EncryptWithMD5(s_uc004);
                if (CreateOperator(uc01, ro001_list.ToArray()) > 0)
                {
                    MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                if (UpdateOperator(uc01, ro001_list.ToArray()) > 0)
                {
                    MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }     
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <returns></returns>
        private int CreateOperator(Uc01 uc01,string[] rolesarry)
        {
            //用户编号
            OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
            op_uc001.Direction = ParameterDirection.Input;
            op_uc001.Value = uc01.uc001;

            //用户代码
            OracleParameter op_uc002 = new OracleParameter("ic_uc002", OracleDbType.Varchar2, 50);
            op_uc002.Direction = ParameterDirection.Input;
            op_uc002.Value = uc01.uc002;

            //用户姓名
            OracleParameter op_uc003 = new OracleParameter("ic_uc003", OracleDbType.Varchar2, 50);
            op_uc003.Direction = ParameterDirection.Input;
            op_uc003.Value = uc01.uc003;
            
            //用户密码
            OracleParameter op_uc004 = new OracleParameter("ic_uc004", OracleDbType.Varchar2, 50);
            op_uc004.Direction = ParameterDirection.Input;
            op_uc004.Value = uc01.uc004;

            
            //用户角色数组
            OracleParameter op_roles_arry = new OracleParameter("ic_roles", OracleDbType.Varchar2,500);
            op_roles_arry.Direction = ParameterDirection.Input;
            op_roles_arry.Value = string.Join("|", rolesarry );
 

            return SqlAssist.ExecuteProcedure("pkg_business.prc_CreateOperator", new OracleParameter[] { op_uc001,op_uc002,op_uc003,op_uc004,  op_roles_arry });
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        private int UpdateOperator(Uc01 uc01, string[] rolesarry)
        {
            //用户编号
            OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
            op_uc001.Direction = ParameterDirection.Input;
            op_uc001.Value = uc01.uc001;

            //用户代码
            OracleParameter op_uc002 = new OracleParameter("ic_uc002", OracleDbType.Varchar2, 50);
            op_uc002.Direction = ParameterDirection.Input;
            op_uc002.Value = uc01.uc002;

            //用户姓名
            OracleParameter op_uc003 = new OracleParameter("ic_uc003", OracleDbType.Varchar2, 50);
            op_uc003.Direction = ParameterDirection.Input;
            op_uc003.Value = uc01.uc003;
             

			//用户角色数组
			OracleParameter op_roles_arry = new OracleParameter("ic_roles", OracleDbType.Varchar2, 500);
            op_roles_arry.Direction = ParameterDirection.Input;
            op_roles_arry.Value = string.Join("|", rolesarry);


            return SqlAssist.ExecuteProcedure("pkg_business.prc_UpdateOperator", 
				new OracleParameter[] { op_uc001, op_uc002, op_uc003,op_roles_arry });
        }

		 
	}
}