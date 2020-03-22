--清理数据
truncate table ac01;  --火化数据
truncate table rc01;  --寄存数据
update bi01 set status = '9',bi010 = null where status = '1';
truncate table bill;
truncate table cb01;
truncate table cb02;
truncate table fa01;  --财务结算表
truncate table fa02;  --财务作废表
truncate table fc01;  --火化证明打印记录
truncate table fi01;  --财政发票打印日志
truncate table fv01;
--truncate table gi01;  --商品信息表
truncate table gr01;  --权限表
truncate table oc01;  --迁出记录表
truncate table prtserv;  --打印服务接口表
truncate table rc04;  --寄存费缴纳记录
truncate table rt01;  --寄存位置变更表
truncate table sa01;
truncate table sa01_log;
truncate table sa10;
--truncate table si01;
truncate table tax_log;
