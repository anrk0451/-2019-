using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Threading;
using Bin2019.Misc;
using Bin2019.Dao;
using Bin2019.Domain;
using DevExpress.XtraSplashScreen;

namespace Bin2019.windows
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {             
            InitializeComponent(); 
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
			if (Envior.cur_userId == null)
				Application.Exit();
			else
				this.Dispose();
        }

        /// <summary>
        /// 验证登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string s_userCode, s_pwd;
            s_userCode = textEdit_user.Text;
            s_pwd = textEdit_pwd.Text;

            if (string.IsNullOrEmpty(s_userCode))
            {                
                MessageBox.Show("请输入用户代码!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                textEdit_user.Focus();
                return;
            }        
            if (string.IsNullOrEmpty(s_pwd))
            {
                MessageBox.Show("请输入密码!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEdit_pwd.Focus();
                return;
            }
            /////////////////////  检索 密码  ///////////////////////////////
            Uc01_dao uc01_dao = new Uc01_dao();
            Uc01 uc01 = uc01_dao.GetSingle(s => s.uc002 == s_userCode && s.status == "1");
            if(uc01 == null)
            {
                textEdit_user.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                textEdit_user.ErrorText = "用户不存在!";
                return;
            }else if(Tools.EncryptWithMD5(s_pwd) != uc01.uc004)
            {
                textEdit_pwd.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                textEdit_pwd.ErrorText = "密码错误!";
                return;
            }
            else
            {
                Envior.cur_userId = uc01.uc001;
                Envior.cur_userName = uc01.uc003;

                //开票点编码
                Envior.invoice_placecode = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["placeCode"].Value.ToString();
                //打印端口
                Envior.PRINT_PORT = Convert.ToInt32(ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["print_port"].Value);
 
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["lastusername"].Value = s_userCode;
                config.Save(ConfigurationSaveMode.Modified);

				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                DialogResult = DialogResult.OK;
				this.Close();
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            this.Focus();

            string lastuser = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["lastusername"].Value.ToString();
            if (!string.IsNullOrEmpty(lastuser))
            {
                textEdit_user.Text = lastuser;
                textEdit_pwd.Focus();
            }
            else
            {
                textEdit_user.Focus();
            }
        }
    }
}