using Bin2019.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bin2019.Action
{
	class MiscAction
	{
		/// <summary>
		/// 返回服务或商品名
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemFullName(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemFullName(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}


		public static decimal GetItemFixPrice(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return decimal.Parse(SqlAssist.ExecuteScalar("select pkg_business.fun_getFixPrice(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString());
		}

		/// <summary>
		/// 保存财政发票基本信息
		/// </summary>
		/// <param name="region"></param>
		/// <param name="deptId"></param>
		/// <param name="appId"></param>
		/// <param name="ver"></param>
		/// <param name="key"></param>
		/// <param name="batchCode"></param>
		/// <returns></returns>
		public static int SaveInvoiceBaseInfo(string region,string deptId,string appId,string ver,string key,string batchCode,string kind)
		{
			OracleParameter op_region = new OracleParameter("ic_region", OracleDbType.Varchar2, 20);
			op_region.Direction = ParameterDirection.Input;
			op_region.Value = region;

			OracleParameter op_deptId = new OracleParameter("ic_deptId", OracleDbType.Varchar2, 20);
			op_deptId.Direction = ParameterDirection.Input;
			op_deptId.Value = deptId;

			OracleParameter op_appId = new OracleParameter("ic_appId", OracleDbType.Varchar2, 20);
			op_appId.Direction = ParameterDirection.Input;
			op_appId.Value = appId;

			OracleParameter op_ver = new OracleParameter("ic_ver", OracleDbType.Varchar2, 20);
			op_ver.Direction = ParameterDirection.Input;
			op_ver.Value = ver;

			OracleParameter op_key = new OracleParameter("ic_key", OracleDbType.Varchar2, 50);
			op_key.Direction = ParameterDirection.Input;
			op_key.Value = key;

			OracleParameter op_batchCode = new OracleParameter("ic_batchcode", OracleDbType.Varchar2, 20);
			op_batchCode.Direction = ParameterDirection.Input;
			op_batchCode.Value = batchCode;

			OracleParameter op_kind = new OracleParameter("ic_kind", OracleDbType.Varchar2, 20);
			op_kind.Direction = ParameterDirection.Input;
			op_kind.Value = kind;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_SaveInvoiceBaseInfo",
				new OracleParameter[] { op_region,op_deptId,op_appId,op_ver,op_key,op_batchCode, op_kind });
		}

		 
		/// <summary>
		/// 操作员姓名映射
		/// </summary>
		/// <param name="uc001"></param>
		/// <returns></returns>
		public static string Mapper_operator(string uc001)
		{
			OracleParameter op_uc001 = new OracleParameter("uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;
			Object re = SqlAssist.ExecuteScalar("select uc003 from uc01 where uc001 = :uc001", new OracleParameter[] { op_uc001 });
			return re.ToString();
		}

		/// <summary>
		/// 财务收款作废
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FinanceRemove(string fa001,string reason,string handler)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//作废原因
			OracleParameter op_fa003 = new OracleParameter("ic_fa003", OracleDbType.Varchar2, 50);
			op_fa003.Direction = ParameterDirection.Input;
			op_fa003.Value = reason;

			//作废人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinanceRemove",
				new OracleParameter[] {op_fa001, op_fa003,op_handler });
		}

		public static int Modify_Pwd(string uc001,string newpwd)
		{
			//用户编号
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;

			//新密码
			OracleParameter op_newpwd = new OracleParameter("ic_newPwd", OracleDbType.Varchar2, 50);
			op_newpwd.Direction = ParameterDirection.Input;
			op_newpwd.Value = newpwd;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_Modify_Pwd",
				new OracleParameter[] { op_uc001, op_newpwd});
		}


		/// <summary>
		/// 返回服务器 系统日期 字符串 yyyy-mm-dd
		/// </summary>
		/// <returns></returns>
		public static string GetServerDateString()
		{
			return SqlAssist.ExecuteScalar("select to_char(sysdate,'yyyy-mm-dd') from dual").ToString();
		}


		/// <summary>
		/// 返回销方银行账号
		/// </summary>
		/// <returns></returns>
		public static string GetTaxSellerBank()
		{
			return SqlAssist.ExecuteScalar("select sp005 from sp01 where sp002 = 'InfoSellerBankAccount' ").ToString();
		}

		/// <summary>
		/// 返回销方地址电话
		/// </summary>
		public static string GetTaxSellerAddress()
		{
			return SqlAssist.ExecuteScalar("select sp005 from sp01 where sp002 = 'InfoSellerAddressPhone' ").ToString();
		}

		/// <summary>
		/// 返回项目税务名称
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemTaxName(string itemId)
		{
			return SqlAssist.ExecuteScalar("select tx003 from v_allItem where item_id ='" + itemId + "'" ).ToString();
		}

		////财务类别统计
		public static int ClassStat(string dbegin, string dend, string[] class_arry)
		{
			OracleCommand cmd = new OracleCommand("pkg_report.prc_ClassStat", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;
		 
			//统计日期1
			OracleParameter op_begin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 16);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = dbegin;
			//统计日期2
			OracleParameter op_end = new OracleParameter("ic_end", OracleDbType.Varchar2, 16);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = dend;

			//销售记录编号数组
			OracleParameter op_class_arry = new OracleParameter("ic_class", OracleDbType.Varchar2);
			op_class_arry.Direction = ParameterDirection.Input;
			op_class_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_class_arry.Value = class_arry;

			//收费员
			//OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			//op_handler.Direction = ParameterDirection.Input;
			//op_handler.Value = handler;

			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] {  op_begin, op_end, op_class_arry, appcode, apperror });
				cmd.ExecuteNonQuery();

				if (int.Parse(appcode.Value.ToString()) < 0)
				{
					trans.Rollback();
					MessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}

				trans.Commit();
				return 1;
			}
			catch (InvalidOperationException e)
			{
				trans.Rollback();
				MessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			finally
			{
				cmd.Dispose();
			}

		}

		/// <summary>
		/// 返回 分类统计笔数
		/// </summary>
		/// <returns></returns>
		public static int GetClassStat_BS()
		{
			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_GetClassStat_BS from dual"));
		}

		/// <summary>
		/// 收款员收费统计
		/// </summary>
		/// <param name="dbegin"></param>
		/// <param name="dend"></param>
		/// <returns></returns>
		public static int CasherStat(string dbegin, string dend)
		{
			OracleCommand cmd = new OracleCommand("pkg_report.prc_CasherStat", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//统计日期1
			OracleParameter op_begin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 16);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = dbegin;
			//统计日期2
			OracleParameter op_end = new OracleParameter("ic_end", OracleDbType.Varchar2, 16);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = dend;
 
			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] { op_begin, op_end ,appcode, apperror });
				cmd.ExecuteNonQuery();

				if (int.Parse(appcode.Value.ToString()) < 0)
				{
					trans.Rollback();
					MessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}

				trans.Commit();
				return 1;
			}
			catch (InvalidOperationException e)
			{
				trans.Rollback();
				MessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			finally
			{
				cmd.Dispose();
			}
		}

		public static int GrantRights(string ro001, string[] ri001_arry, string[] right_arry)
		{
			OracleCommand cmd = new OracleCommand("pkg_business.prc_GrantRights", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//角色编号
			OracleParameter op_ro001 = new OracleParameter("ic_ro001", OracleDbType.Varchar2, 10);
			op_ro001.Direction = ParameterDirection.Input;
			op_ro001.Value = ro001;
			//功能编号数组
			OracleParameter op_ri001_arry = new OracleParameter("ri001_arry", OracleDbType.Varchar2);
			op_ri001_arry.Direction = ParameterDirection.Input;
			op_ri001_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_ri001_arry.Value = ri001_arry;

			//授权数组
			OracleParameter op_right_arry = new OracleParameter("right_arry", OracleDbType.Varchar2);
			op_right_arry.Direction = ParameterDirection.Input;
			op_right_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_right_arry.Value = right_arry;

			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] { op_ro001, op_ri001_arry, op_right_arry, appcode, apperror });
				cmd.ExecuteNonQuery();

				if (int.Parse(appcode.Value.ToString()) < 0)
				{
					trans.Rollback();
					MessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}

				trans.Commit();
				return 1;
			}
			catch (InvalidOperationException e)
			{
				trans.Rollback();
				MessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			finally
			{
				cmd.Dispose();
			}
		}

		/// <summary>
		/// 获取操作员权限
		/// </summary>
		/// <returns></returns>
		public static string GetRight(string uc001, string ri001)
		{
			if (uc001 == AppInfo.ROOTID) return "2";
			OracleCommand cmd = new OracleCommand("pkg_business.fun_GetRight", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			OracleParameter returnValue = new OracleParameter("result", OracleDbType.Varchar2, 3);
			returnValue.Direction = ParameterDirection.ReturnValue;

			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Size = 10;
			op_uc001.Value = uc001;

			OracleParameter op_ri001 = new OracleParameter("ic_ri001", OracleDbType.Varchar2, 10);
			op_ri001.Direction = ParameterDirection.Input;
			op_ri001.Size = 10;
			op_ri001.Value = ri001;

			try
			{
				cmd.Parameters.Add(returnValue);
				cmd.Parameters.Add(op_uc001);
				cmd.Parameters.Add(op_ri001);

				cmd.ExecuteNonQuery();
			}
			catch (InvalidOperationException e)
			{
				MessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				cmd.Dispose();
			}

			return returnValue.Value.ToString();
		}

	}
}
