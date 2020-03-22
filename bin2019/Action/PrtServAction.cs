using Bin2019.Domain;
using Bin2019.Misc;
using Oracle.ManagedDataAccess.Client;
//using PrtServ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bin2019.Action
{
	class PrtServAction
	{
		[DllImport("user32.dll", EntryPoint = "SendMessageA", SetLastError = true/*, CharSet = CharSet.Auto*/)]
		private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, ref string lParam);
		private static string PADSTR = "!@#";
		private static string PADSTR2 = "$$$";

		class Send_PrintData
		{
			public string command { get; set; }
			public string data { get; set; }
			public string extra1 { get; set; }   //附加信息1
			public string extra2 { get; set; }   //附加信息2
			public string extra3 { get; set; }   //附加信息3
			public string extra4 { get; set; }   //附加信息4
			public string extra5 { get; set; }   //附加信息5

		}

		/// <summary>
		/// 打印火化证明
		/// </summary>
		/// <param name="ac001"></param>
		public static void Print_HHZM(string ac001 )
		{
			OracleCommand oc_hhzm = new OracleCommand("select * from v_print_hhzm where ac001= :ac001", SqlAssist.conn);
			OracleParameter op_ac001 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;
			oc_hhzm.Parameters.Add(op_ac001);

			OracleDataReader reader = null;
			try
			{
				reader = oc_hhzm.ExecuteReader();
				if (reader.HasRows && reader.Read())
				{
					StringBuilder sb_1 = new StringBuilder(100);
					sb_1.Append(Convert.ToInt64(reader["AC001"].ToString()).ToString() + PADSTR);  // id 编号
					sb_1.Append(reader["AC003"].ToString() + PADSTR);                              //逝者姓名
					sb_1.Append(reader["AC002"].ToString() + PADSTR);                              //性别
					sb_1.Append(reader["AC004"].ToString() + PADSTR);                              //年龄
					sb_1.Append(reader["AC008"].ToString() + PADSTR);                              //详细住址
					sb_1.Append(reader["AC005"].ToString() + PADSTR);                              //死亡原因
					sb_1.Append(reader["FIRETIME"].ToString() + PADSTR);                           //火化时间
					sb_1.Append(Envior.cur_userName + PADSTR);                                     //经办人
					sb_1.Append(reader["FIRETIME"].ToString() + PADSTR);                           //经办时间(为火化日期)
					sb_1.Append(reader["UNITNAME"].ToString());                                    //单位名称

					Send_PrintData printData = new Send_PrintData();
					printData.command = "HHZM";
					printData.data = sb_1.ToString();

					RibbonForm.socket.sendMsg(Tools.ConvertObjectToJson(printData));
 
				}
				else
				{
					MessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}catch(Exception ee)
			{
				MessageBox.Show(ee.ToString(),"错误");
			}
			finally
			{
				reader.Dispose();
				oc_hhzm.Dispose();
			}
 
		}

		 

		/// <summary>
		/// 打印骨灰寄存证 (初次) 包括原始登记
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="settleId"></param>
		public static void PrtRegisterCert(string rc001,string settleId)
		{
			
			OracleCommand oc_base = new OracleCommand("select * from v_print_regcert where rc001= :rc001", SqlAssist.conn);
			OracleParameter op_rc001 = new OracleParameter("rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			oc_base.Parameters.Add(op_rc001);

			OracleDataReader reader = oc_base.ExecuteReader();

			OracleCommand oc_fin = new OracleCommand("select * from rc04 where rc010 = :rc010", SqlAssist.conn);
			OracleParameter op_rc010 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
			op_rc010.Direction = ParameterDirection.Input;
			op_rc010.Value = settleId;
			oc_fin.Parameters.Add(op_rc010);

			OracleDataReader reader2 = oc_fin.ExecuteReader();

			try
			{
				if (reader.HasRows && reader.Read() )
				{
					StringBuilder sb_1 = new StringBuilder(100);
					sb_1.Append(reader["RC003"].ToString()+ PADSTR);                   // 逝者姓名
					sb_1.Append(reader["RC109"].ToString()+ PADSTR);                   // 寄存证号
					sb_1.Append(reader["POSITION"].ToString() + PADSTR);               // 寄存位置
					sb_1.Append(reader["RC050"].ToString() + PADSTR);                  // 联系人
					sb_1.Append(reader["RC052"].ToString() + PADSTR);                  // 与逝者关系
					sb_1.Append(reader["LXFS"].ToString()+ PADSTR);                    // 电话、地址
					sb_1.Append(reader["RC200"].ToString()+ PADSTR);                   // 经办日期
					sb_1.Append(reader["UNITNAME"].ToString() + PADSTR);               // 单位名称

					reader2.Read();
					if (reader2.HasRows)
					{
						sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader2["RC020"]) + PADSTR);     // 开始日期
						sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader2["RC022"]) + PADSTR);     // 终止日期
						sb_1.Append(reader2["NUMS"].ToString() + PADSTR);                            // 年限
						sb_1.Append(reader2["RC030"].ToString() + PADSTR);                           // 缴费金额
					}
					else
					{
						sb_1.Append("" + PADSTR);													   // 开始日期
						sb_1.Append("" + PADSTR);													   // 终止日期
						sb_1.Append("" + PADSTR);													   // 年限
						sb_1.Append("" + PADSTR);													   // 缴费金额
					}
					sb_1.Append(reader["RC100"].ToString() + PADSTR);                                  // 经办人
					sb_1.Append(reader["UNITTELE"].ToString() + PADSTR);                               // 业务电话

					Send_PrintData printData = new Send_PrintData();
					printData.command = "REGISTER_FIRST";
					printData.data = sb_1.ToString();

					RibbonForm.socket.sendMsg(Tools.ConvertObjectToJson(printData));
				}
				else
				{
					MessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch(Exception ee)
			{
				MessageBox.Show("打印错误!\r\n" + ee.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			finally
			{
				reader.Dispose();
				reader2.Dispose();
				oc_base.Dispose();
				oc_fin.Dispose();
			}
		}

		/// <summary>
		/// 打印寄存标签
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterLabel(string rc001)
		{
			OracleCommand oc_command = new OracleCommand("select * from v_print_reglabel where rc001= :rc001", SqlAssist.conn);
			OracleParameter op_rc001 = new OracleParameter("rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			oc_command.Parameters.Add(op_rc001);

			OracleDataReader reader = oc_command.ExecuteReader();
			if (reader.HasRows && reader.Read())
			{
				string s_szxm = string.Empty;
				StringBuilder sb_1 = new StringBuilder(100);
				if(reader["RC303"] == null || reader["RC303"] is System.DBNull)
				{
					s_szxm = reader["RC003"].ToString();
				}
				else
				{
					s_szxm = reader["RC003"].ToString() + "/" + reader["RC303"];
				}

				sb_1.Append(s_szxm + PADSTR);							 // 逝者姓名
				sb_1.Append(reader["POSITION"].ToString() + PADSTR);     // 寄存位置
				sb_1.Append(reader["RC050"].ToString() + PADSTR);		 // 联系人
				sb_1.Append(reader["RC051"].ToString() + PADSTR);        // 联系电话

				Send_PrintData printData = new Send_PrintData();
				printData.command = "REGISTER_LABEL";
				printData.data = sb_1.ToString();

				RibbonForm.socket.sendMsg(Tools.ConvertObjectToJson(printData));
			}
			else
			{
				MessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();
		}

		/// <summary>
		/// 打印寄存 续存记录
		/// </summary>
		/// <param name="settleId"></param>
		public static void PrtRegisterPayRecord(string settleId)
		{
			OracleCommand oc_command = new OracleCommand("select * from rc04 where rc010 = :rc010", SqlAssist.conn);
			OracleParameter op_rc010 = new OracleParameter("rc010", OracleDbType.Varchar2, 10);
			op_rc010.Direction = ParameterDirection.Input;
			op_rc010.Value = settleId;
			oc_command.Parameters.Add(op_rc010);

			OracleDataReader reader = oc_command.ExecuteReader();
			StringBuilder sb_1 = new StringBuilder(100);

			if (reader.HasRows && reader.Read())
			{
				string s_jbrq = string.Format("{0:yyyy-MM-dd}", reader["RC200"]);
				string s_begin = string.Format("{0:yyyy-MM-dd}", reader["RC020"]);
				string s_end = string.Format("{0:yyyy-MM-dd}", reader["RC022"]);
				string s_rc001 = reader["RC001"].ToString();
 
				sb_1.Append(s_jbrq + PADSTR);                          // 经办日期
				sb_1.Append(s_begin + PADSTR);						   // 寄存开始日期
				sb_1.Append(s_end + PADSTR);						   // 寄存终止日期
				sb_1.Append(reader["NUMS"].ToString() + PADSTR);       // 缴费年限
				sb_1.Append(reader["RC030"].ToString() + PADSTR);      // 缴费金额
				sb_1.Append(MiscAction.Mapper_operator(reader["RC100"].ToString()) + PADSTR);   //经办人


				short i_order = Convert.ToSByte(SqlAssist.ExecuteScalar("select count(*) from v_rc04 where rc001 ='" + s_rc001 + "' and rc010 <= '" + settleId + "'"));

				try
				{
					Send_PrintData printData = new Send_PrintData();
					printData.command = "PAYRECORD";
					printData.data = sb_1.ToString();
					printData.extra1 = i_order.ToString();
					RibbonForm.socket.sendMsg(Tools.ConvertObjectToJson(printData));
				}
				catch (Exception ee)
				{
					MessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();

		}

		/// <summary>
		/// 补打寄存证
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterCertBD(string rc001)
		{
			OracleCommand oc_base = new OracleCommand("select * from v_print_regcert where rc001= :rc001", SqlAssist.conn);
			OracleParameter op_rc001 = new OracleParameter("rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			oc_base.Parameters.Add(op_rc001);

			OracleDataReader reader = oc_base.ExecuteReader();
			try
			{
				if (reader.HasRows && reader.Read())
				{
					StringBuilder sb_1 = new StringBuilder(200);
					sb_1.Append(reader["RC003"].ToString() + PADSTR);                   // 逝者姓名
					sb_1.Append(reader["RC109"].ToString() + PADSTR);                   // 寄存证号
					sb_1.Append(reader["POSITION"].ToString() + PADSTR);                // 寄存位置
					sb_1.Append(reader["RC050"].ToString() + PADSTR);                   // 联系人
					sb_1.Append(reader["RC052"].ToString() + PADSTR);                   // 与逝者关系
					sb_1.Append(reader["LXFS"].ToString() + PADSTR);                    // 电话、地址
					sb_1.Append(reader["RC200"].ToString() + PADSTR);                   // 经办日期
					sb_1.Append(reader["UNITNAME"].ToString() + PADSTR);                // 单位名称
					sb_1.Append(reader["RC100"].ToString() + PADSTR);                   // 经办人
					sb_1.Append(reader["UNITTELE"].ToString() + PADSTR);                // 业务电话
					
					Send_PrintData printData = new Send_PrintData();
					printData.command = "REGISTER_CARD_BD";
					printData.data = sb_1.ToString();
					RibbonForm.socket.sendMsg(Tools.ConvertObjectToJson(printData));
				}
				else
				{
					MessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ee)
			{
				MessageBox.Show("打印错误!\r\n" + ee.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				reader.Dispose();
				oc_base.Dispose();
			}
		}

 

		/// <summary>
		/// 打印 迁出通知单
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterOutNotice(string rc001)
		{
			OracleCommand oc_command = new OracleCommand("select * from v_print_outcard where rc001= :rc001", SqlAssist.conn);
			OracleParameter op_rc001 = new OracleParameter("rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			oc_command.Parameters.Add(op_rc001);

			OracleDataReader reader = oc_command.ExecuteReader();
			if (reader.HasRows && reader.Read())
			{
				StringBuilder sb_1 = new StringBuilder(100);
				 
				sb_1.Append(reader["RC003"].ToString() + PADSTR);       // 逝者姓名
				sb_1.Append(reader["POSITION"].ToString() + PADSTR);    // 寄存位置
				sb_1.Append(reader["RC050"].ToString() + PADSTR);       // 联系人
				sb_1.Append(reader["RC051"].ToString() + PADSTR);       // 联系电话
				sb_1.Append(reader["OC005"].ToString() + PADSTR);       // 迁出原因
				sb_1.Append(reader["OC003"].ToString() + PADSTR);       // 迁出人
				sb_1.Append(reader["OC002"].ToString() + PADSTR);       // 迁出日期

				Send_PrintData printData = new Send_PrintData();
				printData.command = "OUTCARD";
				printData.data = sb_1.ToString();

				RibbonForm.socket.sendMsg(Tools.ConvertObjectToJson(printData));
			}
			else
			{
				MessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();
		}	


        /// <summary>
        /// 打印 火化业务 结算单
        /// </summary>
        /// <param name="fa001"></param>
        /// <param name="whandle"></param>
        public static void Print_JSD(string fa001)
        {
			//结算数据
			OracleCommand oc_jsd = new OracleCommand("select * from v_fa01 where fa001= :fa001", SqlAssist.conn);
			OracleParameter op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			OracleParameter op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
			op_sa010.Direction = ParameterDirection.Input;
			op_sa010.Value = fa001;

			oc_jsd.Parameters.Add(op_fa001);


			//结算明细数据
			OracleCommand oc_detail = new OracleCommand("select * from v_sa01 where sa010= :sa010",SqlAssist.conn);
			oc_detail.Parameters.Add(op_sa010);
 
			OracleDataReader reader = oc_jsd.ExecuteReader();
			OracleDataReader reader2 = oc_detail.ExecuteReader();

			string s_skr = string.Empty;
			string s_skrq = string.Empty;
			string s_cuname = string.Empty;
			int i_count = 0;
			int i_pages = 0;

			if (reader.HasRows && reader.Read())
			{
				StringBuilder sb_1 = new StringBuilder(100);
				while (reader2.Read())
				{
					sb_1.Append(reader2["SA002"].ToString() + PADSTR);                             // 服务商品类别
					sb_1.Append(reader2["SA003"].ToString() + PADSTR);                             // 服务或商品名
					sb_1.Append(reader2["PRICE"].ToString() + PADSTR);                             // 单价
					sb_1.Append(reader2["NUMS"].ToString() + PADSTR);                              // 数量
					sb_1.Append(reader2["SA007"].ToString() + PADSTR);                             // 销售金额
					sb_1.Append(PADSTR2);
					i_count++;
				}

				s_skr = MiscAction.Mapper_operator(reader["FA100"].ToString());
				s_skrq = string.Format("{0:yyyyMMdd}", reader["FA200"]);
				s_cuname = reader["FA003"].ToString();

				Send_PrintData printData = new Send_PrintData();
				printData.command = "FIRE_JSD";
				printData.data = sb_1.ToString();
				printData.extra1 = s_skr;
				printData.extra2 = s_skrq;
				printData.extra3 = s_cuname;
				

				i_pages = (int)Math.Ceiling(i_count/16.0);
				MessageBox.Show("现在开始打印【结算单】，共需要" + i_pages.ToString() + "张!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				RibbonForm.socket.sendMsg(Tools.ConvertObjectToJson(printData));				 
			}
			else
			{
				MessageBox.Show("未找到结算数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			reader2.Dispose();
			oc_jsd.Dispose();
		}



		/// <summary>
		/// 打印财务类别统计
		/// </summary>
		/// <param name="s_begin"></param>
		/// <param name="s_end"></param>
		/// <param name="whandle"></param>
		public static void Print_Report_ClassStat(string s_begin,string s_end,int whandle)
		{
			//int commandNum = PrtServAction.GenNewCommandNum();
			//SendPrtCommand(Envior.prtConnId,
			//			   whandle,
			//			   commandNum,
			//			   "Report_ClassStat",
			//			   s_begin,
			//			   s_end
			// );
		}


		/// <summary>
		/// 打印财务单项统计
		/// </summary>
		/// <param name="s_begin"></param>
		/// <param name="s_end"></param>
		/// <param name="whandle"></param>
		public static void Print_Report_ItemStat(string s_begin, string s_end, int whandle)
		{
			//int commandNum = PrtServAction.GenNewCommandNum();
			//SendPrtCommand(Envior.prtConnId,
			//			   whandle,
			//			   commandNum,
			//			   "Report_ItemStat",
			//			   s_begin,
			//			   s_end
			// );
		}
	}
}
