using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bin2019.Misc
{
    /// <summary>
    /// 应用信息
    /// </summary>
    class AppInfo
    {
        private static string _AppTitle = "殡仪馆管理信息系统";     //应用标题
        private static string _AppVersion = "19.1205001";           //应用版本号
        private static string _UnitName = "宾县殡仪馆";             //使用单位    
        private static string _ROOTID = "0000000000";               //root用户Id

        private static string _BOSI_API_ADDR = @"http://192.168.101.37:18026/standard-web/api/standard/";   //博思API服务地址
  
        private static int _TAXITEMCOUNT = 7;                       //打印发票清单阈值
		private static string _REG_TAX_NAME = "寄存费";             //寄存费税务名称
		private static string online_date = "2019-12-17";           //系统上线日期     


        public static string UnitName
        {
            get { return AppInfo._UnitName; }
        }

        public static string AppTitle
        {
            get { return _AppTitle; }
        }

        public static string AppVersion
        {
            get { return _AppVersion; }
        }

        public static string ROOTID
        {
            get { return _ROOTID; }
        }
 
		public static int TAXITEMCOUNT
		{
			get { return _TAXITEMCOUNT; }
		}

		public static string REG_TAX_NAME
		{
			get { return _REG_TAX_NAME; }
		}

        public static string BOSI_API_ADDR
        {
            get { return _BOSI_API_ADDR; }
        }

		public static string ONLINE_DATE
		{
			get { return online_date; }
		}
         
    }
}
