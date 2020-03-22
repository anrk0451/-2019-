using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bin2019.Domain
{
    class Sa01
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string sa001 { get; set; } //销售流水号
        public string ac001 { get; set; } //逝者编号
        public string sa002 { get; set; } //服务或商品类别
        public string sa003 { get; set; } //服务或商品名称
        public string sa004 { get; set; } //服务或商品编号
        public string sa005 { get; set; } //销售类别 0-火花业务 1-临时销售 2-骨灰寄存
        public decimal? price { get; set; }//销售单价
        public decimal? nums { get; set; } //数量
        public decimal? sa007 { get; set; }//销售金额
        public decimal? sa006 { get; set; }//原始单价
        public string sa008 { get; set; }  //结算状态 0-未结算 1-已结算 2-退费
        public string sa010 { get; set; }  //结算流水号
        public string sa100 { get; set; }    //经办人
        public DateTime? sa200 { get; set; } //经办日期
        public string status { get; set; }   //状态 0-删除 1-正常

    }
}
