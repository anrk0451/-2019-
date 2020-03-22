using Bin2019.Misc;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System; 
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Bin2019.Action
{
	class C_detail
	{
		public string chargeCode;
		public string chargeName;
		public decimal? std;
		public decimal? number;
		public decimal? amt;
		public C_detail()
		{
			chargeCode = string.Empty;
			chargeName = string.Empty;
			std = 0;
			number = 0;
			amt = 0;
		}
	};

	class C_params
	{
		public string region { get; set; }
		public string deptcode { get; set; }
		public string appid { get; set; }
		public string data { get; set; }
		public string noise { get; set; }
		public string version { get; set; }
		public string sign { get; set; }
	}

	/// <summary>
	/// 财政发票类
	/// </summary>
	class InvoiceAction
    {
        /// <summary>
        /// 返回下一张 财政发票 号码
        /// </summary>
        /// <returns></returns>
        public static int GetInvoiceNextNum(string invoiceCode)
        {
			Envior.NEXT_BILL_CODE = "3610";
			Envior.NEXT_BILL_NUM = "00123456";
 
			Dictionary<string, string> bdata = new Dictionary<string, string>();

            //开票点编码
            bdata.Add("placeCode", Envior.invoice_placecode);
            bdata.Add("pBillBatchCode", invoiceCode);
 
            string s_json = Tools.ConvertObjectToJson(bdata);
            string s_business_base64 = Tools.EncodeBase64("utf-8", s_json);

            Dictionary<string,object> retdata = JsonConvert.DeserializeObject < Dictionary<string, object>>( SendInvoiceRequest("getPaperBillNo", s_business_base64));

            if(retdata != null)
            {
                if(retdata["result"].ToString() == "S0000")
                {
					s_business_base64 = retdata["message"].ToString();
					Dictionary<string, string> d_result = null;
					s_json = Tools.DecodeBase64("utf-8", s_business_base64);                        //base64解码为json 
					d_result = JsonConvert.DeserializeObject<Dictionary<string, string>>(s_json);   //json ==》 对象

					if (d_result.ContainsKey("pBillBatchCode") && d_result.ContainsKey("pBillNo"))
					{
						Envior.NEXT_BILL_CODE = d_result["pBillBatchCode"];
						Envior.NEXT_BILL_NUM = d_result["pBillNo"];
						return 1;
					}
					else
					{
						MessageBox.Show("接收的数据结构错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return -1;
					}
                }
                else
                {
                    s_business_base64 = retdata["message"].ToString();
                    MessageBox.Show(Tools.DecodeBase64("utf-8", s_business_base64), "错误" + retdata["result"].ToString(), MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("接收数据失败!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return -1;
            }
            
        }

        /// <summary>
        /// 向博思服务端 发送请求
        /// </summary>
        /// <param name="command"></param>
        /// <param name="businessData"></param>
        /// <returns></returns>        
        public static string SendInvoiceRequest(string command,string businessData)
        {
            StringBuilder sb_link = new StringBuilder(100);
            string s_sign = string.Empty;
            string s_json = string.Empty;
			string s_noise = string.Empty;

            sb_link.Append("region=" + Envior.invoice_region + "&");
            sb_link.Append("deptcode=" + Envior.invoice_dept + "&");
            sb_link.Append("appid=" + Envior.invoice_appid + "&");
            sb_link.Append("data=" + businessData + "&");                         //业务数据 做 Base64编码

			s_noise = Guid.NewGuid().ToString("N");

			sb_link.Append("noise=" + s_noise + "&");
            sb_link.Append("key=" + Envior.invoice_key + "&");
            sb_link.Append("version=" + Envior.invoice_ver);

            //////// 计算签名 //////////
            s_sign = Tools.EncryptWithMD5(sb_link.ToString()).ToUpper();

            ////构建发送博思服务端数据
            InvoiceRequestData reqdata = new InvoiceRequestData();
            reqdata.region = Envior.invoice_region;
            reqdata.deptcode = Envior.invoice_dept;
            reqdata.appid = Envior.invoice_appid;
            reqdata.data = businessData;                   //业务数据,再经过base64编码
			reqdata.noise = s_noise;
            reqdata.version = Envior.invoice_ver;
            reqdata.sign = s_sign;

            s_json = Tools.ConvertObjectToJson(reqdata);

            string s_url = AppInfo.BOSI_API_ADDR + command; 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(s_url);
            request.Method = "POST";
            request.ContentType = "application/json";

			byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(s_json);
			request.ContentLength = buf.Length;

			//request.ContentLength = Encoding.UTF8.GetByteCount(s_json);
			//request.ContentLength = Encoding.UTF8.GetBytes(s_json).Length;

			Stream myRequestStream = request.GetRequestStream();
			myRequestStream.Write(buf, 0, buf.Length);


			//StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));

			//s_json = @"{"region":"230125","deptcode":"610001","appid":"BXBYG3477584","data":"eyJwbGFjZUNvZGUiOiIwMDEiLCJwQmlsbEJhdGNoQ29kZSI6IjM2MTAifQ == ","noise":"ab8fb017177844d9aeb6a1284cfc07b5","version":"3.1.2.0","sign":"63BFAE4B13DBF7522DF207E9DE35D034"}";
			//string ss = Tools.EncodeBase64("utf-8", s_json);

			//myStreamWriter.Write(buf,0,buf.Length);
			myRequestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            InvoiceResponseData repdata = JsonConvert.DeserializeObject<InvoiceResponseData>(retString);

            return Tools.DecodeBase64("utf-8", repdata.data); 
        }


        /// <summary>
        /// 纸质发票开票!
        /// </summary>
        /// <returns></returns>
        public static int Invoice(string fa001)
        {
			try
			{
				DataTable dt_sa01 = new DataTable();
				OracleDataAdapter sa01Adapter = new OracleDataAdapter("select * from v_sa01 where sa010 = :sa010", SqlAssist.conn);
				OracleParameter op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
				op_sa010.Direction = ParameterDirection.Input;

				sa01Adapter.SelectCommand.Parameters.Add(op_sa010);
				op_sa010.Value = fa001;

				sa01Adapter.Fill(dt_sa01);

				OracleDataReader fa01 = SqlAssist.ExecuteReader("select * from fa01 where fa001='" + fa001 + "'");
				if (!fa01.HasRows)
				{
					MessageBox.Show("未找到结算记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return -1;
				}

				Dictionary<string, object> bdata = new Dictionary<string, object>();
				List<C_detail> detaildata = new List<C_detail>();

				while (fa01.Read())
				{
					bdata.Add("busNo", fa001);                                               //业务流水号
					bdata.Add("busDateTime", Convert.ToDateTime(fa01["FA200"]).ToString("yyyyMMddHHmmssfff"));  //业务发生时间
					bdata.Add("billDate", System.DateTime.Now.ToString("yyyyMMddHHmmssfff"));//开票时间
					bdata.Add("placeCode", Envior.invoice_placecode);                        //开票点编码
					bdata.Add("billCode", Envior.invoice_kind);                              //票据种类编码(  填写系统内部编码值)
					bdata.Add("billBatchCode", Envior.NEXT_BILL_CODE);                        //票据代码
					bdata.Add("billNo", Envior.NEXT_BILL_NUM);                               //票据号
					bdata.Add("payer", fa01["FA003"].ToString());                             //交款人
					bdata.Add("payerType", "1");                                              //交款人类型 1-个人 2-单位
					bdata.Add("idCardNo", fa01["FA013"].ToString());                          //身份证号码
					bdata.Add("payChannel", "15");                                            //交费渠道 15-现金
					bdata.Add("payee", MiscAction.Mapper_operator(fa01["FA100"].ToString()));//收费员
					bdata.Add("author", Envior.cur_userName);                                 //开票人
					bdata.Add("totalAmt", fa01["FA004"]);                                     //开票总金额
					bdata.Add("remark", fa01["FA180"]);                                       //备注
				}
				fa01.Dispose();

				string s_invcode = string.Empty;
				int index;


				foreach (DataRow dr in dt_sa01.Rows)
				{
					s_invcode = GetItemInvoiceCode(dr["SA002"].ToString(), dr["SA004"].ToString());
					if (string.IsNullOrEmpty(s_invcode))
					{
						MessageBox.Show(dr["SA003"] + "没有设置财政发票编码!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return -1;
					}

					//如果包含指定 发票编码值
					index = detaildata.FindIndex(item => item.chargeCode == s_invcode);
					if (index >= 0)
					{
						detaildata[index].number = detaildata[index].number + Convert.ToDecimal(dr["NUMS"]);
						detaildata[index].amt = detaildata[index].amt + Convert.ToDecimal(dr["SA007"]);
						detaildata[index].std = Decimal.Round((detaildata[index].amt / detaildata[index].number).Value, 6);
					}
					else
					{
						C_detail str_detail = new C_detail();
						str_detail.chargeCode = s_invcode;
						str_detail.chargeName = GetItemInvoiceName(s_invcode);
						str_detail.std = Convert.ToDecimal(dr["PRICE"]);
						str_detail.number = Convert.ToDecimal(dr["NUMS"]);
						str_detail.amt = Convert.ToDecimal(dr["SA007"]);
						detaildata.Add(str_detail);
					}
				}
				bdata.Add("chargeDetail", detaildata);

				string s_json = Tools.ConvertObjectToJson(bdata);

				//XtraMessageBox.Show(s_json);


				string s_business_base64 = Tools.EncodeBase64("utf-8", s_json);

				Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(SendInvoiceRequest("invoicePBill", s_business_base64));
				if (retdata != null)
				{
					if (retdata["result"].ToString() == "S0000")
					{
						s_business_base64 = retdata["message"].ToString();
						Dictionary<string, string> d_result = null;
						s_json = Tools.DecodeBase64("utf-8", s_business_base64);                        //base64解码为json 
						d_result = JsonConvert.DeserializeObject<Dictionary<string, string>>(s_json);   //json ==》 对象

						/////更新财务发票日志
						string s_billCode = d_result["billCode"].ToString();
						string s_billBatchCode = d_result["billBatchCode"].ToString();
						string s_billNo = d_result["billNo"].ToString();
						string s_random = d_result["random"].ToString();
						string s_kpsj = d_result["billDate"].ToString();

						InvoiceLog(fa001, s_billCode, s_billBatchCode, s_billNo, s_random, s_kpsj, Envior.invoice_placecode);

						MessageBox.Show("发票开具成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
						if (PrintInvoice(s_billBatchCode, s_billNo) > 0)
						{
							XtraMessageBox.Show("发票打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						return 1;
					}
					else
					{
						s_business_base64 = retdata["message"].ToString();
						MessageBox.Show(Tools.DecodeBase64("utf-8", s_business_base64), "错误" + retdata["result"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return -1;
					}
				}
				else
				{
					MessageBox.Show("接收数据失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return -1;
			}
			
        }

        
        /// <summary>
        /// 获取销售项目 财政发票编码
        /// </summary>
        /// <param name="serviceSalesType"></param>
        /// <param name="salesItemId"></param>
        /// <returns></returns>
        public static string GetItemInvoiceCode(string serviceSalesType,string salesItemId)
        {
            OracleParameter op_type = new OracleParameter("ic_serviceSalesType", OracleDbType.Varchar2, 3);
            op_type.Direction = ParameterDirection.Input;
            op_type.Value = serviceSalesType;

            OracleParameter op_id = new OracleParameter("ic_salesItemId", OracleDbType.Varchar2, 10);
            op_id.Direction = ParameterDirection.Input;
            op_id.Value = salesItemId;

            Object re = 
                SqlAssist.ExecuteScalar("select pkg_business.fun_GetInvoiceCode(:ic_serviceSalesType,:ic_salesItemId) from dual", new OracleParameter[] { op_type,op_id });
            return re.ToString();
        }

		/// <summary>
		/// 获取销售项目 财政发票 项目名称
		/// </summary>
		/// <param name="invcode"></param>
		/// <returns></returns>
		public static string GetItemInvoiceName(string invcode)
		{
			OracleParameter op_invcode = new OracleParameter("ic_invcode", OracleDbType.Varchar2, 20);
			op_invcode.Direction = ParameterDirection.Input;
			op_invcode.Value = invcode;
			 
			Object re =
				SqlAssist.ExecuteScalar("select pkg_business.fun_GetInvoiceName(:ic_invcode) from dual", new OracleParameter[] {op_invcode});
			return re.ToString();
		}


        /// <summary>
		/// 财务发票记录日志
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="billCode"></param>
		/// <param name="billBatchCode"></param>
		/// <param name="billNo"></param>
		/// <param name="checksum"></param>
		/// <param name="kpsj"></param>
		/// <returns></returns>
        public static int InvoiceLog(string fa001,string billCode,string billBatchCode,string billNo,string checksum,string kpsj,string placeCode)
        {
            //结算流水号
            OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
            op_fa001.Direction = ParameterDirection.Input;
            op_fa001.Value = fa001;

            //票据种类
            OracleParameter op_billCode = new OracleParameter("ic_billCode", OracleDbType.Varchar2, 50);
            op_billCode.Direction = ParameterDirection.Input;
            op_billCode.Value = billCode;

            //票据代码
            OracleParameter op_billBatchCode = new OracleParameter("ic_billBatchCode", OracleDbType.Varchar2, 50);
            op_billBatchCode.Direction = ParameterDirection.Input;
            op_billBatchCode.Value = billBatchCode;

            //票据号
            OracleParameter op_billNo = new OracleParameter("ic_billNo", OracleDbType.Varchar2, 50);
            op_billNo.Direction = ParameterDirection.Input;
            op_billNo.Value = billNo;

			//校验码
			OracleParameter op_check = new OracleParameter("ic_checksum", OracleDbType.Varchar2, 50);
			op_check.Direction = ParameterDirection.Input;
			op_check.Value = checksum;

			//开票时间
			OracleParameter op_kpsj = new OracleParameter("id_kpsj", OracleDbType.Varchar2,50);
			op_kpsj.Direction = ParameterDirection.Input;
			op_kpsj.Value = kpsj;

			//开票点编码
			OracleParameter op_pc = new OracleParameter("ic_placecode", OracleDbType.Varchar2, 10);
			op_pc.Direction = ParameterDirection.Input;
			op_pc.Value = placeCode;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_InvoiceLog", new OracleParameter[] {op_fa001,op_billCode,op_billBatchCode,op_billNo,op_check,op_kpsj,op_pc});
        }

		/// <summary>
		/// 打印财政发票
		/// </summary>
		/// <param name="pBillBatchCode"></param>
		/// <param name="pBillNo"></param>
		/// <returns></returns>
		public static int PrintInvoice(string pBillBatchCode,string pBillNo)
		{
			Dictionary<string, string> bdata = new Dictionary<string, string>();

			//业务数据
			bdata.Add("pBillBatchCode", pBillBatchCode);
			bdata.Add("pBillNo", pBillNo);
			bdata.Add("randomNumber", "0dbc17");

			string s_json = Tools.ConvertObjectToJson(bdata);
			string s_business_base64 = Tools.EncodeBase64("utf-8", s_json);

			StringBuilder sb_link = new StringBuilder(100);

			//sb_link.Append("region=" + Envior.invoice_region + "&");
			//sb_link.Append("deptcode=" + Envior.invoice_dept + "&");
			sb_link.Append("appid=" + Envior.invoice_appid + "&");
			sb_link.Append("data=" + s_business_base64 + "&");                         //业务数据 做 Base64编码

			string s_noise = Guid.NewGuid().ToString("N");

			sb_link.Append("noise=" + s_noise + "&");
			sb_link.Append("key=" + Envior.invoice_key + "&");
			sb_link.Append("version=" + Envior.invoice_ver);

			//////// 计算签名 //////////
			string s_sign = Tools.EncryptWithMD5(sb_link.ToString()).ToUpper();

			StringBuilder sb_send = new StringBuilder(100);
			sb_send.Append("{\"method\":\"printPaperBill\",\"params\":");

			Dictionary<string, string> s_data = new Dictionary<string, string>();
			s_data.Add("appid",Envior.invoice_appid);
			s_data.Add("data", s_business_base64);
			s_data.Add("noise", s_noise);
			s_data.Add("version",Envior.invoice_ver);
			s_data.Add("sign",s_sign );

			sb_send.Append( Tools.ConvertObjectToJson(s_data) + "}" );

			string s_finish = Tools.EncodeBase64("utf-8", sb_send.ToString());


			string s_url = @"http://127.0.0.1:13526/extend?dllName=NontaxIndustry&func=CallNontaxIndustry&payload=";
			string s_url2 = s_url + s_finish;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(s_url2);
			request.Method = "GET";
			request.ContentType = "application/json";

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream myResponseStream = response.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
			string retString = myStreamReader.ReadToEnd();

			myStreamReader.Close();
			myResponseStream.Close();

			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(retString);

			if (retdata != null)
			{
				Dictionary<string, object> result = JsonConvert.DeserializeObject < Dictionary<string, object> > (Tools.DecodeBase64("utf-8", retdata["data"].ToString()));
				if (result["result"].ToString() == "S0000")
				{
					return 1;
				}
				else
				{
					s_business_base64 = result["message"].ToString();
					MessageBox.Show(Tools.DecodeBase64("utf-8", s_business_base64), "错误" + result["result"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return -1;
				}
			}
			else
			{
				MessageBox.Show("打印进程通信错误,打印失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
		}


		/// <summary>
		/// 打印财政发票(根据结算流水号)
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int PrintInvoice(string fa001)
		{
			try
			{
				string s_pBillBatchCode = string.Empty;
				string s_pBillNo = string.Empty;
				OracleDataReader reader = SqlAssist.ExecuteReader("select * from fi01 where fi001='" + fa001 + "'");
				while (reader.Read())
				{
					s_pBillBatchCode = reader["FI003"].ToString();   //票据代码
					s_pBillNo = reader["FI004"].ToString();          //票据号 
				}
				reader.Dispose();

				if (String.IsNullOrEmpty(s_pBillBatchCode) || String.IsNullOrEmpty(s_pBillNo))
				{
					XtraMessageBox.Show("未找到发票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}

				if(PrintInvoice(s_pBillBatchCode, s_pBillNo) > 0)
				{
					XtraMessageBox.Show("打印发票成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return 1;
				}
				else
				{
					return -1;
				}
				
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return -1;
			}
			
		}

		 
		/// <summary>
		/// 发票作废
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
		public static int RemoveInvoice(string fa001,string reason)
		{
			try
			{
				Dictionary<string, string> bdata = new Dictionary<string, string>();
				OracleDataReader fi01 = SqlAssist.ExecuteReader("select * from fi01 where fi001='" + fa001 + "'");
				if (!fi01.HasRows)
				{
					MessageBox.Show("未找到发票开具记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return -1;
				}

				while (fi01.Read())
				{
					bdata.Add("placeCode", fi01["FI007"].ToString());        //开票点编码
					bdata.Add("billCode", fi01["FI002"].ToString());         //票据种类
					bdata.Add("billBatchCode", fi01["FI003"].ToString());    //票据代码
					bdata.Add("billNo", fi01["FI004"].ToString());           //票号
					bdata.Add("author", Envior.cur_userName);                //作废人
					bdata.Add("reason", reason);                             //作废原因
					bdata.Add("busDateTime", System.DateTime.Now.ToString("yyyyMMddHHmmssfff"));
				}
				fi01.Dispose();

				MessageBox.Show(bdata.ToString());

				string s_json = Tools.ConvertObjectToJson(bdata);
				string s_business_base64 = Tools.EncodeBase64("utf-8", s_json);

				Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(SendInvoiceRequest("invalidPBill", s_business_base64));

				if (retdata != null)
				{
					if (retdata["result"].ToString() == "S0000")
					{
						MessageBox.Show("debug6");
						XtraMessageBox.Show("作废成功!", "提示");
						return 1;
					}
					else
					{
						MessageBox.Show("debug7");
						s_business_base64 = retdata["message"].ToString();
						MessageBox.Show(Tools.DecodeBase64("utf-8", s_business_base64), "错误" + retdata["result"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return -1;
					}
				}
				else
				{
					MessageBox.Show("接收数据失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}catch(Exception ee)
			{
				MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			
		}


		/// <summary>
		/// 更新交款人信息
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="cuname"></param>
		/// <param name="cuid"></param>
		/// <returns></returns>
		public static int UpdateCustomerInfo(string fa001,string cuname, string cuid)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//交款人姓名
			OracleParameter op_cuname = new OracleParameter("ic_cuname", OracleDbType.Varchar2, 50);
			op_cuname.Direction = ParameterDirection.Input;
			op_cuname.Value = cuname;

			//交款人身份证号
			OracleParameter op_cuid = new OracleParameter("ic_cuid", OracleDbType.Varchar2, 50);
			op_cuid.Direction = ParameterDirection.Input;
			op_cuid.Value = cuid;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_UpdateCustomerInfo",
				new OracleParameter[] { op_fa001, op_cuname,op_cuid });
		}


	}
}
