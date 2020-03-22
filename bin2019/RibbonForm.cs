using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using Bin2019.Domain;
using DevExpress.XtraTab;
using Bin2019.Dao;
using Bin2019.windows;
using Bin2019.BaseObject;
using Bin2019.Misc;
using System.Threading;
using DevExpress.XtraTab.ViewInfo;
using Bin2019.DataSet;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Bin2019.Action;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using DevExpress.XtraEditors;
//using PrtServ;

namespace Bin2019
{
	public partial class RibbonForm : DevExpress.XtraBars.Ribbon.RibbonForm
	{
		[DllImport("user32.dll", EntryPoint = "FindWindow")]
		private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

		//打印服务进程
		Process printprocess = new Process();
 
		public static SocketClient socket = new SocketClient();
 
		public Dictionary<string, Object> swapdata { get; set; }

		//追踪已经打开的Tab页
		private Dictionary<string, Bo01> businessTab = new Dictionary<string, Bo01>();
		private Dictionary<string, XtraTabPage> openedTabPage = new Dictionary<string, XtraTabPage>();

		public RibbonForm()
		{
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            Thread.Sleep(2000);
            SplashScreenManager.CloseForm();
            InitializeComponent();

			//启动打印服务进程			 
			printprocess.StartInfo.FileName = "pbnative.exe";
			printprocess.Start();

			swapdata = new Dictionary<string, object>();
		}

		private void RibbonForm_Load(object sender, EventArgs e)
		{
            //// 读取业务对象
            Bo01_dao bo01_dao = new Bo01_dao();
			List<Bo01> bo01_rows = bo01_dao.GetList(it => it.bo004 == "x");
			businessTab = bo01_rows.ToDictionary(key => key.bo001, value => value);

			Login login = new Login();
			login.ShowDialog();

			if (login.DialogResult == DialogResult.OK)  //登录成功处理..........
			{
				barStaticItem2.Caption = Envior.cur_userName;
				bs_version.Caption = AppInfo.AppVersion;
 
				//读取财政发票基础信息
				this.ReadInvoiceBaseInfo();
				login.Dispose();
			}

			//连接打印服务
			this.ConnectPrtServ();
		}


		/// <summary>
		/// 连接打印服务
		/// </summary>
		private void ConnectPrtServ()
		{
			IntPtr hwnd = FindWindow(null, "prtserv");
			if (hwnd != IntPtr.Zero)
			{
				Envior.prtservHandle = hwnd;
				//Envior.socket = new SocketClient();
				//Envior.socket.sendMsg("hello tcp ip");
			}
			else
			{
				XtraMessageBox.Show("没有找到打印服务进程,不能打印!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		/// <summary>
		/// 读取财政发票信息
		/// </summary>
		private void ReadInvoiceBaseInfo()
        {
			 
            DataTable dt_sp01 = new DataTable("SP01");
            OracleDataAdapter sp01Adapter = new OracleDataAdapter("select * from sp01 where sp001 <= '0000000007' ", SqlAssist.conn);
            sp01Adapter.Fill(dt_sp01);

			//MessageBox.Show(dt_sp01.Rows.Count.ToString());
            foreach(DataRow dr in dt_sp01.Rows)
            {
                if (dr["SP001"].ToString() == "0000000001")
                    Envior.invoice_region = dr["SP005"].ToString();  //区域代码
                else if (dr["SP001"].ToString() == "0000000002")
                    Envior.invoice_dept = dr["SP005"].ToString();    //单位代码
                else if (dr["SP001"].ToString() == "0000000003")
                    Envior.invoice_appid = dr["SP005"].ToString();   //应用帐号
                else if (dr["SP001"].ToString() == "0000000004")
                    Envior.invoice_ver = dr["SP005"].ToString();     //版本号
                else if (dr["SP001"].ToString() == "0000000005")
                    Envior.invoice_key = dr["SP005"].ToString();     //签名私钥
                else if (dr["SP001"].ToString() == "0000000006")
                    Envior.invoice_pBillBatchCode = dr["SP005"].ToString();  //票据代码
				else if (dr["SP001"].ToString() == "0000000007")
					Envior.invoice_kind = dr["SP005"].ToString();    //纸质票据种类
			}

            sp01Adapter.Dispose();
            dt_sp01.Dispose();
        }

		/// <summary>
		/// 打开业务对象(如果没有则创建)
		/// </summary>
		public void openBusinessObject(string bo001)
		{
			openBusinessObject(bo001, null);
		}

		/// <summary>
		/// 打开业务对象(如果没有则创建)
		/// </summary>
		public void openBusinessObject(string bo001, object parm)
		{
			if (openedTabPage.ContainsKey(bo001))
			{
				xtraTabControl1.SelectedTabPage = openedTabPage[bo001];
				if (parm != null)
				{
					foreach (Control control in openedTabPage[bo001].Controls)
					{
						if (control is BaseBusiness)
						{
							((BaseBusiness)control).swapdata["parm"] = parm;
							((BaseBusiness)control).Business_Init();
							return;
						}
					}
				}
			}
			else //如果尚未打开，则new
			{
				XtraTabPage newPage = new XtraTabPage();
				newPage.Text = businessTab[bo001].bo003;
				newPage.Tag = bo001;
                newPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;

				BaseBusiness bo = (BaseBusiness)Activator.CreateInstance(Type.GetType("Bin2019.BusinessObject." + bo001));

				Envior.mform = this;

				bo.Dock = DockStyle.Fill;
				bo.Parent = newPage;
				bo.swapdata.Add("parm", parm);

				newPage.Controls.Add(bo);

				xtraTabControl1.TabPages.Add(newPage);
				xtraTabControl1.SelectedTabPage = newPage;

				bo.Business_Init();

				////////登记已打开 Tabpage ////////
				openedTabPage.Add(bo001, newPage);

			}
		}

		/// <summary>
		/// 数据项维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("DataDict");
		}

        private void BarButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("ServiceProduct");
        }

        private void BarButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("Roles");
        }

        private void XtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;

            XtraTabPage curPage = (XtraTabPage)arg.Page;
            ///////// 清除页面追踪 ////////
            openedTabPage.Remove(curPage.Tag.ToString());

            curPage.Dispose();
        }

		/// <summary>
		/// 进灵登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void BarButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "01010") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			Frm_ac01 frm_ac01 = new Frm_ac01();
            frm_ac01.swapdata["action"] = "add";

            Ac01_ds ac01_ds = new Ac01_ds();
            frm_ac01.swapdata["dataset"] = ac01_ds;

            frm_ac01.ShowDialog();
            frm_ac01.Dispose();            
        }
 

        private void BarButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("Combo");
        }

        private void BarButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("RegStru");
        }

		/// <summary>
		/// 税务基础信息设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void BarButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
			//this.openBusinessObject("InvoiceItems");
			Frm_InvoiceInfo frm_1 = new Frm_InvoiceInfo();
			frm_1.ShowDialog();
        }

        private void BarButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "01040") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("FireCheckinBrow");
        }

		private void BarButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "01050") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("BusinessHandleBrow");
		}

		private void BarButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "01090") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("TempSales");
		}

		private void BarButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "02010") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("Register_brow");
		}

		/// <summary>
		/// 操作员管理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
		{
			if(Envior.cur_userId != AppInfo.ROOTID)
			{
				XtraMessageBox.Show("权限不足!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
			try
			{
				this.openBusinessObject("Operator");
			}
			catch (Exception ee)
			{
				XtraMessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			
		}

		/// <summary>
		/// 角色管理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (Envior.cur_userId != AppInfo.ROOTID)
			{
				XtraMessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("Roles");
		}


		/// <summary>
		/// 主窗口关闭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			SqlAssist.DisConnect();

			//关闭关联的打印进程
			if (!printprocess.HasExited) printprocess.Kill();
		}

		/// <summary>
		/// 寄存室数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03040") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("RegisterRoomData");
		}

		/// <summary>
		/// 每日收费明细
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("FinanceDaySearch");
		}

		/// <summary>
		/// 重新连接税务金卡
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
		{
			//n_prtserv n_print = new n_prtserv();
			//StringBuilder s_pdata = new StringBuilder(100);
			//s_pdata.Append("123" + "\t");
			//s_pdata.Append("张三" + "\t");
			//s_pdata.Append("0" + "\t");
			//s_pdata.Append("88" + "\t");
			//s_pdata.Append("长江路111111号" + "\t");
			//s_pdata.Append("疾病" + "\t");
			//s_pdata.Append("2019-12-1" + "\t");
			//s_pdata.Append("管理员" + "\t");
			//s_pdata.Append("2019-12-1" + "\t");
			//s_pdata.Append("王二");

			//n_print.of_print_hhzm(s_pdata.ToString());
			//n_print.Dispose();
		}

		private void BarButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			////////  显示登录窗口  //////////
			Login login = new Login();
			if (login.ShowDialog(this) == DialogResult.OK)
			{
				/////////////////////  成功登陆后处理   ///////////////////
				barStaticItem2.Caption = Envior.cur_userName;
			}
		}

		/// <summary>
		/// 原始登记查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03070") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			openBusinessObject("Report_regori");
		}

		/// <summary>
		/// 类别登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
		{
			openBusinessObject("Report_ClassStat");
		}

		/// <summary>
		/// 升级文件上传
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
		{
			Frm_upgrade frm_1 = new Frm_upgrade();
			frm_1.ShowDialog();
		}

		private void BarButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03060") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("Report_RegisterOut");
		}

		/// <summary>
		/// 出灵数据查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03010") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("Report_Checkout");
		}

		/// <summary>
		/// 欠费数据查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03050") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("Report_Debt");
		}

		/// <summary>
		/// 收款作废查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("FinanceRoll_Report");
		}

		//修改密码
		private void BarButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			Frm_ModifyPwd frm_modify_pwd = new Frm_ModifyPwd();
			frm_modify_pwd.ShowDialog();
			//frm_modify_pwd.Dispose();
		}

		private void BarButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Report_ItemStat");
		}

        private void BarButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
			//string s_response = HttpPost(@"https://www.baidu.com/", "");
			//MessageBox.Show(s_response);
			//MessageBox.Show(Tools.ConvertObjectToJson());

			//InvoiceRequestData idata = new InvoiceRequestData();
			//idata.region = "0001";
			//idata.deptcode = "001001";
			//idata.appid = "app1";

			//idata.data.Add("billBatchCode", "0100000100");
			//idata.data.Add("billNo", "0000000001");
			//idata.noise = new DateTime().ToString();
			//idata.version = "1.0";
			//idata.sign = "E9324CF02F95CB072B6DBCEA33E725C3";

			//string s_json = Tools.ConvertObjectToJson(idata);
			//MessageBox.Show(s_json);

			//InvoiceRequestData rdata = new InvoiceRequestData();
			//rdata = JsonConvert.DeserializeObject<InvoiceRequestData>(s_json);

			//MessageBox.Show(rdata.sign,"sign");

			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03020") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("FireHaving");
		}

        private void BarButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            //InvoiceRequestData idata = new InvoiceRequestData();
            //idata.data.Add("billBatchCode", "0100000100");
            //idata.data.Add("billNo", "0000000001");
            //idata.data.Add("random", "DFG456");
            //string s_json = Tools.ConvertObjectToJson(idata.data);

            //MessageBox.Show(Tools.EncodeBase64("utf-8", s_json));

        }

        private void BarButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
			// 打印发票 InvoiceAction.PrintInvoice("3610","00570009");
			//InvoiceAction.RemoveInvoice("");  
        }

        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
 
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
 
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frm_InvoiceItem frm_ii01 = new Frm_InvoiceItem();
            frm_ii01.ShowDialog();
            frm_ii01.Dispose();
        }

		/// <summary>
		/// 测试发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
		{
			if(InvoiceAction.GetInvoiceNextNum(Envior.invoice_pBillBatchCode) > 0)
			{
				XtraMessageBox.Show(Envior.NEXT_BILL_CODE, "发票代码");
				XtraMessageBox.Show(Envior.NEXT_BILL_NUM,"发票号");
			}
		}

		private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
		{
			Frm_TaxItem frm_1 = new Frm_TaxItem();
			frm_1.ShowDialog();
			frm_1.Dispose();
		}

		/// <summary>
		/// 进灵登记查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03001") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("Report_FireCheckin");
		}

		/// <summary>
		/// 火化安排
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03030") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("FireArrange");
		}

		/// <summary>
		/// 寄存登记统计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
		{
			//权限检查
			if (MiscAction.GetRight(Envior.cur_userId, "03080") == "0")
			{
				MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.openBusinessObject("Report_RegStat");
		}

		private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Report_CasherStat");
		}

		private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (Envior.cur_userId != AppInfo.ROOTID)
			{
				XtraMessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			openBusinessObject("RightGrant");
		}

		private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
		{
			openBusinessObject("InvoiceItemStat");
		}
	}
}