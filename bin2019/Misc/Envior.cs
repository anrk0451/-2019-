using DevExpress.XtraBars.Ribbon;
//using PrtServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bin2019.Misc
{
	class Envior
	{
		public static RibbonForm mform { get; set; }       //当前主窗口
        public static string cur_user { get; set; }        //当前登录用户
        public static string cur_userId { get; set; }      //当前登录用户Id
        public static string cur_userName { get; set; }       //当前登录用户名


		public static string invoice_region { get; set; }          //财政发票-地区代码
        public static string invoice_dept { get; set; }            //财政发票-单位代码
        public static string invoice_appid { get; set; }           //财政发票-应用账号
        public static string invoice_ver { get; set; }             //财政发票-版本号
        public static string invoice_key { get; set; }             //财政发票-密钥
        public static string invoice_placecode { get; set; }       //开票点编码
        public static string invoice_pBillBatchCode { get; set; }  //发票代码
		public static string invoice_kind { get; set; }			   //纸质票据种类	

        public static string NEXT_BILL_CODE { get; set; }     //下张发票代码
        public static string NEXT_BILL_NUM { get; set; }      //下张发票票号
         

        public static string[] rolearry { get; set; }      //所属角色组
        public static char loginMode { get; set; }         //登陆模式

        //public static bool printable { get; set; }       //打印进程是否启动
		public static bool canInvoice { get; set; }		   //当前的用户允许开发票
        public static IntPtr prtservHandle { get; set; }   //打印服务窗口Handle
        public static int prtConnId { get; set; }          //打印会话连接Id 

		public static bool TAX_READY { get; set; }		   // 金税卡状态

        public static int PRINT_PORT { get; set; }         //打印端口 

        public static SocketClient socket { get; set; }    //socket发送对象

		//public static n_prtserv prtserv { get; set; }    //打印服务对象
 
	}
}
